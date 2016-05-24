using System.Web.Mvc;
using WebMVC.Bussiness;

using WebMVC.Entities;

namespace CBCC.Areas.Admin.Controllers
{
    public class NhomTieuChiController : Controller
    {
         [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin,Manage")]
        //
        // GET: /Admin/NhomTieuChi/
        public ActionResult Index()
        {
            var lst = DanhMucService.NhomTieuChiGetAll();
            return View(lst);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var NhomTieuChi = new NhomTieuChi();
            return PartialView("_Create", NhomTieuChi);
        }

        [HttpPost]
        public ActionResult Create(NhomTieuChi nhomTieuChiModel)
        {
            if (ModelState.IsValid)
            {
                var danhmuc = new NhomTieuChi()
                {
                    TenNhomTieuChi = nhomTieuChiModel.TenNhomTieuChi,
                    Active = true
                };
                DanhMucService.NhomTieuChiCreate(danhmuc);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("_Create", nhomTieuChiModel);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var nhomtieuchi = new NhomTieuChi();
            var danhmuc = DanhMucService.NhomTieuChiGet(id);
            nhomtieuchi.TenNhomTieuChi = danhmuc.TenNhomTieuChi;
   
            return PartialView("_Edit", nhomtieuchi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NhomTieuChi NhomTieuChiModel)
        {
            if (ModelState.IsValid)
            {
               
                DanhMucService.NhomTieuChiUpdate(NhomTieuChiModel);
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Admin/NhomTieuChi/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var NhomTieuChi = DanhMucService.NhomTieuChiGet(id);
            if (NhomTieuChi == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Delete", NhomTieuChi);
        }

        //
        // POST: /Admin/NhomTieuChi/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DanhMucService.NhomTieuChiDel(id);
            return RedirectToAction("Index");
        }
    }
}