using game_basis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.Business;
using System.Threading.Tasks;
using Business.Tool;

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
        public HttpResponseMessage Qualifications(string UserId, int PartnerId)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.StatusCode = HttpStatusCode.OK;
            //入参验证
            if (UserId == null || PartnerId == 0)
            {
                response.Content = new StringContent("入参为空");    // 响应内容
            }
            if (UserId.Length > 20 || Math.Log(PartnerId) > 20)
            {
                response.Content = new StringContent("入参错误");    // 响应内容
            }
            try
            {
                var relult = new logBusiness().Qualifications(UserId, PartnerId);
                response.Content = new StringContent(Transformation.SwitchRelult((int)relult));    // 响应内容
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
        public HttpResponseMessage SetActitvityStatus(string UserId, int PartnerId)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.StatusCode = HttpStatusCode.OK;
            //入参验证
            if (UserId == null || PartnerId == 0)
            {
                response.Content = new StringContent("入参为空");    // 响应内容
            }
            if (UserId.Length > 20 || Math.Log(PartnerId) > 20)
            {
                response.Content = new StringContent("入参错误");    // 响应内容
            }
            try
            {
                var relult = new logBusiness().SetActitvityStatus(UserId, PartnerId);
                response.Content = new StringContent(relult);    // 响应内容
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
