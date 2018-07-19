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
        /// 活动状态0:没有参加1:参加中2:无法参加
        /// </summary>
        public int ActitvityStatus { get; set; } = 2;
    }
}
