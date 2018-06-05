using PMS.CommonMVC;
using PMS.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.AdminWeb.Controllers
{
    public class HomeController : Controller
    {
        public IExamService examService { get; set; }
        public IExamTypeService examTypeService { get; set; }
        // GET: Default
        public ActionResult Index()
        {
            var data = examTypeService.GetAll();
            return View(data);
        }
        [HttpGet]
        public ActionResult Inquiry()
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Inquiry(string StuId,string StuName,long TypeId)
        {
            if (string.IsNullOrWhiteSpace(StuId) || string.IsNullOrWhiteSpace(StuId))
            {
                return Content("<script>alert('请输入学号和姓名');location.href='/Home/'</script>");
            }
            else
            {
                var data = examService.GetAll().Where(p => p.StuId == StuId && p.StuName == StuName && p.TypeId== TypeId)
                .OrderByDescending(p => p.CreateDateTime).FirstOrDefault();

                
                if (data==null)
                {
                    return Content("<script>alert('对不起，没有查到您的信息，请检查你的学号和姓名是否正确!');location.href='/Home/'</script>");
                }
                else
                {
                    examService.UpdateLook(data.Id);
                    return View(data);
                }
                
            }
            
        }
    }
}