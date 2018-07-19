using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Filters;

namespace game_basis
{
    /// <summary>
    /// 缓存控制(页面)
    /// </summary>
    public class CacheFilterAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// 缓存时间(秒)
        /// </summary>
        public int CacheTimeDuration { get; set; }

        /// <summary>
        /// 缓存再请求
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromSeconds(CacheTimeDuration),
                MustRevalidate = true,
                Public = true
            };
        }
    }
}