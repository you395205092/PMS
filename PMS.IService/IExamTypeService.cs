using PMS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.IService
{
    public interface IExamTypeService : IServiceSupport
    {
        void Add(ExamTypeDTO model);
        void Edit(ExamTypeDTO model);
        string GetByName(long Id);
        ExamTypeDTO[] GetAll();
        ExamTypeDTO GetById(long Id);
        void MarkDeleted(long Id);

    }
}
