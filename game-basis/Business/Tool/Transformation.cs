using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Tool
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Transformation
    {
        /// <summary>
        /// 返回类型
        /// </summary>
        public enum RelultInfo
        {
            /// <summary>
            /// 未参加 
            /// </summary>
            Uncommitted = 0,

            /// <summary>
            /// 参加中 
            /// </summary>
            InJoin = 1,

            /// <summary>
            /// 已参加 
            /// </summary>
            EndJoin = 2,

            /// <summary>
            /// 没有资格 
            /// </summary>
            NotQualified = 3,

            /// <summary>
            /// 未找到玩家 
            /// </summary>
            NotPlayer = 4

        }

        /// <summary>
        /// 结果转换提示语句 未参加 = 0,参加中 = 1,已参加 = 2,没有资格 = 3,未找到玩家 =4
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>提示语句</returns>
        public static string SwitchRelult(int value)
        {
            switch (value)
            {
                case 0:
                    return "未参加";
                case 1:
                    return "参加中";
                case 2:
                    return "已参加";
                case 3:
                    return "没有资格";
                default:
                    return "未找到玩家";
            }
        }
    }
}
