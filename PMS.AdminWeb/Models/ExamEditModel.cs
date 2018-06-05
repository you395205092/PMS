using PMS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.AdminWeb.Models
{
    public class ExamEditModel
    {
        public ExamDTO exam { get; set; }
        public ExamTypeDTO[] examTypes { get; set; }


    }
}