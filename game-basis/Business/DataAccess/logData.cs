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
using System.Linq;
using MySql.Data.MySqlClient;

namespace Business.DataAccess
{
    /// <summary>
    /// 数据库访问
    /// </summary>
    public class logData
    {
        //利用ConfigurationManager获取连接数据库字符串
        static string connString = System.Configuration.ConfigurationManager.AppSettings["gamedb"];
        //链接
        MySqlConnection conn = new MySqlConnection(connString);

        /// <summary>
        /// 查询最近的登陆记录
        /// </summary>
        /// <param name="UserId">用户id</param>
        /// <param name="PartnerId">合作商id</param>
        public List<UserLog> getInfoByMaxTime(string UserId, string PartnerId)
        {
            //返回单条信息
            string query = "SELECT UserId,PartnerID,PlayerId,MAX(LoginTime) FROM userloginfo WHERE UserId =@UserId AND PartnerId = @PartnerId;";
            var relult = conn.Query<UserLog>(query, new { @UserId = UserId, @PartnerId= PartnerId }).ToList();
            return relult;
        }

        /// <summary>
        /// 获取玩家参加活动记录
        /// </summary>
        /// <param name="UAId"></param>
        /// <param name="PlayerId"></param>
        /// <returns></returns>
        public List<UserActivity> getActitvityInfo(string UAId, Guid PlayerId)
        {
            //返回单条信息
            string query = "SELECT UAId,PlayerId,ActitvityStatus FROM useractivity WHERE UAId =@UAId AND PlayerId = @PlayerId;";
            var relult = conn.Query<UserActivity>(query, new { @UAId = UAId, @PlayerId = PlayerId }).ToList();
            return relult;
        }

        /// <summary>
        /// 设置玩家已经参与活动
        /// </summary>
        /// <param name="UAId">活动id</param>
        /// <param name="PlayerId">玩家id</param>
        /// <param name="Actitvity">玩家参加活动状态</param>
        /// <returns></returns>
        public bool UpdateActivity(string UAId,Guid PlayerId,int ActitvityStatus)
        {
            //返回单条信息
            string query = "UPDATE useractivity SET ActitvityStatus=@ActitvityStatus WHERE PlayerId =@PlayerId AND UAId=@UAId";
            var relult = conn.Execute(query, new { @UAId = UAId, @PlayerId= PlayerId, @ActitvityStatus = ActitvityStatus })>0;

            return relult;
        }
    }
}
