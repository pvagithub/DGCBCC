using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;
namespace CBCC.Areas.Admin.Controllers
{
    public class ThongKeNhomTieuChiController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");
            List<ThongKe> result = ThongKeService.ThongKeNhomTieuChiTP_ByTime(ViewBag.TuNgay, ViewBag.DenNgay);
            ViewBag.NhomTieuChiTP = result;
            ViewBag.NhomTieuChi = new List<ThongKe>();
            return View();
        }
        [HttpPost]
        public ActionResult Index(string tuNgay, string denNgay)
        {
            tuNgay = string.IsNullOrWhiteSpace(tuNgay) ? DateTime.Now.AddMonths(-1).ToString("dd/MM/yyy") : tuNgay;
            denNgay = string.IsNullOrWhiteSpace(denNgay) ? DateTime.Now.ToString("dd/MM/yyy") : denNgay;
            #region thong ke toan tinh
            List<ThongKe> result = ThongKeService.ThongKeNhomTieuChiTP_ByTime(tuNgay, denNgay);
            ViewBag.TuNgay = tuNgay;
            ViewBag.DenNgay = denNgay;
            ViewBag.NhomTieuChiTP = result;
            // ban bieu
            ViewBag.NhomTieuChi = ThongKeService.ThongKeNhomTieuChiDonVi_ByTime(tuNgay, denNgay, 0);
            #endregion
            return View();
        }
    }
}
