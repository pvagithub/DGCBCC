using Newtonsoft.Json;
using System;
using PagedList;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;
using WebMVC.Framework.Utilities;

namespace CBCC.Areas.Admin.Controllers
{
    public class ManageFeedbackController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]
        //
        // GET: /Admin/CaiDat/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TenTieuChiSortParm = String.IsNullOrEmpty(sortOrder) ? "TenTieuChi_desc" : "";
            var tkgopy = ThongKeService.ThongKeGopYCauHoi();
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
                tkgopy = tkgopy.Where(s => s.TenTieuChi.Contains(searchString)).ToList();
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.Page = (pageNumber - 1) * pageSize;
            return View(tkgopy.ToPagedList(pageNumber, pageSize));
        }
        
    }
}