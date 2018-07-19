using game_basis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.Business;
using System.Threading.Tasks;

namespace game_basis.Controllers
{
    /// <summary>
    /// 老用户判断与写入
    /// </summary>
    public class UserLogController : ApiController
    {
        /// <summary>
        /// 判断有没有资格参加活动
        /// </summary>
        /// <param name="UserId">用户id</param>
        /// <param name="PartnerId">合作商id</param>
        /// <returns></returns>
        [HttpPost]
        [CacheFilter(CacheTimeDuration = 10)]
        public HttpResponseMessage Qualifications(string UserId, string PartnerId)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent("操作失败");    // 响应内容
            try
            {
                if (UserId != null && PartnerId != null)
                {
                    var relult = new logBusiness().qualifications(UserId, PartnerId);
                    response.Content = new StringContent(relult == 0 ? "有资格未参加" : relult == 1 ? "已参加" : "没资格");    // 响应内容
                }
            }
            catch (Exception e)
            {
                response.Content = new StringContent(e.Message);
            }

            return response;
            //return relult;
        }

        /// <summary>
        /// 设置此玩家已经参与了老玩家回归活动
        /// </summary>
        /// <param name="UserId">用户id</param>
        /// <param name="PartnerId">合作商id</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage UpdateActivity(string UserId, string PartnerId)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent("操作失败");    // 响应内容
            try
            {
                if (UserId != null && PartnerId != null)
                {
                    var relult = new logBusiness().UpdateActivity(UserId, PartnerId);
                    response.Content = new StringContent(relult ? "设置成功" : "设置失败");    // 响应内容
                }
            }
            catch (Exception e)
            {
                response.Content = new StringContent(e.Message);
            }

            return response;
            //return relult;
        }
    }
}
