using Business.DataAccess;
using Business.Models;
using Business.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Business
{
    /// <summary>
    /// 逻辑处理
    /// </summary>
    public class LogBusiness
    {
        /// <summary>
        /// 查询是否有资格参加活动
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="partnerId">合作商id</param>
        /// <returns></returns>
        public Transformation.RelultInfo Qualifications(string userId, int partnerId)
        {
            //声明
            CacheData logdata = new CacheData();
            Transformation.RelultInfo relult = Transformation.RelultInfo.NotQualified;
            //参加回归活动的最低天数
            int days = 30;

            //获取玩家登录信息
            var maxTimeInfo = logdata.GetInfoByMaxTime(userId, partnerId).FirstOrDefault();
            if (maxTimeInfo ==null || maxTimeInfo.PlayerId == Guid.Empty)
            {
                return Transformation.RelultInfo.NotPlayer;//没找到玩家
            }
            //获取玩家参与活动信息
            var actitvitData = logdata.GetActitvityInfo("1", maxTimeInfo.PlayerId).FirstOrDefault();
            if (actitvitData == null || actitvitData.ActivityId == 0)
            {
                return Transformation.RelultInfo.NotQualified;//没有资格
            }
            //判断玩家参与活动资格
            if (actitvitData.ActitvityStatus != (int)Transformation.RelultInfo.InJoin && 
                actitvitData.ActitvityStatus != (int)Transformation.RelultInfo.EndJoin)
            {
                //大于三十天返回可以参加活动
                if ((DateTime.Now - maxTimeInfo.LoginTime).Days >= days)
                {
                    relult = Transformation.RelultInfo.Uncommitted;//未参加
                }
                else
                {
                    relult = Transformation.RelultInfo.NotQualified;//没有资格
                }
            }
            else if (actitvitData.ActitvityStatus  == (int)Transformation.RelultInfo.InJoin)
            {
                relult = Transformation.RelultInfo.InJoin;//参加中
            }
            else
            {
                relult = Transformation.RelultInfo.EndJoin;//已参加
            }
                 

            return relult;
        }

        /// <summary>
        /// 设置玩家已经参与活动
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="partnerId">合作商id</param>
        /// <returns></returns>
        public string SetActitvityStatus(string userId, int partnerId)
        {
            ///验证与声明
            CacheData logdb = new CacheData();
            var relult = "";
            string activityId = "1";//活动id

            //获取玩家登录信息
            var maxTimeInfo = logdb.GetInfoByMaxTime(userId, partnerId).FirstOrDefault();
            if (maxTimeInfo == null)
            {
                return "未找到玩家";
            }
            if (maxTimeInfo.PlayerId == Guid.Empty)
            {
                return "未找到玩家";
            }
            
            //写入活动状态
            relult = logdb.SetActitvityStatus(activityId, maxTimeInfo.PlayerId, (int)Transformation.RelultInfo.InJoin) ?"写入成功":"写入失败";

            return relult;
        }
    }
}
