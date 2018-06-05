using PMS.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Service.ModelConfig
{
    class ExamTypeConfig: EntityTypeConfiguration<ExamTypeEntity>
    {
        public ExamTypeConfig()
        {
            ToTable("T_ExamTypes");
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            Property(e => e.Name).IsRequired().HasMaxLength(255);
        }
    }
}
