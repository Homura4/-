using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    /// <summary>
    /// 用户登陆记录
    /// </summary>
    public class UserLog
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 合作商Id
        /// </summary>
        public int PartnerId { get; set; } = 0;

        /// <summary>
        /// 玩家id
        /// </summary>
        public Guid PlayerId { get; set; } 

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }


    }
}
