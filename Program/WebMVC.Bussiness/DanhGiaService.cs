using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using WebMVC.Dal;
using WebMVC.Dal.Extensions;
using WebMVC.Entities;

namespace WebMVC.Bussiness
{
    public class DanhGiaService
    {
        #region Kết quả đánh giá

        public static bool SaveKetQuaDanhGia(DanhGia _Danhgia, List<KetQuaDanhGia> _KetQuaDanhGia, long idHoSo, string DonViID, string soBN)
        {
            using (var context = new DataModelEntities())
            {
                var arrMaDonVi = DonViID.Split('_');
                context.ReadUncommited();

                var hoSo = context.HoSoes.Find(idHoSo);
                if (hoSo != null)
                {
                    _Danhgia.DonViID = hoSo.DonViID;
                    _Danhgia.HoSoID = hoSo.ID.ToString();
                    _Danhgia.MaDonVi = arrMaDonVi[0];
                    _Danhgia.MaLienThong = arrMaDonVi[1];
                    _Danhgia.SoBienNhan = soBN;
                    _Danhgia.TenDonVi = hoSo.TenDonVi;
                    _Danhgia.LinhVucID = hoSo.LinhVucID;
                    _Danhgia.TenLinhVuc = hoSo.TenLinhVuc;
                    _Danhgia.ThuTucID = hoSo.ThuTucID;
                    _Danhgia.TenThuTuc = hoSo.TenThuTuc;
                    _Danhgia.NgayDanhGia = DateTime.Now;
                    _Danhgia.DanhGiaTrucTiep = true;
                }
                // danh gia khi nhap  capcha
                else
                {
                    _Danhgia.HoSoID = null;
                    _Danhgia.SoBienNhan = soBN;
                    _Danhgia.DonViID = null;
                    _Danhgia.MaDonVi = arrMaDonVi[0];
                    _Danhgia.MaLienThong = arrMaDonVi[1];
                    _Danhgia.TenDonVi = null;
                    _Danhgia.LinhVucID = null;
                    _Danhgia.TenLinhVuc = null;
                    _Danhgia.ThuTucID = null;
                    _Danhgia.TenThuTuc = null;
                    _Danhgia.NgayDanhGia = DateTime.Now;
                    _Danhgia.DanhGiaTrucTiep = true;
                }

                context.DanhGias.Add(_Danhgia);
                context.SaveChanges();
                var idDanhGia = _Danhgia.ID;
                ///Lưu ket quả danh giá co thông ke
                List<KetQuaDanhGia> lstit = new List<KetQuaDanhGia>();
                foreach (var item in _KetQuaDanhGia)
                {
                    if (item.CauTraLoiID != null)
                    {
                        KetQuaDanhGia it = new KetQuaDanhGia();
                        it.DanhGiaID = idDanhGia;
                        it.TieuChiID = item.TieuChiID;
                        it.CauTraLoiID = item.CauTraLoiID;
                        it.TypeInput = item.TypeInput;
                        lstit.Add(it);
                    }
                }
                context.KetQuaDanhGias.AddRange(lstit);
                context.SaveChanges();
                //Cập nhật thông tin đánh giá cho Hồ sơ

                HoSo_UpdateThongTinDanhGia(idDanhGia, idHoSo);


                return true;
            }
        }


        public static bool HoSo_UpdateThongTinDanhGia(long danhGiaId, long idHoSo)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                var hoSo = context.HoSoes.Find(idHoSo);
                if (hoSo != null)
                {
                    hoSo.DaDanhGia = true;
                    hoSo.DanhGiaID = danhGiaId;
                    context.SaveChanges();
                }

                return true;
            }
        }
        public static bool SaveKetQuaDanhGiaOffline(DanhGia _Danhgia, List<KetQuaDanhGia> _KetQuaDanhGia, long idHoSo, string DonViID, string soBN)
        {
            using (var context = new DataModelEntities())
            {
                var arrMaDonVi = DonViID.Split('_');
                context.ReadUncommited();

                _Danhgia.HoSoID = null;
                _Danhgia.SoBienNhan = soBN;
                _Danhgia.DonViID = null;
                _Danhgia.MaDonVi = arrMaDonVi[0];
                _Danhgia.MaLienThong = arrMaDonVi[1];
                _Danhgia.TenDonVi = null;
                _Danhgia.LinhVucID = null;
                _Danhgia.TenLinhVuc = null;
                _Danhgia.ThuTucID = null;
                _Danhgia.TenThuTuc = null;
                _Danhgia.NgayDanhGia = DateTime.Now;
                _Danhgia.DanhGiaTrucTiep = false;

                context.DanhGias.Add(_Danhgia);
                context.SaveChanges();
                var idDanhGia = _Danhgia.ID;
                ///Lưu ket quả danh giá co thông ke
                List<KetQuaDanhGia> lstit = new List<KetQuaDanhGia>();
                foreach (var item in _KetQuaDanhGia)
                {
                    if (item.CauTraLoiID!=null)
                    {
                        KetQuaDanhGia it = new KetQuaDanhGia();
                        it.DanhGiaID = idDanhGia;
                        it.TieuChiID = item.TieuChiID;
                        it.CauTraLoiID = item.CauTraLoiID;
                        it.TypeInput = item.TypeInput;
                        lstit.Add(it);
                    }
                }
                context.KetQuaDanhGias.AddRange(lstit);
                context.SaveChanges();
                //Cập nhật thông tin đánh giá cho Hồ sơ

                HoSo_UpdateThongTinDanhGia(idDanhGia, idHoSo);

                return true;
            }
        }

        #endregion Kết quả đánh giá

        #region Them Gop Y Cau Hoi
        public static void InsertGopYCauHoi(GopYCauHoi model)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                var gopy = context.GopYCauHois.FirstOrDefault(x => x.SoBienNhan == model.SoBienNhan && x.CauHoiId == model.CauHoiId);
                if (gopy != null)
                {
                    gopy.NoiDungGopY = model.NoiDungGopY;
                    gopy.TieuChiId = model.TieuChiId;
                }
                else
                    context.GopYCauHois.Add(model);
                context.SaveChanges();
            }
        }
        public static string SelectGopYCauHoi(GopYCauHoi model)
        {
            string result = string.Empty;
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                var gopy = context.GopYCauHois.FirstOrDefault(x => x.SoBienNhan == model.SoBienNhan && x.CauHoiId == model.CauHoiId);
                if (gopy != null)
                    result = gopy.NoiDungGopY;
            }
            return result;
        }
        public static List<GopYCauHoi> SelectAllGopYCauHoi()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                return context.GopYCauHois.ToList();
            }
        }
        #endregion

        public static string ThongTinHoSo(string maDonvi, string soBienNhan)
        {
            string json = string.Empty;
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                json = wc.DownloadString("https://dichvucong.hochiminhcity.gov.vn/icloudgate/version4/restapi/onegate/" + maDonvi.Trim() + "/searchrecord?recordNo=" + soBienNhan);
            }
            return json;
        }
    }
}