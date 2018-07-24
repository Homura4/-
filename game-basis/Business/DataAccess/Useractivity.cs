using Business.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataAccess
{
    /// <summary>
    /// 活动类
    /// </summary>
    public class Useractivity
    {
        //利用ConfigurationManager获取连接数据库字符串
        static string connString = System.Configuration.ConfigurationManager.AppSettings["gamedb"];

        /// <summary>
        /// 获取玩家参加活动记录
        /// </summary>
        /// <param name="activityId">活动id</param>
        /// <param name="playerId">玩家id</param>
        /// <returns></returns>
        public List<UserActivity> GetActitvityInfo(string activityId, Guid playerId)
        {
            //链接
            MySqlConnection conn = new MySqlConnection(connString);
            //返回单条信息
            string query = "SELECT UAId,PlayerId,ActitvityStatus,ActivityId FROM useractivity WHERE ActivityId =@ActivityId AND PlayerId = @PlayerId;";
            var result = conn.Query<UserActivity>(query, new { @ActivityId = activityId, @PlayerId = playerId }).ToList();

            return result;
        }

        /// <summary>
        /// 设置玩家已经参与活动
        /// </summary>
        /// <param name="activityId">活动id</param>
        /// <param name="playerId">玩家id</param>
        /// <param name="Actitvity">玩家参加活动状态</param>
        /// <returns></returns>
        public bool SetActitvityStatus(string activityId, Guid playerId, int actitvityStatus)
        {
            //链接
            MySqlConnection conn = new MySqlConnection(connString);
            //返回单条信息
            string query = "INSERT INTO useractivity(PlayerId,ActitvityStatus,ActivityId) VALUES(@PlayerId,@ActitvityStatus,@ActivityId);";
            var result = conn.Execute(query, new { @ActivityId = activityId, @PlayerId = playerId, @ActitvityStatus = actitvityStatus });

            return result > 0;
        }
    }
}
