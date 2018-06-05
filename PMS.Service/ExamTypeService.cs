using PMS.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.DTO;
using PMS.Service.Entities;

namespace PMS.Service
{
    public class ExamTypeService : IExamTypeService
    {
        public void Add(ExamTypeDTO model)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                ExamTypeEntity ef = new ExamTypeEntity();
                ef.Description = model.Description;
                ef.Name = model.Name;
                ctx.ExamTypes.Add(ef);
                ctx.SaveChanges();
            }
        }

        public void Edit(ExamTypeDTO model)
        {
            using (MyDbContext ctx =new MyDbContext())
            {
                BaseService<ExamTypeEntity> bs = new BaseService<ExamTypeEntity>(ctx);
                var data=bs.GetById(model.Id);
                if (data==null)
                {
                    throw new ArgumentNullException("没有找到记录id=" + model.Id);
                }
                data.Name = model.Name;
                data.Description = model.Description;
                ctx.SaveChanges();
            }
        }

        public ExamTypeDTO[] GetAll()
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<ExamTypeEntity> bs = new BaseService<ExamTypeEntity>(ctx);


                return bs.GetAll().OrderByDescending(e=>e.CreateDateTime).ToList().Select(e=>ToDTO(e)).ToArray();
            }
        }
        public ExamTypeDTO ToDTO(ExamTypeEntity ef)
        {
            ExamTypeDTO dto = new ExamTypeDTO();
            dto.Description = ef.Description;
            dto.Name = ef.Name;
            dto.Id = ef.Id;
            return dto;
        }
        public ExamTypeDTO GetById(long Id)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<ExamTypeEntity> bs = new BaseService<ExamTypeEntity>(ctx);
                return ToDTO(bs.GetById(Id));

            }
        }
        public string GetByName(long Id)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<ExamTypeEntity> bs = new BaseService<ExamTypeEntity>(ctx);
                var data=bs.GetById(Id);
                if (data==null)
                {
                    throw new ArgumentNullException("没有找到记录id=" + Id);
                }
                return data.Name;

            }
        }

        public void MarkDeleted(long Id)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<ExamTypeEntity> bs = new BaseService<ExamTypeEntity>(ctx);
                bs.MarkDeleted(Id);
            }
        }
    }
}
