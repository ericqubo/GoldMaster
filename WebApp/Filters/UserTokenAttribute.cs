using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Controllers;
using WebApp.Lib;

namespace WebApp.Filters
{
    /// <summary>
    /// 根据token获取用户ID
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class UserTokenAttribute : ActionFilterAttribute
    {
        public bool MustTokenIsRight;

        /// <summary>
        /// 必须验证Token
        /// </summary>
        /// <param name="mustTokenIsRight"></param>
        public UserTokenAttribute(bool mustTokenIsRight = false)
        {
            MustTokenIsRight = mustTokenIsRight;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(filterContext.Controller is BaseController))
            {
                throw new Exception("UUIDAttribute所在的Controller必须继承BaseController");
            }

            UUIDFilter(filterContext);

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        private void UUIDFilter(ActionExecutingContext filterContext)
        {
            //判断UUID是否存在
            if (string.IsNullOrEmpty(filterContext.HttpContext.Request.Params["_uuid"]))
            {
                filterContext.Result = new JsonResult() { Data = new { m = -1001, e = DataDict_Error.UserErrorDict["2001"] }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return;
            }

            BaseController bc = (filterContext.Controller as BaseController);
            var uuid = filterContext.HttpContext.Request.Params["_uuid"];          //机器码

            //查询用户
            var user = User.GetByIdentity(uuid);
            if (user != null)
            {
                bc.UID = user.ID;
            }
            else
            {
                user = new User();
                user.Identity = uuid;

                User.Set(user);
                bc.UID = user.ID;
            }

            bc.WeakUser = user;
        }
    }
}