using PMS.AdminWeb.App_Start;
using PMS.AdminWeb.Models;
using PMS.CommonMVC;
using PMS.DTO;
using PMS.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.AdminWeb.Controllers
{
    public class UserController : Controller
    {
        public IAdminUserService AdminUserService { get; set; }

        [CheckPermission("User.List")]
        // GET: User
        public ActionResult List()
        {
            AdminUserDTO[] data = AdminUserService.GetAll();
            return View(data);
        }
        [CheckPermission("User.Edit")]
        [HttpGet]
        public ActionResult Edit(long Id)
        {
            var data= AdminUserService.GetById(Id);
            return View(data);
        }
        [CheckPermission("User.Edit")]
        [HttpPost]
        public ActionResult Edit(long Id, string PassWord,long[] RoleIds)
        {
            bool b = AdminUserService.Edit(Id, PassWord);
            if (b)
            {
                return Json(new AjaxResult
                {
                    Status = "ok"
                });
            }
            else
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = "修改ID不存在"
                });
            }
        }
        [CheckPermission("User.Add")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [CheckPermission("User.Add")]
        [HttpPost]
        public ActionResult Add(string Name, string PassWord,long[] RoleIds)
        {
            long adminId = AdminUserService.Add(Name, PassWord);

            if (adminId > 0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "添加失败" });
            }
        }
        [CheckPermission("User.Delete")]
        public ActionResult Delete(long AdminId)
        {
            AdminUserService.MarkDeleted(AdminId);
            return Json(new AjaxResult { Status = "ok" });
        }
        
    }
}