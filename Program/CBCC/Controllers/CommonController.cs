using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;

namespace CBCC.Controllers
{
    public class CommonController : Controller
    {
        public JsonResult LoadTimKiemThongKe()
        {
            var lstNam = DanhMucService.Nam_BaoCao_getList();

            List<SelectListItem> lstNamSelect = new List<SelectListItem>();
            var itemFist = new SelectListItem { Text = "-- Chọn năm --", Value = "0" };
            lstNamSelect.Add(itemFist);

            var lst = lstNam.Select(a => new SelectListItem
            {
                Text = a.Nam.ToString(),
                Value = a.ID.ToString()
            }).ToList();
            lstNamSelect.AddRange(lst);

            return Json(lstNamSelect, JsonRequestBehavior.AllowGet);
        }

        public JsonResult NamThongKe_Change(int namId)
        {
            var lstThang = DanhMucService.Thang_BaoCao_getList(namId, 0);
            List<SelectListItem> lstThangSelect = new List<SelectListItem>();
            var itemFist1 = new SelectListItem { Text = "-- Chọn tháng --", Value = "0" };
            lstThangSelect.Add(itemFist1);
            var lst = lstThang.Select(a => new SelectListItem
                   {
                       Text = a.Thang.ToString(),
                       Value = a.ID.ToString()
                   }).ToList();
            lstThangSelect.AddRange(lst);

            var lstQui = DanhMucService.Quy_BaoCao_getListByNamID(namId);
            List<SelectListItem> lstQuiSelect = new List<SelectListItem>();
            var itemFist = new SelectListItem { Text = "-- Chọn quý --", Value = "0" };
            lstQuiSelect.Add(itemFist);
            var lst1 = lstQui.Select(a => new SelectListItem
             {
                 Text = a.Quy.ToString(),
                 Value = a.ID.ToString()
             }).ToList();
            lstQuiSelect.AddRange(lst1);

            return Json(new { thang = lstThangSelect, quy = lstQuiSelect }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Quy_BaoCaoList(int namId)
        {
            var lstQuy = DanhMucService.Quy_BaoCao_getListByNamID(namId);
            List<SelectListItem> lstQuySelect = new List<SelectListItem>();
            var itemFist = new SelectListItem { Text = "-- Chọn quý --", Value = "0" };
            lstQuySelect.Add(itemFist);
            var lst = lstQuy.Select(a => new SelectListItem
            {
                Text = a.Quy.ToString(),
                Value = a.ID.ToString()
            }).ToList();
            lstQuySelect.AddRange(lst);

            return Json(lstQuySelect, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Tuan_BaoCao_getList(int namID, int quyID, int thangID)
        {
            var lstTuan = DanhMucService.Tuan_BaoCao_getList(namID, quyID, thangID);
            List<SelectListItem> lstTuanSelect = new List<SelectListItem>();

            var itemFist = new SelectListItem { Text = "-- Chọn tuần --", Value = "0" };
            lstTuanSelect.Add(itemFist);
            var lst = lstTuan.Select(a => new SelectListItem
            {
                Text = a.WeekOfYear.ToString(),
                Value = a.ID.ToString()
            }).ToList();

            lstTuanSelect.AddRange(lst);

            return Json(lstTuanSelect, JsonRequestBehavior.AllowGet);
        }
    }
}