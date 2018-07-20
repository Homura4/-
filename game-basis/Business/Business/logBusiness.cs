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
    public class logBusiness
    {
        /// <summary>
        /// 查询是否有资格参加活动
        /// </summary>
        /// <param name="UserId">用户id</param>
        /// <param name="PartnerId">合作商id</param>
        /// <returns></returns>
        public Transformation.RelultInfo Qualifications(string UserId, int PartnerId)
        {
            //声明
            CacheData logdata = new CacheData();
            Transformation.RelultInfo relult = Transformation.RelultInfo.NotQualified;

            //获取玩家登录信息
            var maxTimeInfo = logdata.GetInfoByMaxTime(UserId, PartnerId).FirstOrDefault();
            if (maxTimeInfo ==null || maxTimeInfo.PlayerId == Guid.Empty)
            {
                return Transformation.RelultInfo.NotPlayer;//没找到玩家
            }
            //获取玩家参与活动信息
            var actitvitData = logdata.GetActitvityInfo("1", maxTimeInfo.PlayerId).FirstOrDefault();
            if (actitvitData == null || actitvitData.UAId == 0)
            {
                return Transformation.RelultInfo.NotQualified;//没有资格
            }
            //判断玩家参与活动资格
            if (actitvitData.ActitvityStatus != 1 && actitvitData.ActitvityStatus !=2)
            {
                //大于三十天返回可以参加活动
                if ((DateTime.Now - maxTimeInfo.LoginTime).Days >= 30)
                {
                    relult = Transformation.RelultInfo.Uncommitted;//未参加
                }
                else
                {
                    relult = Transformation.RelultInfo.NotQualified;//没有资格
                }
            }
            else if (actitvitData.ActitvityStatus  == 1)
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
        /// <param name="UserId">用户id</param>
        /// <param name="PartnerId">合作商id</param>
        /// <returns></returns>
        public string SetActitvityStatus(string UserId, int PartnerId)
        {
            ///验证与声明
            CacheData logdb = new CacheData();
            var relult = "";

            //获取玩家登录信息
            var maxTimeInfo = logdb.GetInfoByMaxTime(UserId, PartnerId).FirstOrDefault();
            if (maxTimeInfo == null || maxTimeInfo.PlayerId == Guid.Empty)
            {
                return "未找到玩家";
            }
            //写入活动状态
            relult = logdb.SetActitvityStatus("1", maxTimeInfo.PlayerId, 1)?"写入成功":"写入失败";

            return relult;
        }
    }
}
