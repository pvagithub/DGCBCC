using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;
namespace CBCC.Areas.Admin.Controllers
{
    public class ThongKeGopYDonViController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "User")]
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");

            ViewBag.NhomTieuChiTP = ThongKeService.ThongKeGopYTungDonVi(ViewBag.TuNgay, ViewBag.DenNgay, User.Identity.Name) as List<ThongKe>;
            
            return View();
        }
        [HttpPost]
        public ActionResult Index(string tuNgay, string denNgay)
        {
            tuNgay = string.IsNullOrWhiteSpace(tuNgay) ? DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy") : tuNgay;
            denNgay = string.IsNullOrWhiteSpace(denNgay) ? DateTime.Now.ToString("dd/MM/yyyy") : denNgay;
            #region thong ke toan tinh
            ViewBag.TuNgay = tuNgay;
            ViewBag.DenNgay = denNgay;
            ViewBag.NhomTieuChiTP = ThongKeService.ThongKeGopYTungDonVi(ViewBag.TuNgay, ViewBag.DenNgay, User.Identity.Name) as List<ThongKe>;
            // ban bieu
            #endregion
            
            return View();
            
        }
    }
}
