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
    public class LinhVucController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TenLinhVucSortParm = String.IsNullOrEmpty(sortOrder) ? "TenLinhVuc_desc" : "";
            var linhVuc = DanhMucService.LinhVucGetAllList();

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
                linhVuc = linhVuc.Where(s => s.TenLinhVuc.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "TenLinhVuc":
                    linhVuc = linhVuc.OrderBy(s => s.TenLinhVuc).ToList();
                    break;

                case "TenLinhVuc_desc":
                    linhVuc = linhVuc.OrderByDescending(s => s.TenLinhVuc).ToList();
                    break;

                default:
                    linhVuc = linhVuc.OrderBy(s => s.LinhVucID).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.Page = (pageNumber - 1) * pageSize;
            ViewBag.LinhVucModel = new LinhVucModel();
            return View(linhVuc.ToPagedList(pageNumber, pageSize));
        }
        // GET: /Admin/DonVi/Create
        public ActionResult Create()
        {
            var item = new LinhVucModel();
            return PartialView("_Create", item);
        }

        //
        // POST: /Admin/DonVi/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(LinhVucModel donVi)
        {
            if (ModelState.IsValid)
            {
                var item = new LinhVuc();
                item.MaLinhVuc = donVi.MaLinhVuc;
                item.MoTa = donVi.MoTa;
                item.TenLinhVuc = donVi.TenLinhVuc;
                item.Active = true;
                int id = DanhMucService.LinhVucCreate(item);
                foreach (var it in donVi.ListDonViID)
                {
                    var tmp = new LinhVucDonVi();
                    int donvi = 0;
                    int.TryParse(it, out donvi);
                    tmp.Active = true;
                    tmp.LinhVucID = id;
                    tmp.DonViID = donvi;
                    DanhMucService.DonViLinhVucCreate(tmp);
                }
                return RedirectToAction("Index");
            }
            else
                return PartialView("_Create", donVi);
        }
        public ActionResult Edit(int id)
        {
            var item = DanhMucService.LinhVucGetById(id);
            var tmp = new LinhVucModel();
            tmp.LinhVucID = item.LinhVucID;
            tmp.MaLinhVuc = item.MaLinhVuc;
            tmp.TenLinhVuc = item.TenLinhVuc;
            tmp.MoTa = item.MoTa;
            tmp.DisplayOrder = item.DisplayOrder;
            tmp.Active = item.Active;

            List<string> dsDonViID = new List<string>();
            DanhMucService.DonViLinhVucGetByLinhVucId(item.LinhVucID, out dsDonViID);
            tmp.ListDonViID = dsDonViID;

            return PartialView("_Edit", tmp);
        }
        [HttpPost]
        public ActionResult Edit(LinhVucModel donVi)
        {
            if (ModelState.IsValid)
            {
                var item = new LinhVuc();
                item.LinhVucID = donVi.LinhVucID;
                item.MaLinhVuc = donVi.MaLinhVuc;
                item.MoTa = donVi.MoTa;
                item.TenLinhVuc = donVi.TenLinhVuc;
                DanhMucService.LinhVucUpdate(item);
                if (donVi.ListDonViID != null && donVi.ListDonViID.Count > 0)
                {
                    DanhMucService.DonViLinhVucDeleteByLinhVucID(item.LinhVucID);
                    DanhMucService.DonViLinhVucUpdateByLinhVucId(item.LinhVucID, donVi.ListDonViID);
                }

                return RedirectToAction("Index");
            }
            else
                return PartialView("_Edit", donVi);
        }
        public ActionResult Delete(int id = 0)
        {
            var item = DanhMucService.LinhVucGetById(id);
            if (item == null) HttpNotFound();
            return PartialView("_Delete", item);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            DanhMucService.LinhVucDelete(id);
            return RedirectToAction("Index");
        }
    }
}
