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
        /// <param name="playerId">玩家id反参 仅用于设置接口验证使用</param>
        /// <returns></returns>
        public Transformation.RelultInfo Qualifications(string userId, int partnerId,out Guid playerId)
        {
            //声明
            CacheData logdata = new CacheData();
            Transformation.RelultInfo relult = Transformation.RelultInfo.NotQualified;
            //参加回归活动的最低天数
            int days = 30;
            //玩家id 默认返回空
            playerId = Guid.Empty;

            //获取玩家登录信息
            var maxTimeInfo = logdata.GetInfoByMaxTime(userId, partnerId).FirstOrDefault();
            if (maxTimeInfo ==null || maxTimeInfo.PlayerId == Guid.Empty)
            {
                return Transformation.RelultInfo.NotPlayer;//没找到玩家
            }
            //获取玩家参与活动信息
            var actitvitData = logdata.GetActitvityInfo("1", maxTimeInfo.PlayerId).FirstOrDefault();
            //判断玩家参与活动资格
            if (actitvitData == null)
            {
                //大于三十天返回可以参加活动
                if ((DateTime.Now - maxTimeInfo.LoginTime).Days >= days)
                {
                    playerId = maxTimeInfo.PlayerId;//有资格参加,返回玩家id
                    relult = Transformation.RelultInfo.Uncommitted;//未参加
                }
                else
                {
                    relult = Transformation.RelultInfo.NotQualified;//没有资格
                }
            }
            else
            {
                //返回已参加的活动状态
                relult = (Transformation.RelultInfo)actitvitData.ActitvityStatus;
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
            Guid playerId =Guid.Empty;

            //获取玩家登录信息
            var QualificationsRelult = Qualifications(userId, partnerId,out playerId);
            //玩家资格验证
            if (QualificationsRelult != Transformation.RelultInfo.Uncommitted)
            {
                return Transformation.SwitchRelult(QualificationsRelult);
            }
            
            //判断是否已经参加活动
            var actitvityInfo = logdb.GetActitvityInfo(activityId, playerId);
            if (actitvityInfo.Count() > 0)
            {
                return "玩家已参加活动";
            }
            //写入活动状态
            relult = logdb.SetActitvityStatus(activityId, playerId, (int)Transformation.RelultInfo.InJoin) ?"写入成功":"写入失败";

            return relult;
        }
    }
}
