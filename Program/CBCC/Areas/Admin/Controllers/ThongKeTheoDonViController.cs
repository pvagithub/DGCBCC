using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;
using System.Linq;

namespace CBCC.Areas.Admin.Controllers
{
    public class ThongKeTheoDonViController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "User")]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.DonVi = DanhMucService.DonViGetAllList().Where(x => x.Active == true).ToList();           
            ViewBag.MaDonVi = (ViewBag.DonVi as List<DonVi>) != null ? (ViewBag.DonVi as List<DonVi>)[0].MaDonVi : string.Empty;
           
        }
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");
            ThongKe thongke;
            string username = HttpContext.User.Identity.Name;
            thongke = ThongKeService.ThongKe_TheoDonVi_ByTime(ViewBag.TuNgay, ViewBag.DenNgay, username);
          
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
            ViewBag.BanBieu = ThongKeService.ThongKe_TheoDonVi_ByDonVi_ByTime(ViewBag.TuNgay, ViewBag.DenNgay, username);
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username, string tuNgay, string denNgay)
        {
            tuNgay = string.IsNullOrWhiteSpace(tuNgay) ? DateTime.Now.AddMonths(-1).ToString("dd/MM/yyy") : tuNgay;
            denNgay = string.IsNullOrWhiteSpace(denNgay) ? DateTime.Now.ToString("dd/MM/yyy") : denNgay;
            #region thong ke toan tinh
            ThongKe thongke;
            string suser = HttpContext.User.Identity.Name;
            thongke = ThongKeService.ThongKe_TheoDonVi_ByTime(tuNgay, denNgay, suser);
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
           // ViewBag.MaDonVi = cbDonVi;
            ViewBag.HaiLong = thongke.HaiLong;
            ViewBag.KhongHaiLong = thongke.KhongHaiLong;
            ViewBag.BinhThuong = thongke.BinhThuong;
            ViewBag.SCBinhThuong = thongke.SCBinhThuong;
            ViewBag.SCHaiLong = thongke.SCHaiLong;
            ViewBag.SCKhongHaiLong = thongke.SCKhongHaiLong;
            // ban bieu
            ViewBag.BanBieu = ThongKeService.ThongKe_TheoDonVi_ByDonVi_ByTime(tuNgay, denNgay, suser);
            #endregion
            return View();
        }
    }
	}
