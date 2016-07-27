using CBCC.Models;
using CBCC.SearchService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebMVC.Bussiness;
using System.Linq;
using WebMVC.Entities;

namespace CBCC.Controllers
{
    public class DanhGiaController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.sobiennhan = Request.QueryString["sobienhan"] != null ? Request.QueryString["sobienhan"].ToString() : "";
            ViewBag.madonvi = Request.QueryString["MaDonVi"] != null ? Request.QueryString["MaDonVi"].ToString() : "";
            var lsDonVi = DanhMucService.DonViGetAllList().Where(x => x.Active == true).ToList();
            return View(lsDonVi);
        }

        public JsonResult HoSoGetBySoBienNhan_DanhGia(string soBienNhan, string maDonVi)
        {
            var arrMaDonVi = maDonVi.Split('_');
            DateTime startDate = new DateTime(2016, 4, 1);  // ngày cho phép đánh giá 
            HoSo hoSo = null;
            //if (null != Session["CAPTCHA"])
            //{
            //    var randText = Session["CAPTCHA"].ToString();
            //    if (!string.IsNullOrEmpty(randText) && randText.Equals(soBienNhan))
            //    {
            //        //handle with code
            //        return Json(new { isCaptchaValid = true }, JsonRequestBehavior.AllowGet);
            //    }
            //    else
            //    {
            bool daDuocDanhGia = true;

            hoSo = HoSoService.HoSoGetBySoBienNhan_DanhGia(soBienNhan, out daDuocDanhGia, arrMaDonVi);
            bool isExist = hoSo != null ? true : false;
            bool allowDanhGia = true; // kiem tra lon hon startDate thi cho phep danh gia
            try
            {
                if (!isExist)
                {
                    var dsHoSo = DanhGiaService.ThongTinHoSo(arrMaDonVi[1], soBienNhan);
                    HoSoDetails hs = JsonConvert.DeserializeObject<HoSoDetails>(dsHoSo);
                    if (hs != null && !string.IsNullOrWhiteSpace(hs.recordNo))
                    {
                        //code
                        hoSo = new HoSo()
                        {
                            MaDonVi = arrMaDonVi[1],
                            NguoiNop = hs.fullname,
                            SoBienNhan = hs.recordNo,
                            TenToChuc = hs.fullname,
                            DiaChi = hs.orgAddress,
                            NgayNhan = !string.IsNullOrWhiteSpace(hs.received) ? ConvertToNallableDate("/Date(" + hs.received + "+0000)/") : new Nullable<DateTime>(),
                            NgayHenTra = !string.IsNullOrWhiteSpace(hs.appointment) ? ConvertToNallableDate("/Date(" + hs.appointment + "+0000)/") : new Nullable<DateTime>()
                        };
                        isExist = true;
                        allowDanhGia = hoSo.NgayNhan >= startDate;
                        HoSoService.HoSoCreate(hoSo);
                    }
                    else
                    {
                        DataSet dsInfo = new DataSet();

                        // Service lấy thông tin hồ sơ    
                        WebSearchClient clientweb = new WebSearchClient("websearch");

                        //Service lấy thông tin tình trạng 
                       // VoiceSearchClient clientvoice = new VoiceSearchClient("voicesearch");

                        Dictionary<object, object> datas = new Dictionary<object, object>();
                        datas["SoBienNhan"] = soBienNhan;
                        datas["MaDonVi"] = arrMaDonVi[0];
                        dsInfo = clientweb.WebSearch(datas);

                        if (dsInfo != null && dsInfo.Tables[0].Rows.Count > 0)
                        {
                            hoSo = new HoSo()
                            {
                                MaDonVi = arrMaDonVi[0],
                                NguoiNop = dsInfo.Tables[0].Rows[0]["HoTenNguoiNop"].ToString(),
                                SoBienNhan = dsInfo.Tables[0].Rows[0]["SoBienNhan"].ToString(),
                                TenToChuc = dsInfo.Tables[0].Rows[0]["HoTenNguoiNop"].ToString(),
                                DiaChi = dsInfo.Tables[0].Rows[0]["DiaChiThuongTru"].ToString(),
                                NgayNhan = System.DateTime.ParseExact(dsInfo.Tables[0].Rows[0]["NgayNhan"].ToString(), "dd/MM/yyyy", null),
                                NgayHenTra = System.DateTime.ParseExact(dsInfo.Tables[0].Rows[0]["NgayHenTra"].ToString(), "dd/MM/yyyy", null)
                            };
                            //dsInfo.Tables[0].Rows[0]["TenTinhTrang"].ToString();
                            isExist = true;
                            allowDanhGia = hoSo.NgayNhan >= startDate;
                            HoSoService.HoSoCreate(hoSo);
                        }
                        else
                        {
                            isExist = false;
                        }
                        }
                       
                    //string matinhtrang = clientvoice.VoiceSearch(datas);
                }
                return Json(new { IsExist = isExist, HoSo = hoSo, DaDuocDanhGia = daDuocDanhGia, allow = allowDanhGia }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsExist = false, HoSo = hoSo, DaDuocDanhGia = daDuocDanhGia, allow = allowDanhGia }, JsonRequestBehavior.AllowGet);
            }
           
           
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
        public JsonResult SaveDanhGia(string DanhSachKQ, string iDHoSo, string iDonViID, string soBN)
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

            result = DanhGiaService.SaveKetQuaDanhGia(danhGia, lstKQDG, long.Parse(iDHoSo), iDonViID, soBN);
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
        public static DateTime? ConvertToNallableDate(string date)
        {
            DateTime? val = null;
            /*          /Date(1389435240000+0000)/*/
            try
            {
                if (!string.IsNullOrEmpty(date))
                {
                    date = date.Replace("/Date(", string.Empty).Replace(")/", string.Empty);
                    int pIndex = date.IndexOf("+");
                    if (pIndex < 0) pIndex = date.IndexOf("-");
                    long millisec = 0;
                    date = date.Remove(pIndex);
                    long.TryParse(date, out millisec);
                    System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
                    DateTime newDate = DateTime.Parse("1970,1,1", ci);
                    newDate = newDate.AddMilliseconds(millisec);
                    val = newDate == null ? (DateTime?)null : newDate;

                }
            }
            catch
            {
                val = null;
            }
            return val;
        }
    }
}