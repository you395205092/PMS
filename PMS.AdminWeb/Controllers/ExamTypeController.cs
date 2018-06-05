using PMS.DTO;
using PMS.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.CommonMVC;
using PMS.AdminWeb.App_Start;

namespace PMS.AdminWeb.Controllers
{
    public class ExamTypeController : Controller
    {
        public IExamTypeService examTypeService { get; set; }

        [CheckPermission("ExamType.List")]
        // GET: ExamType
        public ActionResult List()
        {
            var data = examTypeService.GetAll();
            return View(data);
        }
        [CheckPermission("ExamType.Delete")]
        public ActionResult Delete(long id)
        {
            examTypeService.MarkDeleted(id);

            return Json(new AjaxResult { Status = "ok" });
        }
        public ActionResult DeleteAll(long[] Ids)
        {
            foreach (var item in Ids)
            {
                examTypeService.MarkDeleted(item);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("ExamType.Add")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [CheckPermission("ExamType.Add")]
        [HttpPost]
        public ActionResult Add(string name,string description)
        {
            ExamTypeDTO dto = new ExamTypeDTO();
            dto.Description = description;
            dto.Name = name;
            examTypeService.Add(dto);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("ExamType.Edit")]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var data=examTypeService.GetById(id);
            return View(data);
        }
        [CheckPermission("ExamType.Edit")]
        [HttpPost]
        public ActionResult Edit(ExamTypeDTO model)
        {
            examTypeService.Edit(model);

            return Json(new AjaxResult { Status = "ok" });
        }

    }
}