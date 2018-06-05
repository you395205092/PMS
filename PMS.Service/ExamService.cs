using PMS.IService;
using PMS.Service;
using PMS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Service.Entities;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity;

namespace PMS.Service
{
    public class ExamService : IExamService
    {
        public long Add(ExamDTO model)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                ExamEntity ef = new ExamEntity();
                ef.Address = model.Address;
                ef.ClassName = model.ClassName;
                ef.ExamTime = model.ExamTime;
                ef.MajorName = model.MajorName;
                ef.PlaceNum = model.PlaceNum;
                ef.SchoolName = model.SchoolName;
                ef.SFZCode = model.SFZCode;
                ef.StuName = model.StuName;
                ef.SysName = model.SysName;
                ef.ZKZCode = model.ZKZCode;
                ef.TypeId = model.TypeId;
                ef.LLExamAddress = model.LLExamAddress;
                ef.LLExamTime = model.LLExamTime;
                ef.LLExamPlaceNum = model.LLExamPlaceNum;
                ef.StuId = model.StuId;
                ef.llzwh = model.llzwh;
                ctx.Exams.Add(ef);
                ctx.SaveChanges();
                return ef.Id;
            }
        }

        public void Deleted(long Id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<ExamEntity> bs = new BaseService<ExamEntity>(ctx);
                bs.MarkDeleted(Id);
            }
        }

        public long Edit(ExamDTO model)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<ExamEntity> bs = new BaseService<ExamEntity>(ctx);
                var data = bs.GetById(model.Id);
                if (data == null)
                {
                    throw new ArgumentException("找不到记录id为：" + model.Id);
                }
                data.Address = model.Address;
                data.ClassName = model.ClassName;
                data.ExamTime = model.ExamTime;
                data.LLExamAddress = model.LLExamAddress;
                data.LLExamPlaceNum = model.LLExamPlaceNum;
                data.LLExamTime = model.LLExamTime;
                data.MajorName = model.MajorName;
                data.PlaceNum = model.PlaceNum;
                data.SchoolName = model.SchoolName;
                data.SFZCode = model.SFZCode;
                data.StuId = model.StuId;
                data.StuName = model.StuName;
                data.SysName = model.SysName;
                data.TypeId = model.TypeId;
                data.ZKZCode = model.ZKZCode;
                data.llzwh = model.llzwh;
                ctx.SaveChanges();
                return data.Id;

            }
        }

        public ExamDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<ExamEntity> bs = new BaseService<ExamEntity>(ctx);
                return bs.GetAll().Include(h => h.ExamTypes).ToList().Select(p => ToDTO(p)).ToArray();
            }
        }
        public ExamDTO ToDTO(ExamEntity ef)
        {
            ExamDTO dto = new ExamDTO();
            dto.Address = ef.Address;
            dto.ClassName = ef.ClassName;
            dto.ExamTime = ef.ExamTime;
            dto.MajorName = ef.MajorName;
            dto.PlaceNum = ef.PlaceNum;
            dto.SchoolName = ef.SchoolName;
            dto.SFZCode = ef.SFZCode;
            dto.StuName = ef.StuName;
            dto.SysName = ef.SysName;
            dto.TypeId = ef.TypeId;
            dto.TypeName = ef.ExamTypes.Name;
            dto.TypeDescription = ef.ExamTypes.Description;
            dto.ZKZCode = ef.ZKZCode;
            dto.IsLook = ef.IsLook;
            dto.LLExamAddress = ef.LLExamAddress;
            dto.LLExamTime = ef.LLExamTime;
            dto.LLExamPlaceNum = ef.LLExamPlaceNum;
            dto.StuId = ef.StuId;
            dto.Id = ef.Id;
            dto.llzwh = ef.llzwh;
            return dto;
        }
        public SearchResult Search(SearchOpt options)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<ExamEntity> bs = new BaseService<ExamEntity>(ctx);
                BaseService<ExamTypeEntity> bsType = new BaseService<ExamTypeEntity>(ctx);
                var items = bs.GetAll();
                if (options.TypeId != null && options.TypeId != 9999)
                {
                    items = items.Where(t => t.ExamTypes.Id == options.TypeId);
                }
                if (!string.IsNullOrEmpty(options.Keywords))
                {
                    items = items.Where(t => t.StuName == options.Keywords
                    || t.SFZCode == options.Keywords
                    || t.ZKZCode == options.Keywords);
                }
                if (options.IsLook !=null && options.IsLook !=2)
                {
                    if (options.IsLook==0)
                    {
                        items = items.Where(t => t.IsLook == false);
                    }
                    else
                    {
                        items = items.Where(t => t.IsLook == true);
                    }
                    
                }
                var typeids = bsType.GetAll().Select(e => e.Id);
                items = items.Where(m => typeids.Contains(m.TypeId));

                long totalCount = items.LongCount();//总搜索结果条数
                items = items.Include(e => e.ExamTypes).OrderByDescending(t => t.CreateDateTime).Skip((options.CurrentIndex - 1) * options.PageSize)
                    .Take(options.PageSize);
                SearchResult res = new SearchResult();
                res.totalCount = totalCount;
                List<ExamDTO> exams = new List<ExamDTO>();
                foreach (var item in items)
                {
                    exams.Add(ToDTO(item));
                }
                res.result = exams.ToArray();
                List<ExamTypeDTO> examTypes = new List<ExamTypeDTO>();
                var efexamTypes = bsType.GetAll();


                ExamTypeDTO dtotype2 = new ExamTypeDTO();
                dtotype2.Description = "所有";
                dtotype2.Name = "全部";
                dtotype2.Id = 9999;
                examTypes.Add(dtotype2); 
                foreach (var item in efexamTypes)
                {
                    ExamTypeDTO dtotype = new ExamTypeDTO();
                    dtotype.Description = item.Description;
                    dtotype.Name = item.Name;
                    dtotype.Id = item.Id;
                    examTypes.Add(dtotype);
                }
                res.examType = examTypes.ToArray();
                return res;
            }
        }

        public ExamDTO GetById(long Id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<ExamEntity> bs = new BaseService<ExamEntity>(ctx);
                return bs.GetAll().Include(h => h.ExamTypes).OrderByDescending(e => e.CreateDateTime).ToList().Select(p => ToDTO(p)).FirstOrDefault();
            }
        }

        public void UpdateLook(long Id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<ExamEntity> bs = new BaseService<ExamEntity>(ctx);
                var data = bs.GetById(Id);

                if (data == null)
                {
                    throw new ArgumentException("找不到记录id为：" + Id);
                }
                data.IsLook = true;
                ctx.SaveChanges();
            }
        }

        public void UpdateAllNoLook(long TypeID)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<ExamEntity> bs = new BaseService<ExamEntity>(ctx);
                var data = bs.GetAll().Where(p => p.IsLook);
                foreach (var item in data)
                {
                    item.IsLook = false;
                }
                ctx.SaveChanges();
            }
        }
    }
}
