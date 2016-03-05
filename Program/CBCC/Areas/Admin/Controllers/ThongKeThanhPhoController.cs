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
    public class ThongKeThanhPhoController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");
            ThongKe thongke;
            thongke = ThongKeService.ThongKeToanTP_ByTime(ViewBag.TuNgay, ViewBag.DenNgay);
            if (thongke == null || thongke.HaiLong == null)
            {
                thongke = new ThongKe();
                thongke.HaiLong = 85;
                thongke.KhongHaiLong = 5;
                thongke.BinhThuong = 10;
                thongke.SCBinhThuong = "0/0";
                thongke.SCHaiLong = "0/0";
                thongke.SCKhongHaiLong = "0/0";
            }
            ViewBag.HaiLong = thongke.HaiLong;
            ViewBag.KhongHaiLong = thongke.KhongHaiLong;
            ViewBag.BinhThuong = thongke.BinhThuong;
            ViewBag.SCBinhThuong = thongke.SCBinhThuong;
            ViewBag.SCHaiLong = thongke.SCHaiLong;
            ViewBag.SCKhongHaiLong = thongke.SCKhongHaiLong;

            // ban bieu
            ViewBag.BanBieu = ThongKeService.ThongKeToanTP_BanBieu_ByTime(ViewBag.TuNgay, ViewBag.DenNgay);
            ViewBag.CaoNhat = ThongKeService.ThongKeToanTP_DonVi_ByTime_10Top(ViewBag.TuNgay, ViewBag.DenNgay);
            ViewBag.ThapNhat = ThongKeService.ThongKeToanTP_DonVi_ByTime_10Bottom(ViewBag.TuNgay, ViewBag.DenNgay);
            return View();
        }
        [HttpPost]
        public ActionResult Index(string tuNgay, string denNgay)
        {
            tuNgay = string.IsNullOrWhiteSpace(tuNgay) ? DateTime.Now.AddMonths(-1).ToString("dd/MM/yyy") : tuNgay;
            denNgay = string.IsNullOrWhiteSpace(denNgay) ? DateTime.Now.ToString("dd/MM/yyy") : denNgay;
            #region thong ke toan tinh
            ThongKe thongke;
            thongke = ThongKeService.ThongKeToanTP_ByTime(tuNgay, denNgay);
            if (thongke == null || thongke.HaiLong == null)
            {
                thongke = new ThongKe();
                thongke.HaiLong = 100;
                thongke.KhongHaiLong = 0;
                thongke.BinhThuong = 0;
                thongke.SCBinhThuong = "0/0";
                thongke.SCHaiLong = "0/0";
                thongke.SCKhongHaiLong = "0/0";
            }
            ViewBag.TuNgay = tuNgay;
            ViewBag.DenNgay = denNgay;
            ViewBag.HaiLong = thongke.HaiLong;
            ViewBag.KhongHaiLong = thongke.KhongHaiLong;
            ViewBag.BinhThuong = thongke.BinhThuong;
            ViewBag.SCBinhThuong = thongke.SCBinhThuong;
            ViewBag.SCHaiLong = thongke.SCHaiLong;
            ViewBag.SCKhongHaiLong = thongke.SCKhongHaiLong;

            // ban bieu
            ViewBag.BanBieu = ThongKeService.ThongKeToanTP_BanBieu_ByTime(tuNgay, denNgay);
            ViewBag.CaoNhat = ThongKeService.ThongKeToanTP_DonVi_ByTime_10Top(tuNgay, denNgay);
            ViewBag.ThapNhat = ThongKeService.ThongKeToanTP_DonVi_ByTime_10Bottom(tuNgay, denNgay);
            #endregion
            return View();
        }
    }
}
