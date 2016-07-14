using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;

namespace CBCC.Areas.Admin.Controllers
{
    public class SoLuotDanhGiaController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin,Manage,User")]
        //
        // GET: /Admin/CaiDat/
        public ActionResult Index(string tuNgay = "", string denNgay = "", bool back = false)
        {
            ViewBag.TuNgay = string.IsNullOrWhiteSpace(tuNgay) ? (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy") : tuNgay;
            ViewBag.DenNgay = string.IsNullOrWhiteSpace(denNgay) ? (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy") : denNgay;
            var thongke = ThongKeService.ThongKeSoLuotDanhGia(ViewBag.TuNgay, ViewBag.DenNgay, User.Identity.Name);
            ViewBag.Result = thongke;
            return View();
        }
        [HttpPost]
        public ActionResult Index(string tuNgay, string denNgay)
        {
            ViewBag.TuNgay = string.IsNullOrWhiteSpace(tuNgay) ? (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy") : tuNgay;
            ViewBag.DenNgay = string.IsNullOrWhiteSpace(denNgay) ? (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy") : denNgay;
            var thongke = ThongKeService.ThongKeSoLuotDanhGia(ViewBag.TuNgay, ViewBag.DenNgay, User.Identity.Name);
            ViewBag.Result = thongke;
            return View();
        }
        public ActionResult ThongKeSoLuotDanhGiaDonVi(string tuNgay, string denNgay, string madonvi, string malienthong)
        {
            ViewBag.TuNgay = string.IsNullOrWhiteSpace(tuNgay) ? (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy") : tuNgay;
            ViewBag.DenNgay = string.IsNullOrWhiteSpace(denNgay) ? (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy") : denNgay;
            var thongke = ThongKeService.ThongKeSoLuotDanhGia_By_DonVi(ViewBag.TuNgay, ViewBag.DenNgay, madonvi, malienthong, User.Identity.Name);
            ViewBag.Result = thongke;
            return View();
        }
        public ActionResult KetQuaDanhGiaHosoByDanhGiaID(string danhGiaID)
        {
            ViewBag.HoSo = ThongKeService.KetQuaDanhGiaThongTinHoSoByDanhGiaID(danhGiaID, string.Empty);
            ViewBag.Result = ThongKeService.KetQuaDanhGiaHosoByDanhGiaID(danhGiaID);
            return View();
        }
    }
}