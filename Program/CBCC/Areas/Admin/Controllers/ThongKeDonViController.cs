using System;
using System.Collections.Generic;
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
            ViewBag.MaDonVi = (ViewBag.DonVi as List<DonVi>) != null ? (ViewBag.DonVi as List<DonVi>)[0].MaDonVi : string.Empty;
        }
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");
            ThongKe thongke;
            thongke = ThongKeService.ThongKeToanTP_DonVi_ByTime(ViewBag.TuNgay, ViewBag.DenNgay, ViewBag.MaDonVi);
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
            ViewBag.HaiLong = thongke.HaiLong;
            ViewBag.KhongHaiLong = thongke.KhongHaiLong;
            ViewBag.BinhThuong = thongke.BinhThuong;
            ViewBag.SCBinhThuong = thongke.SCBinhThuong;
            ViewBag.SCHaiLong = thongke.SCHaiLong;
            ViewBag.SCKhongHaiLong = thongke.SCKhongHaiLong;

            // ban bieu
            ViewBag.BanBieu = ThongKeService.ThongKeToanTP_DonVi_ByDonVi_ByTime(ViewBag.TuNgay, ViewBag.DenNgay, ViewBag.MaDonVi);
            return View();
        }
        [HttpPost]
        public ActionResult Index(string cbDonVi, string tuNgay, string denNgay)
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
                thongke.SCBinhThuong = "0/0";
                thongke.SCHaiLong = "0/0";
                thongke.SCKhongHaiLong = "0/0";
            }
            ViewBag.TuNgay = tuNgay;
            ViewBag.DenNgay = denNgay;
            ViewBag.MaDonVi = cbDonVi;
            ViewBag.HaiLong = thongke.HaiLong;
            ViewBag.KhongHaiLong = thongke.KhongHaiLong;
            ViewBag.BinhThuong = thongke.BinhThuong;
            ViewBag.SCBinhThuong = thongke.SCBinhThuong;
            ViewBag.SCHaiLong = thongke.SCHaiLong;
            ViewBag.SCKhongHaiLong = thongke.SCKhongHaiLong;
            // ban bieu
            ViewBag.BanBieu = ThongKeService.ThongKeToanTP_DonVi_ByDonVi_ByTime(tuNgay, denNgay, cbDonVi);
            #endregion
            return View();
        }
    }
}
