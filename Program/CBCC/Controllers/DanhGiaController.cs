using CBCC.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebMVC.Bussiness;
using WebMVC.Entities;

namespace CBCC.Controllers
{
    public class DanhGiaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult HoSoGetBySoBienNhan_DanhGia(string soBienNhan)
        {
            bool daDuocDanhGia = true;

            var hoSo = HoSoService.HoSoGetBySoBienNhan_DanhGia(soBienNhan, out daDuocDanhGia);
            bool isExist = hoSo != null ? true : false;

            return Json(new { IsExist = isExist, HoSo = hoSo, DaDuocDanhGia = daDuocDanhGia }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckDanhGia(string DanhSachKQ)
        {
            bool result = true;
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            List<KQDanhGiaModel> lstKetQua = (List<KQDanhGiaModel>)Newtonsoft.Json.JsonConvert.DeserializeObject(DanhSachKQ, typeof(List<KQDanhGiaModel>));
            foreach (var item in lstKetQua)
            {
                if (item.QuestionTypeId == 1)
                {
                    if (item.Answered == null || int.Parse(item.Answered) == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveDanhGia(string DanhSachKQ, string iDHoSo)
        {
            bool result = true;
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            List<KQDanhGiaModel> lstKetQua = (List<KQDanhGiaModel>)Newtonsoft.Json.JsonConvert.DeserializeObject(DanhSachKQ, typeof(List<KQDanhGiaModel>));

            var danhGia = new DanhGia();
            // Get thong tin đánh giá

            var lstKQDG = new List<KetQuaDanhGia>();

            foreach (var item in lstKetQua)
            {
                var kq = new KetQuaDanhGia();
                if (item.QuestionTypeId == 1)
                {
                    kq.TieuChiID = item.QuestionId;
                    kq.CauTraLoiID = int.Parse(item.Answered);
                }

                if (item.QuestionTypeId == 2)
                {
                    kq.CauTraLoiText = item.Answered;
                }
                lstKQDG.Add(kq);
            }

            result = DanhGiaService.SaveKetQuaDanhGia(danhGia, lstKQDG, long.Parse(iDHoSo));
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
    }
}