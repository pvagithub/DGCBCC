using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebMVC.Dal;
using WebMVC.Dal.Extensions;
using WebMVC.Entities;

namespace WebMVC.Bussiness
{
    public class DanhMucService
    {
        #region DonVi

        public static List<DonVi> DonViGetAllList()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.DonVis.ToList();
            }
        }

        public static int DonViCreate(DonVi donVi)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                context.DonVis.Add(donVi);
                context.SaveChanges();
                return donVi.DonViID;
            }
        }

        public static DonVi DonViGetById(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.DonVis.Find(id);
            }
        }

        public static void DonViUpdate(DonVi donVi)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                var item = context.DonVis.Find(donVi.DonViID);
                item.DonViID = donVi.DonViID;
                item.TenDonVi = donVi.TenDonVi;
                item.MoTa = donVi.MoTa;
                item.MaDonVi = donVi.MaDonVi;

                context.SaveChanges();
            }
        }

        public static void DonViDelete(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                var item = context.DonVis.Find(id);
                item.Active = !item.Active;
                context.SaveChanges();
            }
        }

        #endregion DonVi

        #region LinhVuc

        public static List<LinhVuc> LinhVucGetAllList()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.LinhVucs.OrderByDescending(x => x.TenLinhVuc).ToList();
            }
        }

        public static LinhVuc LinhVucGetById(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.LinhVucs.Find(id);
            }
        }

        public static int LinhVucCreate(LinhVuc linhVuc)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                context.LinhVucs.Add(linhVuc);
                context.SaveChanges();
                return linhVuc.LinhVucID;
            }
        }

        public static void LinhVucUpdate(LinhVuc linhVuc)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                var item = context.LinhVucs.Find(linhVuc.LinhVucID);
                item.MaLinhVuc = linhVuc.MaLinhVuc;
                item.TenLinhVuc = linhVuc.TenLinhVuc;
                item.MoTa = linhVuc.MoTa;

                context.SaveChanges();
            }
        }

        public static void LinhVucDelete(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                var item = context.LinhVucs.Find(id);
                item.Active = !item.Active;
                context.SaveChanges();
            }
        }

        #endregion LinhVuc

        #region ThuTuc

        public static List<ThuTuc> ThuTucGetAllList_ByLinhVucID(int linhVucID)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.ThuTucs.OrderByDescending(x => x.LinhVucID == linhVucID).ToList();
            }
        }

        public static List<ThuTuc> ThuTucGetAllList()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.ThuTucs.OrderByDescending(x => x.TenThucTuc).ToList();
            }
        }

        public static ThuTuc ThuTucGetById(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.ThuTucs.Find(id);
            }
        }

        public static int ThuTucCreate(ThuTuc thuTuc)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                context.ThuTucs.Add(thuTuc);
                context.SaveChanges();
                return thuTuc.ThuTucID;
            }
        }

        public static void ThuTucUpdate(ThuTuc thuTuc)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                var item = context.ThuTucs.Find(thuTuc.ThuTucID);
                item.MaThuTuc = thuTuc.MaThuTuc;
                item.TenThucTuc = thuTuc.TenThucTuc;
                item.Description = thuTuc.Description;

                context.SaveChanges();
            }
        }

        public static void ThuTucDelete(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                var item = context.ThuTucs.Find(id);
                item.Active = !item.Active;
                context.SaveChanges();
            }
        }

        public static List<ThuTuc> ThuTucGetAllList_ByDonViID(int donViID)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                var lstLinhVuc = context.LinhVucDonVis.Where(x => x.DonViID == donViID).Select(x => x.LinhVucID).ToList();

                var lstThuTuc = context.ThuTucs.Include(x => x.LinhVuc).Where(x => lstLinhVuc.Contains(x.LinhVucID.Value)).ToList();

                return lstThuTuc;
            }
        }

        #endregion ThuTuc

        #region Nhóm tiêu chí

        public static List<NhomTieuChi> NhomTieuChiGetAll()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.NhomTieuChis.ToList();
            }
        }

        public static void NhomTieuChiCreate(NhomTieuChi category)
        {
            using (var context = new DataModelEntities())
            {
                context.NhomTieuChis.Add(category);
                context.SaveChanges();
            }
        }

        public static void NhomTieuChiUpdate(NhomTieuChi nhomtieuchi)
        {
            using (var context = new DataModelEntities())
            {
                var item = context.NhomTieuChis.First(x => x.ID == nhomtieuchi.ID);

                item.TenNhomTieuChi = nhomtieuchi.TenNhomTieuChi;

                context.SaveChanges();
            }
        }

        public static NhomTieuChi NhomTieuChiGet(int id)
        {
            using (var context = new DataModelEntities())
            {
                return context.NhomTieuChis.First(x => x.ID == id);
            }
        }

        public static void NhomTieuChiDel(int id)
        {
            using (var context = new DataModelEntities())
            {
                var item = context.NhomTieuChis.First(x => x.ID == id);
                if (item.Active != null)
                {
                    item.Active = !item.Active;
                }
                else
                {
                    item.Active = true;
                }
                context.SaveChanges();
            }
        }

        #endregion Nhóm tiêu chí

        #region Tiêu chí

        /// <summary>
        /// Get danh sách câu hỏi và các option của câu hỏi đó
        /// </summary>
        /// <param name="dsCauTraLoi"></param>
        /// <returns></returns>
        public static List<TieuChi> TieuChiGetAll(out List<TieuChiCauTraLoi> dsCauTraLoi)
        {
            using (var context = new DataModelEntities())
            {
                dsCauTraLoi = new List<TieuChiCauTraLoi>();
                dsCauTraLoi = context.TieuChiCauTraLois.Include(x => x.CauTraLoi).OrderByDescending(x => x.CauTraLoi.GiaTri).ToList();
                context.ReadUncommited();

                return context.TieuChis.OrderBy(x => x.ID).Include(x => x.NhomTieuChi).AsNoTracking().ToList();
            }
        }

        public static List<TieuChi> TieuChiGetAll()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                IQueryable<TieuChi> query = context.TieuChis.Where(x => x.Active == true);
                return query.Include(x => x.NhomTieuChi).AsNoTracking().ToList();
            }
        }

        public static List<TieuChi> TieuChiGetAll_ThongKe()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                IQueryable<TieuChi> query = context.TieuChis.Where(x => x.Active == true && x.TypeInput == 1);
                return query.Include(x => x.NhomTieuChi).AsNoTracking().ToList();
            }
        }

        public static int TieuChiCreate(TieuChi chitieu)
        {
            using (var context = new DataModelEntities())
            {
                context.TieuChis.Add(chitieu);
                context.ReadCommited();
                context.SaveChanges();
                return chitieu.ID; // Yes it's here
            }
        }

        public static void TieuChiUpdate(TieuChi chitieu)
        {
            using (var context = new DataModelEntities())
            {
                var item = context.TieuChis.First(x => x.ID == chitieu.ID);

                item.TenTieuChi = chitieu.TenTieuChi;
                item.Active = chitieu.Active;
                item.NhomTieuChiID = chitieu.NhomTieuChiID;
                item.TypeInput = chitieu.TypeInput;
                item.TenVanTat = chitieu.TenVanTat;

                context.ReadCommited();
                context.SaveChanges();
            }
        }

        public static TieuChi TieuChiGet(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();

                return context.TieuChis.First(x => x.ID == id);
            }
        }

        public static TieuChi TieuChiGet(int id, out List<int> dsCauTraLoi)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                dsCauTraLoi = TieuChiCauTraLoiGetList_ByTieuChiID(id);
                return context.TieuChis.First(x => x.ID == id);
            }
        }

        public static void TieuChiDel(int id)
        {
            using (var context = new DataModelEntities())
            {
                var item = context.TieuChis.First(x => x.ID == id);
                if (item.Active != null)
                {
                    item.Active = !item.Active;
                }
                else
                {
                    item.Active = true;
                }
                context.SaveChanges();
            }
        }

        #endregion Tiêu chí

        #region Câu trả lời

        /// <summary>
        /// Get list câu trả lời sắp xep theo chiều giá trị giảm dần
        /// </summary>
        /// <returns></returns>
        public static List<CauTraLoi> CauTraLoiGetAll_ForBO()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.CauTraLois.OrderByDescending(x => x.GiaTri).ThenBy(x => x.TenCauTraLoi).AsNoTracking().ToList();
            }
        }

        public static List<CauTraLoi> CauTraLoiGetAll()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();

                return context.CauTraLois.OrderBy(x => x.GiaTri).ThenBy(x => x.TenCauTraLoi).AsNoTracking().ToList();
            }
        }

        public static void CauTraLoiCreate(CauTraLoi cautraloi)
        {
            using (var context = new DataModelEntities())
            {
                context.CauTraLois.Add(cautraloi);
                context.ReadCommited();
                context.SaveChanges();
            }
        }

        public static void CauTraLoiUpdate(CauTraLoi cautraloi)
        {
            using (var context = new DataModelEntities())
            {
                var item = context.CauTraLois.First(x => x.ID == cautraloi.ID);

                item.GiaTri = cautraloi.GiaTri;
                item.Active = cautraloi.Active;

                item.TenCauTraLoi = cautraloi.TenCauTraLoi;
                if (string.IsNullOrEmpty(cautraloi.TenVanTat))
                    item.TenVanTat = item.TenCauTraLoi;
                else
                    item.TenVanTat = cautraloi.TenVanTat;

                context.ReadCommited();
                context.SaveChanges();
            }
        }

        public static CauTraLoi CauTraLoiGet(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();

                return context.CauTraLois.First(x => x.ID == id);
            }
        }

        public static void CauTraLoiDel(int id)
        {
            using (var context = new DataModelEntities())
            {
                var item = context.CauTraLois.First(x => x.ID == id);
                if (item.Active != null)
                {
                    item.Active = !item.Active;
                }
                else
                {
                    item.Active = true;
                }
                context.SaveChanges();
            }
        }

        #endregion Câu trả lời

        #region Tiêu chí và câu trả lời

        public static void TieuChiCauTraLoiCreate(int tieuChiID, List<int> dsCauTraLoi)
        {
            using (var context = new DataModelEntities())
            {
                context.TieuChiCauTraLois.RemoveRange(context.TieuChiCauTraLois.Where(x => x.TieuChiID == tieuChiID));
                foreach (var item in dsCauTraLoi)
                {
                    TieuChiCauTraLoi tieuchicautraloi = new TieuChiCauTraLoi();
                    tieuchicautraloi.CauTraLoiID = item;
                    tieuchicautraloi.TieuChiID = tieuChiID;

                    context.TieuChiCauTraLois.Add(tieuchicautraloi);
                    context.ReadCommited();
                    context.SaveChanges();
                }
            }
        }

        public static List<int> TieuChiCauTraLoiGetList_ByTieuChiID(int tieuChiID)
        {
            using (var context = new DataModelEntities())
            {
                return context.TieuChiCauTraLois.Where(x => x.TieuChiID == tieuChiID).Select(x => x.CauTraLoiID.Value).ToList();
            }
        }

        public static List<TieuChiCauTraLoi> TieuChiCauTraLoiGetAll_ByTieuChiID(int tieuChiID, bool sortTangDan = true)
        {
            using (var context = new DataModelEntities())
            {
                if (!sortTangDan)
                {
                    return context.TieuChiCauTraLois.Include(x => x.CauTraLoi).Include(x => x.TieuChi).Where(x => x.TieuChiID == tieuChiID).OrderBy(x => x.CauTraLoi.GiaTri).ToList();
                }
                else
                    return context.TieuChiCauTraLois.Include(x => x.CauTraLoi).Include(x => x.TieuChi).Where(x => x.TieuChiID == tieuChiID).OrderByDescending(x => x.CauTraLoi.GiaTri).ToList();
            }
        }

        public static List<TieuChiCauTraLoi> TieuChiCauTraLoiGetAll()
        {
            using (var context = new DataModelEntities())
            {
                return context.TieuChiCauTraLois.Include(x => x.CauTraLoi).Include(x => x.TieuChi).ToList();
            }
        }

        #endregion Tiêu chí và câu trả lời

        #region InputType

        public static List<InputTypeTieuChi> InputTypeGetAllList()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.InputTypeTieuChis.ToList();
            }
        }

        #endregion InputType

        #region DonVi - LinhVuc

        public static List<LinhVucDonVi> DonViLinhVucGetAllList()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.LinhVucDonVis.Where(x => x.Active == true).ToList();
            }
        }

        public static int DonViLinhVucCreate(LinhVucDonVi donVi)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                context.LinhVucDonVis.Add(donVi);
                context.SaveChanges();
                return donVi.ID;
            }
        }

        public static void DonViLinhVucListCreate(List<LinhVucDonVi> lst)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                context.LinhVucDonVis.AddRange(lst);
                context.SaveChanges();
            }
        }

        public static LinhVucDonVi DonViLinhVucGetById(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.LinhVucDonVis.Find(id);
            }
        }

        public static void DonViLinhVucUpdate(LinhVucDonVi donVi)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                var item = context.LinhVucDonVis.Find(donVi.ID);
                item.DonViID = donVi.DonViID;
                item.LinhVucID = donVi.LinhVucID;

                context.SaveChanges();
            }
        }

        public static void DonViLinhVucDelete(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                var item = context.LinhVucDonVis.Find(id);
                item.Active = !item.Active;
                context.SaveChanges();
            }
        }

        public static void LinhVucDonViDeleteAll()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                context.LinhVucDonVis.RemoveRange(context.LinhVucDonVis);

                context.SaveChanges();
            }
        }

        public static void DonViLinhVucGetByLinhVucId(int id, out List<string> lstDonViID)
        {
            lstDonViID = new List<string>();
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                var item = context.LinhVucDonVis.Where(x => x.LinhVucID == id && x.Active == true);
                foreach (var it in item)
                {
                    lstDonViID.Add(it.DonViID.Value.ToString());
                }
            }
        }

        public static void DonViLinhVucDeleteByLinhVucID(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                var item = context.LinhVucDonVis.Where(x => x.LinhVucID == id);
                foreach (var it in item)
                {
                    it.Active = false;
                }
                context.SaveChanges();
            }
        }

        public static void DonViLinhVucUpdateByLinhVucId(int id, List<string> lstDonViID)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                var item = context.LinhVucDonVis.Where(x => x.LinhVucID == id).ToList();
                foreach (var it in lstDonViID)
                {
                    if (item.Where(x => x.DonViID.Value.ToString() == it).Count() > 0)
                    {
                        item.Where(x => x.DonViID.Value.ToString() == it).FirstOrDefault().Active = true;
                    }
                    else
                    {
                        var tmp = new LinhVucDonVi();
                        tmp.Active = true;
                        tmp.LinhVucID = id;
                        tmp.DonViID = int.Parse(it);
                        context.LinhVucDonVis.Add(tmp);
                    }
                }
                context.SaveChanges();
            }
        }

        #endregion DonVi - LinhVuc

        #region DanhMucThongKe

        /// <summary>
        /// Get list năm
        /// </summary>
        /// <returns></returns>
        public static List<Nam_BaoCao> Nam_BaoCao_getList()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.Nam_BaoCao.ToList();
            }
        }

        /// <summary>
        /// Get quý theo năm id
        /// </summary>
        /// <param name="namID"></param>
        /// <returns></returns>
        public static List<Quy_BaoCao> Quy_BaoCao_getListByNamID(int namID)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.Quy_BaoCao.Where(x => x.NamID == namID).ToList();
            }
        }

        /// <summary>
        /// Get list Tháng bat buột phải có nămID
        /// </summary>
        /// <param name="namID"></param>
        /// <returns></returns>
        public static List<Thang_BaoCao> Thang_BaoCao_getList(int namID, int? quyID)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                var query = context.Thang_BaoCao.Where(x => x.NamID == namID).ToList();

                if (quyID != 0)
                {
                    query = query.Where(x => x.NamID == namID && x.QuyID == quyID).ToList();
                }

                return query;
            }
        }

        /// <summary>
        /// List Tuần bất buột phải có nămID
        /// </summary>
        /// <param name="namID"></param>
        /// <param name="quyID"></param>
        /// <param name="thangID"></param>
        /// <returns></returns>
        public static List<Tuan_BaoCao> Tuan_BaoCao_getList(int namID, int? quyID, int? thangID)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                var query = context.Tuan_BaoCao.Where(x => x.Year == namID).ToList();

                if (quyID != 0 && thangID == 0)
                {
                    query = query.Where(x => x.Year == namID && x.Quarter == quyID).ToList();
                }

                if (quyID != 0 && thangID != 0)
                {
                    query = query.Where(x => x.Year == namID && x.Quarter == quyID && x.Month == thangID).ToList();
                }

                if (quyID == 0 && thangID != 0)
                {
                    query = query.Where(x => x.Year == namID && x.Month == thangID).ToList();
                }

                return query;
            }
        }

        #endregion DanhMucThongKe

        public static void ClearDataBase()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                context.Database.ExecuteSqlCommand("DELETE dbo.QuaTrinhXuLy");
                context.Database.ExecuteSqlCommand("DELETE dbo.HoSo");
                context.Database.ExecuteSqlCommand("DELETE dbo.KetQuaDanhGia");
                context.Database.ExecuteSqlCommand("DELETE dbo.DanhGia");

                context.SaveChanges();
            }
        }
    }
}