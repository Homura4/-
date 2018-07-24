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
    /// 用户登录表操作
    /// </summary>
    public class UserLogInfo
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
            var result = conn.Query<UserLog>(query, new { @UserId = userId, @PartnerId= partnerId }).ToList();

            return result;
        }


    }
}
