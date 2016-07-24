using System;
using System.Web.Mvc;
using WebMVC.Bussiness;
namespace CBCC.Areas.Admin.Controllers
{
    public class ThongKeThreeMonthDonViController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "User")]
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-2).Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");

            ViewBag.ThreeMonth = ThongKeService.ThongKeToanTP_BanBieu_ThreeMonthDonVi(ViewBag.TuNgay, ViewBag.DenNgay, User.Identity.Name);
            ViewBag.BieuDo = ThongKeService.ThongKeToanTP_3Thang_BieuDoCot_DonVi(ViewBag.TuNgay, ViewBag.DenNgay, User.Identity.Name);
            return View();
        }
        [HttpPost]
        public ActionResult Index(string id)
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-2).Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");

            ViewBag.ThreeMonth = ThongKeService.ThongKeToanTP_BanBieu_ThreeMonthDonVi(ViewBag.TuNgay, ViewBag.DenNgay, User.Identity.Name);
            ViewBag.BieuDo = ThongKeService.ThongKeToanTP_3Thang_BieuDoCot_DonVi(ViewBag.TuNgay, ViewBag.DenNgay, User.Identity.Name);
            return View();
        }
        public ActionResult ThongKeToanTP_BanBieu_ByDonViMonthYear(string madonvi, int month, int year)
        {
            var result = ThongKeService.ThongKeToanTP_BanBieu_ByDonViMonthYear(madonvi, month, year);
            return View(result);
        }
    }
}
