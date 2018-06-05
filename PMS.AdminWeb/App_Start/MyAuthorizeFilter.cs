using PMS.CommonMVC;
using PMS.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.AdminWeb.App_Start
{
    public class MyAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            CheckPermissionAttribute[] permAtts = (CheckPermissionAttribute[])filterContext.ActionDescriptor
                .GetCustomAttributes(typeof(CheckPermissionAttribute), false);
            if (permAtts.Length <= 0)//没有标注任何的CheckPermissionAttribute，因此也就不需要检查是否登录
                                     //“无欲无求”
            {
                return;//登录等这些不要求有用户登录的功能
            }
            long? AdminId = (long?)filterContext.HttpContext.Session["LoginAdminId"];
            if (AdminId ==null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    AjaxResult ajaxResult = new AjaxResult();
                    ajaxResult.Status = "redirect";
                    ajaxResult.Data = "/Main/Login";
                    ajaxResult.ErrorMsg = "没有登录";
                    filterContext.Result = new JsonNetResult { Data = ajaxResult };
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Main/Login");
                }
                return;
                //filterContext.Result = new ContentResult() { Content= "没有登录" };
                //return;
            }


        }
    }
}