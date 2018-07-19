using Business.DataAccess;
using Business.Models;
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
        /// <param name="UserId"></param>
        /// <param name="PartnerId"></param>
        /// <returns></returns>
        public int qualifications(string UserId, string PartnerId)
        {
            //声明
            CacheData logdata = new CacheData();
            string userid = UserId.Length < 20 ? UserId : string.Empty;
            string partnerid = PartnerId.Length <= 20 ? Regex.IsMatch(PartnerId, @"^[+-]?\d*[.]?\d*$") ? 
                PartnerId : string.Empty : string.Empty;
            int relult = 2;
            
            //获取玩家登录信息
            var maxTimeInfo = logdata.GetInfoByMaxTime(userid, partnerid).FirstOrDefault();
            maxTimeInfo = maxTimeInfo == null ? new UserLog() : maxTimeInfo;
            //获取玩家参与活动信息
            var data = logdata.GetActitvityInfo("1", maxTimeInfo.PlayerId).FirstOrDefault();
            data = data == null ? new UserActivity() : data;
            //判断玩家参与活动资格
            if (data.ActitvityStatus != 1)
            {
                //大于三十天返回可以参加活动
                relult = ((DateTime.Now - maxTimeInfo.LoginTime).Days >= 30) ? 0 : 2;
            }

            return data.UAId == 0 ? 2 : relult;
        }

        /// <summary>
        /// 设置玩家已经参与活动
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PartnerId"></param>
        /// <returns></returns>
        public bool UpdateActivity(string UserId, string PartnerId)
        {
            ///验证与声明
            logData logdb = new logData();
            string userid = UserId.Length < 20 ? UserId : string.Empty;
            string partnerid = PartnerId.Length <= 20 ? Regex.IsMatch(PartnerId, @"^[+-]?\d*[.]?\d*$") ? 
                PartnerId : string.Empty : string.Empty;
            var relult = false;

            //获取玩家登录信息
            var maxTimeInfo = logdb.getInfoByMaxTime(userid, partnerid).FirstOrDefault();
            //写入活动状态
            relult = logdb.UpdateActivity("1", maxTimeInfo.PlayerId, 1);

            return relult;
        }
    }
}
