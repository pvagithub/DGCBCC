using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Drawing;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;
namespace CBCC.Areas.Admin.Controllers
{
    public class ThongKeTyLePhanMemController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");
            ThongKe thongke;
            thongke = ThongKeService.ThongKeTyLeGopYPhanMem(ViewBag.TuNgay, ViewBag.DenNgay);
            
            double tmp;
            double.TryParse(thongke.ChapNhanDuoc, out tmp);
            ViewBag.ChapNhanDuoc = tmp;

            double.TryParse(thongke.KhoSuDung, out tmp);
            ViewBag.KhoSuDung = tmp;

            double.TryParse(thongke.Khac, out tmp);
            ViewBag.Khac = tmp;

            ViewBag.rowChapNhan = thongke.rowChapNhan;
            ViewBag.rowKhoSuDung = thongke.rowKhoSuDung;
            ViewBag.rowKhac = thongke.rowKhac;
            ViewBag.Total = thongke.Total;
            return View();
        }
        [HttpPost]
        public ActionResult Index(string tuNgay, string denNgay)
        {
            tuNgay = string.IsNullOrWhiteSpace(tuNgay) ? DateTime.Now.AddMonths(-1).ToString("dd/MM/yyy") : tuNgay;
            denNgay = string.IsNullOrWhiteSpace(denNgay) ? DateTime.Now.ToString("dd/MM/yyy") : denNgay;
            #region thong ke toan tinh
            ThongKe thongke;
            thongke = ThongKeService.ThongKeTyLeGopYPhanMem(tuNgay, denNgay);
            
            ViewBag.TuNgay = tuNgay;
            ViewBag.DenNgay = denNgay;

            double tmp;
            double.TryParse(thongke.ChapNhanDuoc, out tmp);
            ViewBag.ChapNhanDuoc = tmp;

            double.TryParse(thongke.KhoSuDung, out tmp);
            ViewBag.KhoSuDung = tmp;

            double.TryParse(thongke.Khac, out tmp);
            ViewBag.Khac = tmp;

            ViewBag.rowChapNhan = thongke.rowChapNhan;
            ViewBag.rowKhoSuDung = thongke.rowKhoSuDung;
            ViewBag.rowKhac = thongke.rowKhac;
            ViewBag.Total = thongke.Total;
            #endregion
            return View();
        }
    }
}
