using PMS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.IService
{
    public interface IExamService : IServiceSupport
    {
        long Add(ExamDTO model);
        long Edit(ExamDTO model);
        void Deleted(long Id);
        ExamDTO[] GetAll();
        ExamDTO GetById(long Id);
        SearchResult Search(SearchOpt search);
        void UpdateLook(long Id);
        void UpdateAllNoLook(long TypeID);

    }
}
