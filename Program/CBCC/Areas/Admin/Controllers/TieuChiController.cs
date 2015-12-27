using CBCC.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;
using WebMVC.Framework.Utilities;

namespace CBCC.Areas.Admin.Controllers
{
    public class TieuChiController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<TieuChiCauTraLoi> dsCauTraLoi = new List<TieuChiCauTraLoi>();
            var dsTieuChi = DanhMucService.TieuChiGetAll(out dsCauTraLoi);
            ViewBag.DanhSacCauTraLoi = dsCauTraLoi;

            ViewBag.CurrentSort = sortOrder;

            ViewBag.TenTieuChiSortParm = sortOrder == "TenTieuChi" ? "TenTieuChi_desc" : "TenTieuChi";

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
                dsTieuChi = dsTieuChi.Where(s => TextUtility.RemoveSign4VietnameseString(s.TenTieuChi).Trim().ToLower().Contains(TextUtility.RemoveSign4VietnameseString(searchString).ToLower().Trim())).ToList();
            }

            switch (sortOrder)
            {
                case "TenTieuChi":
                    dsTieuChi = dsTieuChi.OrderBy(s => s.TenTieuChi).ToList(); ;
                    break;

                case "TenTieuChi_desc":
                    dsTieuChi = dsTieuChi.OrderByDescending(s => s.TenTieuChi).ToList(); ;
                    break;
            }

            int pageSize = Convert.ToInt32(ConfigurationSettings.AppSettings["PageSize_TieuChi"]);
            int pageNumber = (page ?? 1);
            ViewBag.Page = (pageNumber - 1) * pageSize;
            return View(dsTieuChi.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var tieuchi = new TieuChiModel();

            List<int> dsCauTraLoi = new List<int>();

            //var dsCauTraLoiEntities = DanhMucService.CauTraLoiGetAll_ForBO().ToList().Select(x => x.ID);

            return PartialView("_CreateChiTieu", tieuchi);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TieuChiModel tieuchiModel)
        {
            if (ModelState.IsValid)
            {
                TieuChi tieuchi = new TieuChi();
                tieuchi.Active = true;
                tieuchi.TenTieuChi = tieuchiModel.TenTieuChi;
                tieuchi.NhomTieuChiID = tieuchiModel.NhomTieuChiID;
                tieuchi.TypeInput = tieuchiModel.TypeInput;
                if (string.IsNullOrEmpty(tieuchiModel.TenVanTat))
                    tieuchi.TenVanTat = tieuchiModel.TenTieuChi;
                else
                    tieuchi.TenVanTat = tieuchiModel.TenVanTat;
                int IDTieuChi = DanhMucService.TieuChiCreate(tieuchi);

                if (tieuchiModel.ListIDCauTraLoi != null && tieuchiModel.ListIDCauTraLoi.Count > 0)
                {
                    DanhMucService.TieuChiCauTraLoiCreate(IDTieuChi, tieuchiModel.ListIDCauTraLoi.Select(s => int.Parse(s)).ToList());
                }

                return RedirectToAction("Index");
            }

            return PartialView("_CreateChiTieu", tieuchiModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var tieuchi = new TieuChiModel();

            List<int> dsCauTraLoi = new List<int>();

            TieuChi tieuchiGet = DanhMucService.TieuChiGet(id, out dsCauTraLoi);
            tieuchi.ID = tieuchiGet.ID;
            tieuchi.TenTieuChi = tieuchiGet.TenTieuChi;
            tieuchi.NhomTieuChiID = tieuchiGet.NhomTieuChiID;
            tieuchi.TenVanTat = tieuchiGet.TenVanTat;
            tieuchi.TypeInput = tieuchiGet.TypeInput;
            tieuchi.Active = tieuchiGet.Active;
        

            tieuchi.ListIDCauTraLoi = (from i in dsCauTraLoi select i.ToString()).ToList();

            return PartialView("_EditChiTieu", tieuchi);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TieuChiModel tieuchiModel)
        {
            if (ModelState.IsValid)
            {
                DanhMucService.TieuChiUpdate(tieuchiModel);

                if (tieuchiModel.ListIDCauTraLoi != null && tieuchiModel.ListIDCauTraLoi.Count > 0)
                {
                    var lstID = tieuchiModel.ListIDCauTraLoi.Select(s => int.Parse(s)).ToList();
                    DanhMucService.TieuChiCauTraLoiCreate(tieuchiModel.ID, lstID);
                }
                return RedirectToAction("Index");
            }

            return PartialView("_EditChiTieu", tieuchiModel);
        }

        public ActionResult Delete(int id = 0)
        {
            var tieuchi = DanhMucService.TieuChiGet(id);
            if (tieuchi == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Delete", tieuchi);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DanhMucService.TieuChiDel(id);
            return RedirectToAction("Index");
        }
    }
}