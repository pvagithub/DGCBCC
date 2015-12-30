using System;
using System.Collections.Generic;
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
               _Danhgia.HoSoID = idHoSo.ToString();
               if (null != hoSo)
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


                   ///Lưu Thông tin danh giá
                 
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

                //Cập nhật thông tin đánh giá cho Hồ sơ

                HoSo_UpdateThongTinDanhGia(idDanhGia, idHoSo);

                context.SaveChanges();

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
                } 
                context.SaveChanges();

                return true;
            }
        }


        #endregion Kết quả đánh giá
    }
}