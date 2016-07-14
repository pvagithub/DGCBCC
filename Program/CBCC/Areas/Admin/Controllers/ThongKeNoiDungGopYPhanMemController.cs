using System;
using System.Web.Mvc;
using WebMVC.Bussiness;
namespace CBCC.Areas.Admin.Controllers
{
    public class ThongKeNoiDungGopYPhanMemController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin,Manage")]
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");
            // ban bieu
            return View(ThongKeService.ThongKeNoiDungGopYPhanMem(ViewBag.TuNgay, ViewBag.DenNgay));
        }
        [HttpPost]
        public ActionResult Index(string tuNgay, string denNgay)
        {
            tuNgay = string.IsNullOrWhiteSpace(tuNgay) ? DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy") : tuNgay;
            denNgay = string.IsNullOrWhiteSpace(denNgay) ? DateTime.Now.ToString("dd/MM/yyyy") : denNgay;

            return View(ThongKeService.ThongKeNoiDungGopYPhanMem(tuNgay, denNgay));
        }
    }
}
