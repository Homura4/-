using Business.DataAccess;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Business
{
    /// <summary>
    /// 缓存处理类
    /// </summary>
    public class CacheData
    {
        /// <summary>
        /// 查询最近的登陆记录
        /// </summary>
        /// <param name="UserId">用户id</param>
        /// <param name="PartnerId">合作商id</param>
        /// <returns></returns>
        public IEnumerable<UserLog> GetInfoByMaxTime(string UserId, string PartnerId)
        {
            var cache = MyCache.GetCache("userLog_" + UserId+"_"+ PartnerId);//先读取
            if (cache == null)//如果没有该缓存
            {
                logData logdb = new logData();
                var queryCompany = logdb.getInfoByMaxTime(UserId, PartnerId); ;//从数据库取出
                var userLog = queryCompany.ToList();
                if (userLog == null || userLog.Count == 0)//如果没有数据直接返回
                {
                    return userLog;
                }
                MyCache.SetCache("userLog_"+ UserId + "_" + PartnerId, userLog);//添加缓存
                return userLog;
            }
            var result = (List<UserLog>)cache;//有就直接返回该缓存
            return result;
        }

        /// <summary>
        /// 获取玩家参加活动记录
        /// </summary>
        /// <param name="UAId"></param>
        /// <param name="PlayerId"></param>
        /// <returns></returns>
        public IEnumerable<UserActivity> GetActitvityInfo(string UAId, Guid PlayerId)
        {

            var cache = MyCache.GetCache("ActitvityInfo_" + UAId + "_" + PlayerId);//先读取
            if (cache == null )//如果没有该缓存
            {
                logData logdb = new logData();
                var queryCompany = logdb.getActitvityInfo(UAId, PlayerId); ;//从数据库取出
                var actitvityInfo = queryCompany.ToList();
                if (actitvityInfo == null || actitvityInfo.Count == 0)//如果没有数据直接返回
                {
                    return actitvityInfo;
                }

                MyCache.SetCache("ActitvityInfo_"+UAId+"_"+ PlayerId, actitvityInfo);//添加缓存
                return actitvityInfo;
            }

            var result = (List<UserActivity>)cache;//有就直接返回该缓存
            return result;
        }

        /// <summary>
        /// 设置玩家已经参与活动
        /// </summary>
        /// <param name="UAId">活动id</param>
        /// <param name="PlayerId">玩家id</param>
        /// <param name="Actitvity">玩家参加活动状态</param>
        /// <returns></returns>
        public bool UpdateActivity(string UAId, Guid PlayerId, int ActitvityStatus)
        {

            logData logdb = new logData();
            var relult = logdb.UpdateActivity(UAId, PlayerId, 1);//访问数据库

            if (relult)
            {
                MyCache.RemoveAllCache("ActitvityInfo_" + UAId + "_" + PlayerId);//移除缓存
            }

            return relult;
        }
    }
}
