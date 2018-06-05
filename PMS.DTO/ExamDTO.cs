using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DTO
{
    public class ExamDTO:BaseDTO
    {
        public long TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }
        public string ZKZCode { get; set; }
        public string StuName { get; set; }
        public string SFZCode { get; set; }
        public string Address { get; set; }
        public string ExamTime { get; set; }
        public string PlaceNum { get; set; }
        public string SchoolName { get; set; }
        public string SysName { get; set; }
        public string MajorName { get; set; }
        public string ClassName { get; set; }
        public bool IsLook { get; set; }
        public string LLExamAddress { get; set; }
        public string LLExamTime { get; set; }
        public string LLExamPlaceNum { get; set; }
        public string StuId { get; set; }
        public string llzwh { get; set; }
    }
}
