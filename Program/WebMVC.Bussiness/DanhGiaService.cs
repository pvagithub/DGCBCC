using System;
using System.Collections.Generic;
using System.Linq;
using WebMVC.Dal;
using WebMVC.Dal.Extensions;
using WebMVC.Entities;

namespace WebMVC.Bussiness
{
    public class DanhGiaService
    {
        #region Kết quả đánh giá

        public static bool SaveKetQuaDanhGia(DanhGia _Danhgia, List<KetQuaDanhGia> _KetQuaDanhGia, long idHoSo)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();

                var hoSo = context.HoSoes.Find(idHoSo);
                if (hoSo != null)
                {
                    _Danhgia.HoSoID = hoSo.ID.ToString();
                    _Danhgia.SoBienNhan = hoSo.SoBienNhan;
                    _Danhgia.DonViID = hoSo.DonViID;
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
                    _Danhgia.SoBienNhan = idHoSo.ToString();
                    _Danhgia.DonViID = null;
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

                foreach (var item in _KetQuaDanhGia)
                {
                    item.DanhGiaID = idDanhGia;
                    context.KetQuaDanhGias.Add(item);
                }
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
    }
}