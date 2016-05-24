using System.Collections.Generic;
using System.Linq;
using WebMVC.Dal;
using WebMVC.Dal.Extensions;
using WebMVC.Entities;

namespace WebMVC.Bussiness
{
    public class HoSoService
    {
        #region HoSo

        public static List<HoSo> HoSoGetAll()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.HoSoes.ToList();
            }
        }

        public static HoSo HoSoGetBySoBienNhan(string soBienNhan)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.HoSoes.Where(x => x.SoBienNhan == soBienNhan).FirstOrDefault();
            }
        }

        public static HoSo HoSoGetBySoBienNhan_DanhGia(string soBienNhan, out bool daDuocDanhGia, string[] maDonVi)
        {
            string oldMaDonVi = maDonVi[0];
            string newMaDonVi = maDonVi[1];
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();

                var hoso = context.HoSoes.Where(x => x.SoBienNhan == soBienNhan && (x.MaDonVi.Trim() == oldMaDonVi || x.MaDonVi.Trim() == newMaDonVi)).FirstOrDefault();

                if (hoso != null)
                {
                    daDuocDanhGia = hoso.DaDanhGia ?? false;
                }
                else
                {
                    daDuocDanhGia = false;
                }

                return hoso;
            }
        }

        public static void HoSoListCreate(List<HoSo> lst)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();

                context.HoSoes.AddRange(lst);
                context.SaveChanges();
            }
        }

        public static void QuaTrinhListCreate(List<QuaTrinhXuLy> lst)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();

                context.QuaTrinhXuLies.AddRange(lst);
                context.SaveChanges();
            }
        }

        public static void ThongTinNguoiXuLyListCreate(List<ThongTinNguoiXuLy> lst)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();

                context.ThongTinNguoiXuLies.AddRange(lst);
                context.SaveChanges();
            }
        }
        public static string HoSoCreate(HoSo hs)
        {
            string id = string.Empty;
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();

                context.HoSoes.Add(hs);
                context.SaveChanges();
                id = hs.HoSoID;
            }
            return id;
        }
        #endregion HoSo
    }
}