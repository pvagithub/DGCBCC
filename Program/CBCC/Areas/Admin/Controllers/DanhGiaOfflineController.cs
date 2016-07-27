using CBCC.Models;
using CBCC.SearchService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;

namespace CBCC.Areas.Admin.Controllers
{
    public class DanhGiaOfflineController : Controller
    {
        //
        // GET: /Admin/CaiDat/
        public ActionResult Index()
        {
            ViewBag.Result = ThongKeService.HienThiTieuChi();
            var lsDonVi = DanhMucService.DonViGetAllList().Where(x => x.Active == true).ToList();
            return View(lsDonVi);
        }
        [HttpPost]
        public JsonResult Index(string donViID, string soBienNhan, string[] arrAnswer)
        {
            var arrMaDonVi = donViID.Split('_');
            HoSo hoSo = null;
            hoSo = new HoSo()
            {
                SoBienNhan = soBienNhan,
            };
            HoSoService.HoSoCreate(hoSo);
            //bool daDuocDanhGia = true;

            //hoSo = HoSoService.HoSoGetBySoBienNhan_DanhGia(soBienNhan, out daDuocDanhGia, arrMaDonVi);
            //bool isExist = hoSo != null ? true : false;
            try
            {
                //if (!isExist)
                //{
                //    var dsHoSo = DanhGiaService.ThongTinHoSo(arrMaDonVi[1], soBienNhan);
                //    HoSoDetails hs = JsonConvert.DeserializeObject<HoSoDetails>(dsHoSo);
                //    if (hs != null && !string.IsNullOrWhiteSpace(hs.recordNo))
                //    {
                //        //code
                //        hoSo = new HoSo()
                //        {
                //            MaDonVi = arrMaDonVi[1],
                //            NguoiNop = hs.fullname,
                //            SoBienNhan = hs.recordNo,
                //            TenToChuc = hs.fullname,
                //            DiaChi = hs.orgAddress,
                //            NgayNhan = !string.IsNullOrWhiteSpace(hs.received) ? ConvertToNallableDate("/Date(" + hs.received + "+0000)/") : new Nullable<DateTime>(),
                //            NgayHenTra = !string.IsNullOrWhiteSpace(hs.appointment) ? ConvertToNallableDate("/Date(" + hs.appointment + "+0000)/") : new Nullable<DateTime>()
                //        };
                //        HoSoService.HoSoCreate(hoSo);
                //    }
                //    else
                //    {
                //        DataSet dsInfo = new DataSet();
                //        // Service lấy thông tin hồ sơ    
                //        WebSearchClient clientweb = new WebSearchClient("websearch");

                //        Dictionary<object, object> datas = new Dictionary<object, object>();
                //        datas["SoBienNhan"] = soBienNhan;
                //        datas["MaDonVi"] = arrMaDonVi[0];
                //        dsInfo = clientweb.WebSearch(datas);

                //        if (dsInfo != null && dsInfo.Tables[0].Rows.Count > 0)
                //        {
                //            hoSo = new HoSo()
                //            {
                //                MaDonVi = arrMaDonVi[0],
                //                NguoiNop = dsInfo.Tables[0].Rows[0]["HoTenNguoiNop"].ToString(),
                //                SoBienNhan = dsInfo.Tables[0].Rows[0]["SoBienNhan"].ToString(),
                //                TenToChuc = dsInfo.Tables[0].Rows[0]["HoTenNguoiNop"].ToString(),
                //                DiaChi = dsInfo.Tables[0].Rows[0]["DiaChiThuongTru"].ToString(),
                //                NgayNhan = System.DateTime.ParseExact(dsInfo.Tables[0].Rows[0]["NgayNhan"].ToString(), "dd/MM/yyyy", null),
                //                NgayHenTra = System.DateTime.ParseExact(dsInfo.Tables[0].Rows[0]["NgayHenTra"].ToString(), "dd/MM/yyyy", null)
                //            };
                //            HoSoService.HoSoCreate(hoSo);
                //        }
                //    }
                //}
                //else
                //{
                //    return Json(new { success = false, msg = "Số biên nhận không đúng!" }, JsonRequestBehavior.AllowGet);
                //}
                var danhGia = new DanhGia();
                // Get thong tin đánh giá

                var lstKQDG = new List<KetQuaDanhGia>();
                foreach (var item in arrAnswer)
                {
                    var kq = new KetQuaDanhGia();

                    kq.TieuChiID = int.Parse(item.Split('_')[0]);
                    if (!string.IsNullOrWhiteSpace(item.Split('_')[1]))
                        kq.CauTraLoiID = int.Parse(item.Split('_')[1]);
                    lstKQDG.Add(kq);
                }

                DanhGiaService.SaveKetQuaDanhGiaOffline(danhGia, lstKQDG, hoSo.ID, donViID, soBienNhan);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "Lưu không thành công" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, msg = "Lưu thành công" }, JsonRequestBehavior.AllowGet);
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