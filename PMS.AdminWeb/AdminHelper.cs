using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.AdminWeb
{
    public class AdminHelper
    {
        public static long? GetAdminId(HttpContextBase ctx)
        {
            return (long?)ctx.Session["LoginAdminId"];
        }
    }
}