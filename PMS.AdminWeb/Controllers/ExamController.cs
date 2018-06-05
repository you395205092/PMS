using PMS.Common;
using PMS.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.CommonMVC;
using PMS.DTO;
using System.IO;
using NPOI.SS.UserModel;
using PMS.AdminWeb.App_Start;
using PMS.AdminWeb.Models;
using System.IO.Compression;
using System.Data;

namespace PMS.AdminWeb.Controllers
{
    public class ExamController : Controller
    {
        public IExamService examService { get; set; }
        public IExamTypeService examTypeService { get; set; }
        public IAdminUserService adminUserService { get; set; }

        [CheckPermission("Exam.List")]
        public ActionResult List(long? TypeId, int? IsLook, string keywords, int pageIndex = 1)
        {

            SearchOpt opt = new SearchOpt();
            opt.CurrentIndex = pageIndex;
            opt.TypeId = TypeId;
            opt.Keywords = keywords;
            opt.IsLook = IsLook;
            opt.PageSize = 10;
            ViewBag.pageIndex = pageIndex;
            ViewBag.IsLook = IsLook;
            ViewBag.TypeId = TypeId;
            ViewBag.keywords = keywords;

            var data = examService.Search(opt);
            return View(data);
        }
        [CheckPermission("Exam.Add")]
        [HttpGet]
        public ActionResult Add()
        {
            var data = examTypeService.GetAll();
            return View(data);
        }
        [CheckPermission("Exam.Add")]
        [HttpPost]
        public ActionResult Add(ExamDTO dto)
        {
            var data = examService.Add(dto);
            if (data > 0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "添加失败！" });
            }
        }
        [CheckPermission("Exam.Edit")]
        [HttpGet]
        public ActionResult Edit(long Id)
        {

            ExamEditModel model = new ExamEditModel();
            model.exam = examService.GetById(Id);
            model.examTypes = examTypeService.GetAll();
            return View(model);
        }
        [CheckPermission("Exam.Edit")]
        [HttpPost]
        public ActionResult Edit(ExamDTO dto)
        {
            var data = examService.Edit(dto);
            if (data > 0)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "添加失败！" });
            }

        }


        [CheckPermission("Exam.Export")]
        [HttpGet]
        public ActionResult Export()
        {
            var data = examTypeService.GetAll();
            return View(data);
        }

        [CheckPermission("Exam.Export")]
        [HttpPost]
        public ActionResult Export(HttpPostedFileBase fileData, string TypeId)
        {
            if (fileData != null)
            {
                try
                {
                    string filePath = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名


                    string saveName = CommonHelper.CalcMD5(fileData.InputStream) + CommonHelper.CreateVerifyCode(5) + fileExtension; // 保存文件名称

                    fileData.SaveAs(filePath + saveName);

                    IWorkbook wb1 = WorkbookFactory.Create(filePath + saveName);
                    ISheet sheel = wb1.GetSheetAt(0);
                    ExamDTO dto = new ExamDTO();

                    for (int i = 1; i <= sheel.LastRowNum; i++)
                    {
                        IRow row = sheel.GetRow(i);
                        ICell StuName = row.GetCell(0);
                        ICell SysName = row.GetCell(1);
                        ICell MajorName = row.GetCell(2);
                        ICell StuId = row.GetCell(3);
                        ICell SFZCode = row.GetCell(4);
                        ICell ClassName = row.GetCell(5);
                        ICell ZKZCode = row.GetCell(6);
                        ICell SchoolName = row.GetCell(7);
                        ICell PlaceNum = row.GetCell(8);
                        ICell Address = row.GetCell(9);
                        ICell ExamTime = row.GetCell(10);
                        ICell LLllzwh = row.GetCell(11);
                        ICell LLExamAddress = row.GetCell(12);
                        ICell LLExamTime = row.GetCell(13);
                        ICell LLExamPlaceNum = row.GetCell(14);


                        dto.StuName = StuName.ToString();
                        dto.SysName = SysName.ToString();
                        dto.MajorName = MajorName.ToString();
                        dto.StuId = StuId.ToString();
                        dto.SFZCode = SFZCode.ToString();
                        dto.ClassName = ClassName.ToString();
                        dto.ZKZCode = ZKZCode.ToString();
                        dto.SchoolName = SchoolName.ToString();
                        dto.PlaceNum = PlaceNum.ToString();
                        dto.Address = Address.ToString();
                        dto.ExamTime = ExamTime.ToString();
                        dto.LLExamAddress = LLExamAddress.ToString();
                        dto.LLExamTime = LLExamTime.ToString();
                        dto.LLExamPlaceNum = LLExamPlaceNum.ToString();
                        dto.TypeId = Convert.ToInt64(TypeId);
                        dto.llzwh = LLllzwh.ToString();

                        examService.Add(dto);
                    }
                    return Content("<script>alert('导入成功!');parent.location.reload()</script>");
                }
                catch (Exception ex)
                {
                    return Content("<script>alert('导入失败!" + ex.Message + "');parent.location.reload()</script>");
                    //return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
            }
            else
            {
                return Content("<script>alert('请选择要上传的文件！');parent.location.reload()</script>");
            }
        }

        [CheckPermission("Exam.PicExport")]
        [HttpGet]
        public ActionResult PicExport()
        {
            return View();
        }

        [CheckPermission("Exam.PicExport")]
        [HttpPost]
        public ActionResult PicExport(HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                try
                {
                    string filePath = Server.MapPath("~/Uploads/Pic/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName = CommonHelper.CalcMD5(fileData.InputStream) + CommonHelper.CreateVerifyCode(5) + fileExtension; // 保存文件名称

                    fileData.SaveAs(filePath + saveName);
                    System.IO.Compression.ZipFile.ExtractToDirectory(filePath + saveName, filePath); //解压

                    return Content("<script>alert('导入成功!');parent.location.reload();</script>");
                }
                catch (Exception ex)
                {
                    return Content("<script>alert('导入失败!" + ex.Message + "';parent.location.reload();)</script>");
                    //return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
            }
            else
            {
                return Content("<script>alert('请选择要上传的文件！');parent.location.reload()</script>");
            }
        }

        [CheckPermission("Exam.UpdateFind")]
        [HttpGet]
        public ActionResult UpdateFind()
        {

            var data = examTypeService.GetAll();
            return View(data);
        }
        [CheckPermission("Exam.UpdateFind")]
        [HttpPost]
        public ActionResult UpdateFind(long TypeId)
        {
            examService.UpdateAllNoLook(TypeId);
            return Content("<script>alert('已经批量更新为未查询！');parent.location.reload()</script>");
        }
        [CheckPermission("Exam.DownLoad")]
        public ActionResult DownLoad()
        {
            var typeids = examTypeService.GetAll().Select(e => e.Id);

            var list = examService.GetAll().Where(m => typeids.Contains(m.TypeId)).Where(p => p.IsLook == false).ToArray();



            string filePath = Server.MapPath("/Uploads/未查询名单.xls");

            MemoryStream ms = RenderDataTableToExcel(list) as MemoryStream;
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
            //data = null;
            ms = null;
            fs = null;


            return File(filePath, "Application/excel", "未查询名单.xls");


        }
        public static Stream RenderDataTableToExcel(ExamDTO[] SourceTable)
        {

            MemoryStream ms = new MemoryStream();
            NPOI.HSSF.UserModel.HSSFWorkbook workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet();
            NPOI.SS.UserModel.IRow headerRow = sheet.CreateRow(0);

            //sheet.DefaultColumnWidth = 3500;
            //sheet.DefaultRowHeight = 2000;
            //设置7列宽为100
            //加标题

            headerRow.CreateCell(0).SetCellValue("姓名");
            headerRow.CreateCell(1).SetCellValue("学院");
            headerRow.CreateCell(2).SetCellValue("专业");
            headerRow.CreateCell(3).SetCellValue("学号");
            headerRow.CreateCell(4).SetCellValue("身份证号");
            headerRow.CreateCell(5).SetCellValue("班级");
            headerRow.CreateCell(6).SetCellValue("准考证号");
            headerRow.CreateCell(7).SetCellValue("校区名称");
            headerRow.CreateCell(8).SetCellValue("考场号");
            headerRow.CreateCell(9).SetCellValue("理论考试地点");
            headerRow.CreateCell(10).SetCellValue("理论考试时间");
            headerRow.CreateCell(11).SetCellValue("理论考试座位号");
            headerRow.CreateCell(12).SetCellValue("上机考试地点");
            headerRow.CreateCell(13).SetCellValue("上机考试时间");
            headerRow.CreateCell(14).SetCellValue("上机考试座位号");
            headerRow.CreateCell(15).SetCellValue("考试类别");


            int rowIndex = 1;
            foreach (var item in SourceTable)
            {
                NPOI.SS.UserModel.IRow dataRow = sheet.CreateRow(rowIndex);
                //列高50

                dataRow.CreateCell(0).SetCellValue(item.StuName);
                dataRow.CreateCell(1).SetCellValue(item.SysName);
                dataRow.CreateCell(2).SetCellValue(item.MajorName);
                dataRow.CreateCell(3).SetCellValue(item.StuId);
                dataRow.CreateCell(4).SetCellValue(item.SFZCode);
                dataRow.CreateCell(5).SetCellValue(item.ClassName);
                dataRow.CreateCell(6).SetCellValue(item.ZKZCode);
                dataRow.CreateCell(7).SetCellValue(item.SchoolName);
                dataRow.CreateCell(8).SetCellValue(item.PlaceNum);
                dataRow.CreateCell(9).SetCellValue(item.Address);
                dataRow.CreateCell(10).SetCellValue(item.ExamTime);
                dataRow.CreateCell(11).SetCellValue(item.llzwh);
                dataRow.CreateCell(12).SetCellValue(item.LLExamAddress);
                dataRow.CreateCell(13).SetCellValue(item.LLExamTime);
                dataRow.CreateCell(14).SetCellValue(item.LLExamPlaceNum);
                dataRow.CreateCell(15).SetCellValue(item.TypeName);

                rowIndex++;
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            headerRow = null;
            workbook = null;
            return ms;
        }
        [CheckPermission("Exam.Delete")]
        public ActionResult Delete(long Id)
        {
            examService.Deleted(Id);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("Exam.Delete")]
        public ActionResult DeleteAll(long[] Ids)
        {
            foreach (var item in Ids)
            {
                examService.Deleted(item);

            }
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("Exam.UpdatePic")]
        [HttpGet]
        public ActionResult UpdatePic()
        {
            return View();  
        }
        
        [CheckPermission("Exam.UpdatePic")]
        [HttpPost]
        public ActionResult UpdatePic(HttpPostedFileBase file)
        {
            if (file != null)
            {
                try
                {
                    string filePath = Server.MapPath("~/Uploads/Pic/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(file.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    if (Path.GetExtension(fileName) != ".jpg")
                    {
   
                        return Json(new AjaxResult { Status = "error", ErrorMsg = "图片格式不正确!" });
                    }
                    file.SaveAs(filePath + fileName);
                    //return Content("<script>alert('上传图片成功!');parent.location.reload();</script>");
                    return Json(new AjaxResult { Status = "ok", ErrorMsg = "上传图片成功" });
                }
                catch (Exception ex)
                {
                    //return Content("<script>alert('上传图片失败!" + ex.Message + "';parent.location.reload();)</script>");
                    return Json(new AjaxResult { Status = "error", ErrorMsg = "上传图片失败"+ ex.Message });
                }
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "请选择要上传的图片" });
            }

        }
    }

}