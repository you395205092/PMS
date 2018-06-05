using PMS.CommonMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.AdminWeb.App_Start
{
    public class FilterConfig
    {
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            //异常AOP
            filters.Add(new MyExceptionFilter());
            //对return JsonResult的重写 AOP
            filters.Add(new JsonNetActionFilter());
            //权限校验AOP
            filters.Add(new MyAuthorizeFilter());
        }
    }
}