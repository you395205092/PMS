using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.AdminWeb.App_Start
{
    public class MyExceptionFilter : IExceptionFilter
    {
        private ILog log = LogManager.GetLogger(typeof(MyExceptionFilter));
        public void OnException(ExceptionContext filterContext)
        {
            //log.Error("出现未处理异常", filterContext.Exception);
            log.DebugFormat("未处理异常{0}", filterContext.Exception);
        }
    }
}