using PMS.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Service.ModelConfig
{
    class ExamConfig: EntityTypeConfiguration<ExamEntity>
    {
        public ExamConfig()
        {
            ToTable("T_Exams");
            HasRequired(h => h.ExamTypes).WithMany().HasForeignKey(h => h.TypeId).WillCascadeOnDelete(false);
            Property(e => e.ZKZCode).HasMaxLength(50).IsRequired();
            Property(e => e.SFZCode).HasMaxLength(50).IsRequired();
            Property(e => e.StuName).HasMaxLength(50).IsRequired();
        }
    }
}
