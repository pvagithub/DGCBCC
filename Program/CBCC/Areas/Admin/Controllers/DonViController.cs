using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;

namespace CBCC.Areas.Admin.Controllers
{
    public class DonViController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                ViewBag.LoaiDonVi = DanhMucService.DonViGetAllList().Where(p => p.ParentDonViID == null).ToList();
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TenDonViSortParm = String.IsNullOrEmpty(sortOrder) ? "TenDonVi_desc" : "";
            var donVi = DanhMucService.DonViGetAllList();

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
                donVi = donVi.Where(s => s.MaDonVi.Contains(searchString) || s.TenDonVi.Contains(searchString) || s.MoTa.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "TenDonVi":
                    donVi = donVi.OrderBy(s => s.TenDonVi).ToList();
                    break;

                case "TenDonVi_desc":
                    donVi = donVi.OrderByDescending(s => s.TenDonVi).ToList();
                    break;

                default:
                    donVi = donVi.OrderBy(s => s.TenDonVi).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.Page = (pageNumber - 1) * pageSize;
            return View(donVi.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Admin/DonVi/Create
        public ActionResult Create()
        {
            var item = new DonVi();
            return PartialView("_Create", item);
        }

        //
        // POST: /Admin/DonVi/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DonVi donVi)
        {
            if (ModelState.IsValid)
            {
                var item = new DonVi();
                item.TenDonVi = donVi.TenDonVi;
                item.MoTa = donVi.MoTa;
                item.MaDonVi = donVi.MaDonVi;
                item.ParentDonViID = donVi.ParentDonViID;
                item.Active = true;
                int id = DanhMucService.DonViCreate(item);
                return RedirectToAction("Index");
            }
            else
                return PartialView("_Create", donVi);
        }
        public ActionResult Edit(int id)
        {
            DonVi donVi = new DonVi();
            var item = DanhMucService.DonViGetById(id);
            return PartialView("_Edit", item);
        }
        [HttpPost]
        public ActionResult Edit(DonVi donVi)
        {
            if (ModelState.IsValid)
            {
                DanhMucService.DonViUpdate(donVi);
                return RedirectToAction("Index");
            }
            else
                return PartialView("_Edit", donVi);
        }
        public ActionResult Delete(int id = 0)
        {
            var item = DanhMucService.DonViGetById(id);
            if (item == null) HttpNotFound();
            return PartialView("_Delete", item);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            DanhMucService.DonViDelete(id);
            return RedirectToAction("Index");
        }
    }
}