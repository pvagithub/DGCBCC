﻿using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;

namespace CBCC.Areas.Admin.Controllers
{
    public class SoftwareFeedbackController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.GopYPhanMem = GopYService.GetGopY();
        }
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin,Manage")]
        // GET: Admin/SoftwareFeedback
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
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