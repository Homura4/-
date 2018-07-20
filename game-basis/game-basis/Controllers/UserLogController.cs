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
        /// <param name="userId">用户id</param>
        /// <param name="partnerId">合作商id</param>
        /// <returns></returns>
        [HttpPost]
        [CacheFilter(CacheTimeDuration = 10)]
        public HttpResponseMessage Qualifications(string userId, int partnerId)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.StatusCode = HttpStatusCode.OK;
            //入参验证
            if (userId == null || partnerId == 0)
            {
                response.Content = new StringContent("入参为空");    // 响应内容
            }
            if (userId.Length > 20 || Math.Log(partnerId) > 20)
            {
                response.Content = new StringContent("入参错误");    // 响应内容
            }
            try
            {
                var relult = new LogBusiness().Qualifications(userId, partnerId);
                response.Content = new StringContent(Transformation.SwitchRelult(relult));    // 响应内容
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
        /// <param name="userId">用户id</param>
        /// <param name="partnerId">合作商id</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SetActitvityStatus(string userId, int partnerId)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.StatusCode = HttpStatusCode.OK;
            //入参验证
            if (userId == null || partnerId == 0)
            {
                response.Content = new StringContent("入参为空");    // 响应内容
            }
            if (userId.Length > 20 || Math.Log(partnerId) > 20)
            {
                response.Content = new StringContent("入参错误");    // 响应内容
            }
            try
            {
                var relult = new LogBusiness().SetActitvityStatus(userId, partnerId);
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
