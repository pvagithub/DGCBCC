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
    public class ThongKeDonViController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.DonVi = DanhMucService.DonViGetAllList();
        }
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");
            ThongKe thongke;
            thongke = ThongKeService.ThongKeToanTP_DonVi_ByTime(ViewBag.TuNgay, ViewBag.DenNgay, 1);
            if (thongke == null || thongke.HaiLong == null)
            {
                thongke = new ThongKe();
                thongke.HaiLong = 100;
                thongke.KhongHaiLong = 0;
                thongke.BinhThuong = 0;
            }
            ViewBag.HaiLong = thongke.HaiLong;
            ViewBag.KhongHaiLong = thongke.KhongHaiLong;
            ViewBag.BinhThuong = thongke.BinhThuong;
            return View();
        }
        [HttpPost]
        public ActionResult Index(int cbDonVi, string tuNgay, string denNgay)
        {
            tuNgay = string.IsNullOrWhiteSpace(tuNgay) ? DateTime.Now.AddMonths(-1).ToString("dd/MM/yyy") : tuNgay;
            denNgay = string.IsNullOrWhiteSpace(denNgay) ? DateTime.Now.ToString("dd/MM/yyy") : denNgay;
            #region thong ke toan tinh
            ThongKe thongke;
            thongke = ThongKeService.ThongKeToanTP_DonVi_ByTime(tuNgay, denNgay, cbDonVi);
            if (thongke == null || thongke.HaiLong == null)
            {
                thongke = new ThongKe();
                thongke.HaiLong = 100;
                thongke.KhongHaiLong = 0;
                thongke.BinhThuong = 0;
            }
            ViewBag.TuNgay = tuNgay;
            ViewBag.DenNgay = denNgay;
            ViewBag.DonViID = cbDonVi;
            ViewBag.HaiLong = thongke.HaiLong;
            ViewBag.KhongHaiLong = thongke.KhongHaiLong;
            ViewBag.BinhThuong = thongke.BinhThuong;

            // ban bieu
            ViewBag.BanBieu = ThongKeService.ThongKeToanTP_DonVi_ByDonVi_ByTime(tuNgay, denNgay, cbDonVi);
            #endregion
            return View();
        }
    }
}
