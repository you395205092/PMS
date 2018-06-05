using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.CommonMVC
{
    /// <summary>
    /// 所有ajax都要返回这个类
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Data { get; set; }
    }
}
