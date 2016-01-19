using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Web;
using System.Web.Mvc;
using WebMVC.Entities;
using WebMVC.Bussiness;

namespace CBCC.Areas.Admin.Controllers
{
    public class SoftWareViewModel
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }
    public class SoftwareFeedbackController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.GopYPhanMem = GopYService.GetGopY();
        }
        //[MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]
        // GET: Admin/SoftwareFeedback
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var lstAnswers = GopYService.GetGopYAnswers();
            var lstQuestions = GopYService.GetGopYQuestions();
            foreach (var item in lstQuestions)
            {
                var arrInt = item.IDAnswers.Split(',');
                for (int i = 0; i < arrInt.Length; i++)
                {
                    var ans =lstAnswers.Where(p=>p.ID=======)
                }
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.TenTieuChiSortParm = String.IsNullOrEmpty(sortOrder) ? "TenTieuChi_desc" : "";
            var tkgopy = ThongKeService.ThongKeGopYPhanMem();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                tkgopy = tkgopy.Where(s => s.Question.Contains(searchString)).ToList();
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.Page = (pageNumber - 1) * pageSize;
            return View(tkgopy.ToPagedList(pageNumber, pageSize));
        }
    }
}