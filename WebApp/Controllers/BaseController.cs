using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 设备UUID对应用户
        /// </summary>
        public User WeakUser
        { get; set; }

        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin
        { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UID
        { get; set; }

        /// <summary>
        /// 返回JsonResult
        /// </summary>
        /// <param name="data"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        protected JsonResult JsonResult(object data, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            return Json(data, behavior);
        }

        [NonAction]
        public JsonResult NotPassCheck(User user)
        {
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                m = -1000,
            };

            return jr;
        }

        /// <summary>
        /// 获取图片Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string FormatImgUrl(string url)
        {
            return string.IsNullOrWhiteSpace(url) ? "" : string.Format("{0}{1}", ConfigHelper.ImgDomain, url);
        }

        /// <summary>
        /// 获取视频Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string FormatVideoUrl(string url)
        {
            return string.IsNullOrWhiteSpace(url) ? "" : string.Format("{0}{1}", ConfigHelper.VideoDomain, url);
        }
    }
}
