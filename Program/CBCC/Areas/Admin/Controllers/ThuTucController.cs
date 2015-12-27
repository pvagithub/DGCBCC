using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using CBCC.Models;
using WebMVC.Framework.Utilities;
namespace CBCC.Areas.Admin.Controllers
{
    public class ThuTucController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TenThuTucSortParm = String.IsNullOrEmpty(sortOrder) ? "TenThuTuc_desc" : "";
            var thuTuc = DanhMucService.ThuTucGetAllList();

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
                thuTuc = thuTuc.Where(s => s.TenThucTuc.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "TenThucTuc":
                    thuTuc = thuTuc.OrderBy(s => s.TenThucTuc).ToList();
                    break;

                case "TenThucTuc_desc":
                    thuTuc = thuTuc.OrderByDescending(s => s.TenThucTuc).ToList();
                    break;

                default:
                    thuTuc = thuTuc.OrderBy(s => s.ThuTucID).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.Page = (pageNumber - 1) * pageSize;
            return View(thuTuc.ToPagedList(pageNumber, pageSize));
        }
        // GET: /Admin/DonVi/Create
        public ActionResult Create()
        {
            var item = new ThuTucModel();
            return PartialView("_Create", item);
        }

        //
        // POST: /Admin/DonVi/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ThuTucModel thuTuc)
        {
            if (ModelState.IsValid)
            {
                var item = new ThuTuc();
                item.MaThuTuc = thuTuc.MaThuTuc;
                item.TenThucTuc = thuTuc.TenThucTuc;
                item.Description = thuTuc.Description;
                item.LinhVucID = thuTuc.LinhVucID;
                item.Active = true;
                int id = DanhMucService.ThuTucCreate(item);
                return RedirectToAction("Index");
            }
            else
                return PartialView("_Create", thuTuc);
        }
        public ActionResult Edit(int id)
        {
            var item = DanhMucService.ThuTucGetById(id);
            ThuTucModel model = new ThuTucModel();
            model.Active = item.Active;
            model.Description = item.Description;
            model.DisplayOrder = item.DisplayOrder;
            model.Image = item.Image;
            model.LinhVucID = item.LinhVucID;
            model.MaThuTuc = item.MaThuTuc;
            model.TenThucTuc = item.TenThucTuc;
            model.ThuTucID = item.ThuTucID;
            return PartialView("_Edit", model);
        }
        [HttpPost]
        public ActionResult Edit(ThuTucModel thuTuc)
        {
            if (ModelState.IsValid)
            {
                var item = new ThuTuc();
                item.ThuTucID = thuTuc.ThuTucID;
                item.MaThuTuc = thuTuc.MaThuTuc;
                item.TenThucTuc = thuTuc.TenThucTuc;
                item.Description = thuTuc.Description;
                item.LinhVucID = thuTuc.LinhVucID;
                DanhMucService.ThuTucUpdate(thuTuc);
                return RedirectToAction("Index");
            }
            else
                return PartialView("_Edit", thuTuc);
        }
        public ActionResult Delete(int id = 0)
        {
            var item = DanhMucService.ThuTucGetById(id);
            ThuTucModel model = new ThuTucModel();
            model.Active = item.Active;
            model.Description = item.Description;
            model.DisplayOrder = item.DisplayOrder;
            model.Image = item.Image;
            model.LinhVucID = item.LinhVucID;
            model.MaThuTuc = item.MaThuTuc;
            model.TenThucTuc = item.TenThucTuc;
            model.ThuTucID = item.ThuTucID;
            if (item == null) HttpNotFound();
            return PartialView("_Delete", model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            DanhMucService.ThuTucDelete(id);
            return RedirectToAction("Index");
        }
    }
}
