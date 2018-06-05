using PMS.Common;
using PMS.CommonMVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaGen;
using PMS.IService;
using PMS.AdminWeb.Models;
using PMS.AdminWeb.App_Start;

namespace PMS.AdminWeb.Controllers
{
    public class MainController : Controller
    {
        public IAdminUserService adminUserService { get; set; }
        [CheckPermission("Main.Index")]
        // GET: Main
        public ActionResult Index()
        {
            long? adminId=AdminHelper.GetAdminId(HttpContext);
            ViewBag.AdminId = adminId;
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    ErrorMsg = MVCHelper.GetValidMsg(ModelState)
                });
            }
            if (model.Code != (string)TempData["verifyCode"])
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误！" });
            }
            if (adminUserService.CheckLogin(model.Name,model.PassWord))
            {
                Session["LoginAdminId"] = adminUserService.GetByName(model.Name).Id;
                return Json(new AjaxResult { Status = "ok" });

            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "账号密码错误！" });
                

            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();//销毁Session
            //return Redirect("~/Main/Login");
            return RedirectToAction("Login");
        }
        public ActionResult CreateVerifyCode()
        {
            string verifyCode = CommonHelper.CreateVerifyCode(4);
            TempData["verifyCode"] = verifyCode;
            //Session["verifyCode"] = verifyCode;
            /*
            using (MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 60, 100, 20, 6))
            {
                return File(ms, "image/jpeg");
            }*/
            MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 60, 100, 20, 6);
            return File(ms, "image/jpeg");
        }
    }

}