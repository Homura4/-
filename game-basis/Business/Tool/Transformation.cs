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
        /// 返回活动状态
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
        public static string SwitchRelult(RelultInfo value)
        {
            switch (value)
            {
                case RelultInfo.Uncommitted:
                    return "未参加";
                case RelultInfo.InJoin:
                    return "参加中";
                case RelultInfo.EndJoin:
                    return "已参加";
                case RelultInfo.NotQualified:
                    return "没有资格";
                default :
                    return "未找到玩家";
            }
        }

        /// <summary>
        /// 入参验证
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="partnerId">合作商id</param>
        /// <returns>提示</returns>
        public static string RefVerification(string userId, int partnerId)
        {
            string relult = string.Empty;
            //userId验证
            if (string.IsNullOrEmpty(userId))
            {
                relult = "用户id为空";
            }
            if (userId.Length > 20)
            {
                relult = "用户id长度大于20";
            }
            if (userId.IndexOf(" ") > -1)
            {
                relult = "用户id不能含有空格";
            }
            //partnerId验证
            if (partnerId == 0)
            {
                relult = "合作公司id为空";
            }
            if (partnerId > 999999999)
            {
                relult = "合作公司id不超过9位";    // 响应内容
            }
            if (partnerId < 0)
            {
                relult = "合作公司id不为负数";
            }

            return relult;
        }
    }
}
