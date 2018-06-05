using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PMS.CommonMVC
{
    public class TrimToDBCModelBinder: DefaultModelBinder
    {
        /// <summary>
        /// 重新方法过滤字符AOP
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object value = base.BindModel(controllerContext, bindingContext);
            if (value is string)
            {
                string strValue = (string)value;
                string value2 = ToDBC(strValue).Trim();
                return value2;
            }
            else
            {
                return value;
            }
        }
        /// <summary>
        /// 过滤全交字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                {
                    c[i] = (char)(c[i] - 65248);
                }
            }
            return new string(c);
        }
    }
}
