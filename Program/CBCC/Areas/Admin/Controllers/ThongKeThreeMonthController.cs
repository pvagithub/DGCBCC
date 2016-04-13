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
    public class ThongKeThreeMonthController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-3).Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");

            ViewBag.ThreeMonth = ThongKeService.ThongKeToanTP_BanBieu_ThreeMonth(ViewBag.TuNgay, ViewBag.DenNgay);
            return View();
        }
        [HttpPost]
        public ActionResult Index(string id)
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-3).Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");

            ViewBag.ThreeMonth = ThongKeService.ThongKeToanTP_BanBieu_ThreeMonth(ViewBag.TuNgay, ViewBag.DenNgay);
            return View();
        }
        public ActionResult GetDonViMonthYear(int month, int year)
        {
            var result = ThongKeService.ThongKeToanTP_BanBieu_ByMonthYear(month, year);
            ViewBag.Month = month;
            ViewBag.Year = year;
            return View(result);
        }
        public ActionResult ThongKeToanTP_BanBieu_ByDonViMonthYear(string madonvi, int month, int year)
        {
            var result = ThongKeService.ThongKeToanTP_BanBieu_ByDonViMonthYear(madonvi, month, year);
            return View(result);
        }
    }
}
