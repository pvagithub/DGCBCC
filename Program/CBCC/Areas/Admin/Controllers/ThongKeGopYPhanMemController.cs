using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;

namespace CBCC.Areas.Admin.Controllers
{
    public class ThongKeGopYPhanMemController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin,Manage")]
        //
        // GET: /Admin/CaiDat/
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");
            ViewBag.Result = ThongKeService.ThongKeNoiDungGopYPhanMemResult(ViewBag.TuNgay, ViewBag.DenNgay);
            ViewBag.Result2 = ThongKeService.ThongKeNoiDungGopYPhanMem(ViewBag.TuNgay, ViewBag.DenNgay);
            return View();
        }
        [HttpPost]
        public ActionResult Index(string tuNgay, string denNgay)
        {
            tuNgay = string.IsNullOrWhiteSpace(tuNgay) ? DateTime.Now.AddMonths(-1).ToString("dd/MM/yyy") : tuNgay;
            denNgay = string.IsNullOrWhiteSpace(denNgay) ? DateTime.Now.ToString("dd/MM/yyy") : denNgay;
            ViewBag.Result = ThongKeService.ThongKeNoiDungGopYPhanMemResult(ViewBag.TuNgay, ViewBag.DenNgay);
            ViewBag.Result2 = ThongKeService.ThongKeNoiDungGopYPhanMem(ViewBag.TuNgay, ViewBag.DenNgay);
            return View();
        }
    }
}