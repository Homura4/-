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
        //声明数据库写入类,静态全局 多线程并发 活动类
        private static Useractivity logdb = new Useractivity();

        /// <summary>
        /// 查询最近的登陆记录
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="partnerId">合作商id</param>
        /// <returns>登录记录</returns>
        public IEnumerable<UserLog> GetInfoByMaxTime(string userId, int partnerId)
        {
            //缓存的key
            string cacheKey = "userLog_" + userId + "_" + partnerId;
            var cache = MyCache.GetCache(cacheKey);//先读取
            if (cache == null)//如果没有该缓存
            {
                UserLogInfo logdb = new UserLogInfo();
                var userLog = logdb.GetInfoByMaxTime(userId, partnerId); ;//从数据库取出
                if (userLog == null || userLog.Count == 0)//如果没有数据直接返回
                {
                    return userLog;
                }
                MyCache.SetCache(cacheKey, userLog);//添加缓存
                return userLog;
            }
            var result = (List<UserLog>)cache;//有就直接返回该缓存
            return result;
        }

        /// <summary>
        /// 获取玩家参加活动记录
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="PlayerId"></param>
        /// <returns></returns>
        public IEnumerable<UserActivity> GetActitvityInfo(string activityId, Guid PlayerId)
        {
            //缓存的key
            string cacheKey = "ActitvityInfo_" + activityId + "_" + PlayerId;
            var cache = MyCache.GetCache(cacheKey);//先读取
            if (cache == null )//如果没有该缓存
            {
                Useractivity logdb = new Useractivity();
                var actitvityInfo = logdb.GetActitvityInfo(activityId, PlayerId); ;//从数据库取出
                if (actitvityInfo == null || actitvityInfo.Count == 0)//如果没有数据直接返回
                {
                    return actitvityInfo;
                }

                MyCache.SetCache(cacheKey, actitvityInfo);//添加缓存
                return actitvityInfo;
            }

            var result = (List<UserActivity>)cache;//有就直接返回该缓存
            return result;
        }

        /// <summary>
        /// 设置玩家已经参与活动
        /// </summary>
        /// <param name="activityId">活动id</param>
        /// <param name="playerId">玩家id</param>
        /// <param name="Actitvity">玩家参加活动状态未参加 = 0,参加中 = 1,已参加 = 2,没有资格 = 3,未找到玩家 =4</param>
        /// <returns>是否设置成功</returns>
        public bool SetActitvityStatus(string activityId, Guid playerId, int actitvityStatus)
        {
            bool result;
            //锁定
            lock (logdb)
            {
                result = logdb.SetActitvityStatus(activityId, playerId, 1);//访问数据库
            }

            if (result)
            {
                MyCache.RemoveAllCache("ActitvityInfo_" + activityId + "_" + playerId);//移除缓存
            }

            return result;
        }
    }
}
