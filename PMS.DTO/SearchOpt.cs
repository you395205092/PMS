using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DTO
{
    public class SearchOpt
    {
        public long? TypeId { get; set; }
        public int? IsLook { get; set; }
        public string Keywords { get; set; }
        public int PageSize { get; set; }//每页数据条数
        public int CurrentIndex { get; set; }//当前页码（页码从1开始）
    }
}
