using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    /// <summary>
    /// 用户活动参与表
    /// </summary>
    public class UserActivity
    {
        /// <summary>
        /// 参与活动id
        /// </summary>
        public int UAId { get; set; } = 0;

        /// <summary>
        /// 玩家id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 活动状态 未参加 = 0,参加中 = 1,已参加 = 2,没有资格 = 3,未找到玩家 =4
        /// </summary>
        public int ActitvityStatus { get; set; } = 3;

    }
}
