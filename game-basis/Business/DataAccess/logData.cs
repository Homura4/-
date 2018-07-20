using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace Business.DataAccess
{
    /// <summary>
    /// 数据库访问
    /// </summary>
    public class LogData
    {
        //利用ConfigurationManager获取连接数据库字符串
        static string connString = System.Configuration.ConfigurationManager.AppSettings["gamedb"];


        /// <summary>
        /// 查询最近的登陆记录
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="partnerId">合作商id</param>
        public List<UserLog> GetInfoByMaxTime(string userId, int partnerId)
        {
            //链接
            MySqlConnection conn = new MySqlConnection(connString);
            //返回单条信息
            string query = "SELECT UserId,PartnerID,PlayerId,MAX(LoginTime) FROM userloginfo WHERE UserId =@UserId AND PartnerId = @PartnerId;";
            var relult = conn.Query<UserLog>(query, new { @UserId = userId, @PartnerId= partnerId }).ToList();

            return relult;
        }

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
            var relult = conn.Query<UserActivity>(query, new { @ActivityId = activityId, @PlayerId = playerId }).ToList();

            return relult;
        }

        /// <summary>
        /// 设置玩家已经参与活动
        /// </summary>
        /// <param name="activityId">活动id</param>
        /// <param name="playerId">玩家id</param>
        /// <param name="Actitvity">玩家参加活动状态</param>
        /// <returns></returns>
        public bool SetActitvityStatus(string activityId,Guid playerId,int actitvityStatus)
        {
            //链接
            MySqlConnection conn = new MySqlConnection(connString);
            //返回单条信息
            string query = "INSERT INTO useractivity(PlayerId,ActitvityStatus,ActivityId) VALUES(@PlayerId,@ActitvityStatus,@ActivityId);";
            var relult = conn.Execute(query, new { @ActivityId = activityId, @PlayerId= playerId, @ActitvityStatus = actitvityStatus })>0;

            return relult;
        }
    }
}
