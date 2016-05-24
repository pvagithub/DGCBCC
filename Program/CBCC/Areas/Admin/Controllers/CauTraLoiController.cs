using PagedList;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;
using WebMVC.Framework.Utilities;

namespace CBCC.Areas.Admin.Controllers
{
    public class CauTraLoiController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin,Manage")]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            var lstCauTraLoi = DanhMucService.CauTraLoiGetAll_ForBO();

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
                lstCauTraLoi = lstCauTraLoi.Where(s => TextUtility.RemoveSign4VietnameseString(s.TenCauTraLoi).Trim().ToLower().Contains(TextUtility.RemoveSign4VietnameseString(searchString).Trim().ToLower())).ToList();
            }

            int pageSize = Convert.ToInt32(ConfigurationSettings.AppSettings["PageSize_Admin"]);
            int pageNumber = (page ?? 1);
            ViewBag.Page = (pageNumber - 1) * pageSize;
            return View(lstCauTraLoi.ToPagedList(pageNumber, pageSize));
        }

        //

        [HttpGet]
        public ActionResult Create()
        {
            var cautraloi = new CauTraLoiExtend();

            return PartialView("_CreateCauTraLoi", cautraloi);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(CauTraLoiExtend cauTraLoi)
        {
            if (ModelState.IsValid)
            {
                CauTraLoi ctl = new CauTraLoi();
                ctl.GiaTri = cauTraLoi.GiaTri;
                ctl.ID = cauTraLoi.ID;

                ctl.TenCauTraLoi = cauTraLoi.TenCauTraLoi;
                if (string.IsNullOrEmpty(cauTraLoi.TenVanTat))
                    ctl.TenVanTat = ctl.TenCauTraLoi;
                else
                    ctl.TenVanTat = cauTraLoi.TenVanTat;
                ctl.TieuChiCauTraLois = cauTraLoi.TieuChiCauTraLois;
                ctl.Active = true;

                DanhMucService.CauTraLoiCreate(ctl);
                return RedirectToAction("Index");
            }

            return PartialView("_CreateCauTraLoi", cauTraLoi);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CauTraLoi cautraloi = DanhMucService.CauTraLoiGet(id);
            CauTraLoiExtend cauTraLoiExtend = new CauTraLoiExtend();
            cauTraLoiExtend.GiaTri = cautraloi.GiaTri;
            cauTraLoiExtend.ID = cautraloi.ID;
            cauTraLoiExtend.Active = cautraloi.Active;

            cauTraLoiExtend.TenCauTraLoi = cautraloi.TenCauTraLoi;
            cauTraLoiExtend.TenVanTat = cautraloi.TenVanTat;
            return PartialView("_EditCauTraLoi", cauTraLoiExtend);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(CauTraLoiExtend cautraloi)
        {
            if (ModelState.IsValid)
            {
                CauTraLoi ctl = new CauTraLoi();
                ctl.GiaTri = cautraloi.GiaTri;
                ctl.ID = cautraloi.ID;
                ctl.Active = cautraloi.Active;

                ctl.TenCauTraLoi = cautraloi.TenCauTraLoi;
                ctl.TenVanTat = cautraloi.TenVanTat;
                DanhMucService.CauTraLoiUpdate(ctl);
                return RedirectToAction("Index");
            }

            return PartialView("_EditCauTraLoi", cautraloi);
        }

        //
        // GET: /Admin/NhomCauTraLoi/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var cauTraLoi = DanhMucService.CauTraLoiGet(id);
            if (cauTraLoi == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Delete", cauTraLoi);
        }

        //
        // POST: /Admin/NhomCauTraLoi/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DanhMucService.CauTraLoiDel(id);
            return RedirectToAction("Index");
        }
    }
}