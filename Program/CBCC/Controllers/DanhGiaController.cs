using CBCC.Models;
using CBCC.SearchService;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
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
            HoSo hoSo = null;
            if (null != Session["CAPTCHA"])
            {
                var randText = Session["CAPTCHA"].ToString();
                if (!string.IsNullOrEmpty(randText) && randText.Equals(soBienNhan))
                {
                    //handle with code
                    return Json(new { isCaptchaValid = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    bool daDuocDanhGia = true;

                    hoSo = HoSoService.HoSoGetBySoBienNhan_DanhGia(soBienNhan, out daDuocDanhGia);
                    bool isExist = hoSo != null ? true : false;
                    if (!isExist)
                    {
                        DataSet dsInfo = new DataSet();

                        // Service lấy thông tin hồ sơ    
                        WebSearchClient clientweb = new WebSearchClient("websearch");

                        //Service lấy thông tin tình trạng 
                        VoiceSearchClient clientvoice = new VoiceSearchClient("voicesearch");

                        Dictionary<object, object> datas = new Dictionary<object, object>();
                        datas["SoBienNhan"] = soBienNhan;
                        dsInfo = clientweb.WebSearch(datas);

                        if (dsInfo != null && dsInfo.Tables[0].Rows.Count > 0)
                        {
                            hoSo = new HoSo()
                            {
                                SoBienNhan = dsInfo.Tables[0].Rows[0]["SoBienNhan"].ToString(),
                                TenToChuc = dsInfo.Tables[0].Rows[0]["HoTenNguoiNop"].ToString(),
                                DiaChi = dsInfo.Tables[0].Rows[0]["DiaChiThuongTru"].ToString(),
                                NgayNhan = System.DateTime.ParseExact(dsInfo.Tables[0].Rows[0]["NgayNhan"].ToString(), "dd/MM/yyyy", null),
                                NgayHenTra = System.DateTime.ParseExact(dsInfo.Tables[0].Rows[0]["NgayHenTra"].ToString(), "dd/MM/yyyy", null)
                            };
                            //dsInfo.Tables[0].Rows[0]["TenTinhTrang"].ToString();
                            isExist = true;

                        }

                        //string matinhtrang = clientvoice.VoiceSearch(datas);
                    }
                    return Json(new { IsExist = isExist, HoSo = hoSo, DaDuocDanhGia = daDuocDanhGia }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { IsExist = false, isCaptchaValid = false, HoSo = hoSo, DaDuocDanhGia = true }, JsonRequestBehavior.AllowGet);
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
                    if (!string.IsNullOrWhiteSpace(item.Answered))
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

        [HttpPost]
        public JsonResult InsertGopYCauHoi(string soBienNhan, long cauHoiId, string noiDungGopY, long tieuChiId)
        {
            var gopy = new GopYCauHoi();
            if (!string.IsNullOrWhiteSpace(noiDungGopY))
            {
                gopy.SoBienNhan = soBienNhan;
                gopy.CauHoiId = cauHoiId;
                gopy.NoiDungGopY = noiDungGopY;
                gopy.TieuChiId = tieuChiId;
                DanhGiaService.InsertGopYCauHoi(gopy);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SelectGopYCauHoi(string soBienNhan, long cauHoiId)
        {
            var gopy = new GopYCauHoi();
            gopy.SoBienNhan = soBienNhan;
            gopy.CauHoiId = cauHoiId;
            string result = DanhGiaService.SelectGopYCauHoi(gopy);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}