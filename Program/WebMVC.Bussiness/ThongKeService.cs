using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using WebMVC.Dal;
using WebMVC.Dal.Extensions;
using WebMVC.Entities;

namespace WebMVC.Bussiness
{
    public class ThongKeService
    {
        #region Thống kê theo đơn vị

        /// <summary>
        /// Thông kê đơn vị theo năm
        /// </summary>
        /// <param name="namID"></param>
        /// <returns></returns>

        public static List<TK_DonVi_Nam> Nam_BaoCao_getList(int namID)
        {
            var lstResult = new List<TK_DonVi_Nam>();

            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                lstResult = context.TK_DonVi_Nam.Where(x => x.NamID == namID).ToList();
                return lstResult;
            }
        }

        /// <summary>
        /// Danh sách hồ sơ theo đơn vị, lĩnh vực, thủ tục
        /// </summary>
        /// <param name="donViID"></param>
        /// <param name="linhVucID"></param>
        /// <param name="thuTucID"></param>
        /// <returns></returns>
        public static List<HoSo> DanhSachHoSo(int donViID, int linhVucID, int thuTucID)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.HoSoes.Where(x => x.DonViID == donViID && x.LinhVucID == linhVucID && x.ThuTucID == thuTucID && x.DaDanhGia == true).OrderByDescending(x => x.NgayNhan).ToList();
            }
        }

        /// <summary>
        /// Thông tin hồ sơ theo số biên nhận
        /// </summary>
        /// <param name="soBienNhan"></param>
        /// <returns></returns>
        public static HoSo ThongTinHoSoBySoBienNhan(string soBienNhan)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.HoSoes.Where(x => x.SoBienNhan == soBienNhan).FirstOrDefault();
            }
        }

        /// <summary>
        /// Quá trình xử lý theo hồ sơ id
        /// </summary>
        /// <param name="hoSoID"></param>
        /// <returns></returns>
        public static List<QuaTrinhXuLy> QuaTrinhXuLyByHoSoID(string hoSoID)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.QuaTrinhXuLies.Where(x => x.HoSoID == hoSoID).ToList();
            }
        }

        /// <summary>
        /// Thống kê đơn vị
        /// </summary>
        /// <param name="namID"></param>
        /// <param name="quyID"></param>
        /// <param name="thangID"></param>
        /// <param name="tuanID"></param>
        /// <returns></returns>
        public static Tuple<List<TK_DonVi_Nam>, List<TK_DonVi_Quy>, List<TK_DonVi_Thang>, List<TK_DonVi_Tuan>> DonVi_BaoCao_getList(int? namID, int? quyID, int? thangID, int? tuanID)
        {
            using (var context = new DataModelEntities())
            {
                var tkNam = new List<TK_DonVi_Nam>();
                var tkQuy = new List<TK_DonVi_Quy>();
                var tkThang = new List<TK_DonVi_Thang>();
                var tkTuan = new List<TK_DonVi_Tuan>();
                context.ReadUncommited();

                if (tuanID > 0)
                {
                    tkTuan = context.TK_DonVi_Tuan.Where(x => x.TuanID == tuanID).OrderBy(x => x.TenDonVi).ToList();
                }
                else if (thangID > 0)
                {
                    tkThang = context.TK_DonVi_Thang.Where(x => x.ThangID == thangID).OrderBy(x => x.TenDonVi).ToList();
                }
                else if (quyID > 0)
                {
                    tkQuy = context.TK_DonVi_Quy.Where(x => x.QuiID == quyID).OrderBy(x => x.TenDonVi).ToList();
                }
                else if (namID > 0)
                {
                    tkNam = context.TK_DonVi_Nam.Where(x => x.NamID == namID).OrderBy(x => x.TenDonVi).ToList();
                }

                return Tuple.Create(tkNam, tkQuy, tkThang, tkTuan);
            }
        }

        /// <summary>
        /// Thống kê đơn vị từ ngày ,đến ngày
        /// </summary>
        /// <returns></returns>
        public static List<ThongKeDonVi_TuNgay_DenNgay_Result> DonVi_BaoCao_Day(string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));

                var result = context.Database.SqlQuery<ThongKeDonVi_TuNgay_DenNgay_Result>("ThongKeDonVi_TuNgay_DenNgay @TuNgay,@DenNgay",
                    tuNgayParameter, denNgayParameter).ToList();

                return result;
            }
        }

        #endregion Thống kê theo đơn vị

        #region Thong Ke Toan Thanh Pho

        /// <summary>
        /// Thống kê biểu đồ tròn toàn thành phố
        /// </summary>
        /// <param name="nam"></param>
        /// <returns></returns>
        public static GetThongKeToanTP_Result ThongKeToanThanhPho(int nam)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.GetThongKeToanTP(nam).FirstOrDefault();
            }
        }

        #endregion Thong Ke Toan Thanh Pho

        #region Thong Ke Toan Thanh Pho - Don Vi

        /// <summary>
        /// Thống kê tỷ lệ hài lòng toàn thành phố theo năm
        /// </summary>
        /// <param name="nam"></param>
        /// <returns></returns>

        public static List<GetThongKeToanTP_DonVi_Nam_Result> ThongKeToanThanhPho_Nam(int nam)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.GetThongKeToanTP_DonVi_Nam(nam).OrderByDescending(x => x.HaiLong).ToList();
            }
        }

        /// <summary>
        /// Thống kê tỷ lệ hài lòng toàn thành phố theo tháng
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="thang"></param>
        /// <returns></returns>
        public static List<GetThongKeToanTP_DonVi_Thang_Result> ThongKeToanThanhPho_Thang(int nam, int thang)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.GetThongKeToanTP_DonVi_Thang(nam, thang).OrderByDescending(x => x.HaiLong).ToList();
            }
        }

        /// <summary>
        /// Thống kê tỷ lệ hài lòng toàn thành phố theo quý
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="quy"></param>
        /// <returns></returns>
        public static List<GetThongKeToanTP_DonVi_Quy_Result> ThongKeToanThanhPho_Quy(int nam, int quy)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.GetThongKeToanTP_DonVi_Quy(nam, quy).OrderByDescending(x => x.HaiLong).ToList();
            }
        }

        /// <summary>
        /// Thống kê tỷ lệ hài lòng toàn thành phố theo tuần
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="thang"></param>
        /// <param name="tuan"></param>
        /// <returns></returns>
        public static List<TK_TongHop_Tuan> ThongKeToanThanhPho_Tuan(int nam, int thang, int tuan)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.TK_TongHop_Tuan.Where(x => x.NamID == nam && x.ThangID == thang && x.TuanID == tuan).OrderByDescending(x => x.HaiLong).ToList();
            }
        }

        #endregion Thong Ke Toan Thanh Pho - Don Vi

        #region Thong ke linh vuc

        /// <summary>
        /// Thống kê lĩnh vực theo năm, quý, tháng, tuần
        /// </summary>
        /// <param name="donviID"></param>
        /// <param name="namID"></param>
        /// <param name="quyID"></param>
        /// <param name="thangID"></param>
        /// <param name="tuanID"></param>
        /// <returns></returns>
        public static Tuple<List<TK_LinhVuc_Nam>, List<TK_LinhVuc_Quy>, List<TK_LinhVuc_Thang>, List<TK_LinhVuc_Tuan>> LinhVuc_BaoCao_getList(int donviID, int? namID, int? quyID, int? thangID, int? tuanID)
        {
            using (var context = new DataModelEntities())
            {
                var tkNam = new List<TK_LinhVuc_Nam>();
                var tkQuy = new List<TK_LinhVuc_Quy>();
                var tkThang = new List<TK_LinhVuc_Thang>();
                var tkTuan = new List<TK_LinhVuc_Tuan>();
                context.ReadUncommited();

                if (tuanID > 0)
                {
                    tkTuan = context.TK_LinhVuc_Tuan.Where(x => x.TuanID == tuanID && x.DonViID == donviID).OrderBy(x => x.TenLinhVuc).ToList();
                }
                else if (thangID > 0)
                {
                    tkThang = context.TK_LinhVuc_Thang.Where(x => x.ThangID == thangID && x.DonViID == donviID).OrderBy(x => x.TenLinhVuc).ToList();
                }
                else if (quyID > 0)
                {
                    tkQuy = context.TK_LinhVuc_Quy.Where(x => x.QuyID == quyID && x.DonViID == donviID).OrderBy(x => x.TenLinhVuc).ToList();
                }
                else if (namID > 0)
                {
                    tkNam = context.TK_LinhVuc_Nam.Where(x => x.NamID == namID && x.DonViID == donviID).OrderBy(x => x.TenLinhVuc).ToList();
                }

                return Tuple.Create(tkNam, tkQuy, tkThang, tkTuan);
            }
        }

        /// <summary>
        /// Thống kê lĩnh vực từ ngày đến ngày
        /// </summary>
        /// <param name="donviID"></param>
        /// <param name="tuNgay"></param>
        /// <param name="denNgay"></param>
        /// <returns></returns>
        public static List<ThongKeLinhVuc_TuNgay_DenNgay_Result> LinhVuc_BaoCao_Day(int donviID, string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                int DonViID = donviID;
                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));
                var donviIDParameter = new SqlParameter("donviID", DonViID);
                var result = context.Database.SqlQuery<ThongKeLinhVuc_TuNgay_DenNgay_Result>
                    ("ThongKeLinhVuc_TuNgay_DenNgay @DonViID,@TuNgay,@DenNgay", donviIDParameter,
                    tuNgayParameter, denNgayParameter).ToList();

                return result;
            }
        }

        #endregion Thong ke linh vuc

        #region Thong ke thu tuc

        /// <summary>
        /// Thống kê thủ tục theo năm, quý, tháng, tuần
        /// </summary>
        /// <param name="donviID"></param>
        /// <param name="linhVucID"></param>
        /// <param name="namID"></param>
        /// <param name="quyID"></param>
        /// <param name="thangID"></param>
        /// <param name="tuanID"></param>
        /// <returns></returns>
        public static Tuple<List<TK_ThuTuc_Nam>, List<TK_ThuTuc_Quy>, List<TK_ThuTuc_Thang>, List<TK_ThuTuc_Tuan>> ThuTuc_BaoCao_getList(int donviID, int linhVucID, int? namID, int? quyID, int? thangID, int? tuanID)
        {
            using (var context = new DataModelEntities())
            {
                var tkNam = new List<TK_ThuTuc_Nam>();
                var tkQuy = new List<TK_ThuTuc_Quy>();
                var tkThang = new List<TK_ThuTuc_Thang>();
                var tkTuan = new List<TK_ThuTuc_Tuan>();
                context.ReadUncommited();

                if (tuanID > 0)
                {
                    tkTuan = context.TK_ThuTuc_Tuan.Where(x => x.TuanID == tuanID && x.DonViID == donviID && x.LinhVucID == linhVucID).OrderBy(x => x.TenThuTuc).ToList();
                }
                else if (thangID > 0)
                {
                    tkThang = context.TK_ThuTuc_Thang.Where(x => x.ThangID == thangID && x.DonViID == donviID && x.LinhVucID == linhVucID).OrderBy(x => x.TenThuTuc).ToList();
                }
                else if (quyID > 0)
                {
                    tkQuy = context.TK_ThuTuc_Quy.Where(x => x.QuyID == quyID && x.DonViID == donviID && x.LinhVucID == linhVucID).OrderBy(x => x.TenThuTuc).ToList();
                }
                else if (namID > 0)
                {
                    tkNam = context.TK_ThuTuc_Nam.Where(x => x.NamID == namID && x.DonViID == donviID && x.LinhVucID == linhVucID).OrderBy(x => x.TenThuTuc).ToList();
                }

                return Tuple.Create(tkNam, tkQuy, tkThang, tkTuan);
            }
        }

        /// <summary>
        /// Thống kê thủ tục từ ngày đến ngày
        /// </summary>
        /// <param name="donviID"></param>
        /// <param name="linhVucID"></param>
        /// <param name="tuNgay"></param>
        /// <param name="denNgay"></param>
        /// <returns></returns>
        public static List<ThongKeThuTuc_TuNgay_DenNgay_Result> ThuTuc_BaoCao_Day(int donviID, int linhVucID, string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                int DonViID = donviID;
                int LinhVucID = linhVucID;
                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));
                var donviIDParameter = new SqlParameter("donviID", DonViID);
                var linhVucIDParameter = new SqlParameter("LinhVucID", LinhVucID);
                var result = context.Database.SqlQuery<ThongKeThuTuc_TuNgay_DenNgay_Result>
                    ("ThongKeThuTuc_TuNgay_DenNgay @DonviID,@LinhVucID,@TuNgay,@DenNgay", donviIDParameter, linhVucIDParameter,
                    tuNgayParameter, denNgayParameter).ToList();

                return result;
            }
        }

        #endregion Thong ke thu tuc

        /// <summary>
        /// Lấy kết quả đánh giá từng hồ sơ
        /// </summary>
        /// <param name="DanhGiaID"></param>
        /// <returns></returns>
        public static List<KetQuaDanhGiaByDanhGiaID_Result> KetQuaDanhGiaByDanhGiaID(string DanhGiaID)
        {
            using (var context = new DataModelEntities())
            {
                var danhgia = new SqlParameter("DanhGiaID", DanhGiaID);
                var result = context.Database.SqlQuery<KetQuaDanhGiaByDanhGiaID_Result>("KetQuaDanhGiaByDanhGiaID @DanhGiaID", danhgia).ToList();
                return result;
            }

        }
        /// <summary>
        /// Thông tin người xử lý hồ sơ
        /// </summary>
        /// <param name="hoSoID"></param>
        /// <returns></returns>
        public static List<ThongTinNguoiXuLy> ThongTinNguoiXuLyByHoSoID(string hoSoID)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.ThongTinNguoiXuLies.Where(x => x.HoSoID == hoSoID).ToList();
            }
        }
        public static List<ThongKeGopYCauHoi_Result> ThongKeGopYCauHoi()
        {
            using (var context = new DataModelEntities())
            {
                var result = context.Database.SqlQuery<ThongKeGopYCauHoi_Result>("ThongKeGopYCauHoi").ToList();
                return result;
            }
        }
        public static List<ThongKeGopYPhanMem_Result> ThongKeGopYPhanMem()
        {
            using (var context = new DataModelEntities())
            {
                var result = context.Database.SqlQuery<ThongKeGopYPhanMem_Result>("ThongKeGopYPhanMem").ToList();
                return result;
            }
        }
        public static List<ThongKeGopYPhanMemChiTiet_Result> ThongKeGopYPhanMemChiTiet(int idCau)
        {
            using (var context = new DataModelEntities())
            {
                var idcau = new SqlParameter("idCau", idCau);
                var result = context.Database.SqlQuery<ThongKeGopYPhanMemChiTiet_Result>("ThongKeGopYPhanMemChiTiet @idCau", idcau).ToList();
                return result;
            }
        }

        #region Write New Thong Ke
        /// <summary>
        /// Biểu đồ thống kê toàn Tp theo tỷ lệ hài lòng, không hài lòng, bình thường theo thời gian
        /// </summary>
        public static ThongKe ThongKeToanTP_ByTime(string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));

                var result = context.Database.SqlQuery<ThongKe>("ThongKeToanTP_ByTime @TuNgay,@DenNgay",
                    tuNgayParameter, denNgayParameter).FirstOrDefault();

                return result;
            }
        }

        /// <summary>
        /// Biểu đồ thống kê toàn Tp theo tỷ lệ hài lòng, không hài lòng, bình thường theo thời gian
        /// Biểu đồ thống kê tỷ lệ hài lòng, không hài lòng, bình thường theo thời gian của từng đơn vị
        /// </summary>
        public static List<ThongKe> ThongKeToanTP_BanBieu_ByTime(string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));

                var result = context.Database.SqlQuery<ThongKe>("ThongKeToanTP_BanBieu_ByTime @TuNgay,@DenNgay",
                    tuNgayParameter, denNgayParameter).ToList();

                return result;
            }
        }
        /// <summary>
        /// Thống kê 10 đơn vị có tỷ lệ hài lòng cao nhất
        /// </summary>
        public static List<ThongKe> ThongKeToanTP_DonVi_ByTime_10Top(string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));

                var result = context.Database.SqlQuery<ThongKe>("ThongKeToanTP_DonVi_ByTime_10Top @TuNgay,@DenNgay",
                    tuNgayParameter, denNgayParameter).ToList();

                return result;
            }
        }

        /// <summary>
        /// Thống kê 10 đơn vị có tỷ lệ hài lòng thấp nhất
        /// </summary>
        public static List<ThongKe> ThongKeToanTP_DonVi_ByTime_10Bottom(string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));

                var result = context.Database.SqlQuery<ThongKe>("ThongKeToanTP_DonVi_ByTime_10Bottom @TuNgay,@DenNgay",
                    tuNgayParameter, denNgayParameter).ToList();

                return result;
            }
        }
        /// <summary>
        /// Thống kê tất  cả đơn vị
        /// </summary>
        public static List<ThongKe> ThongKeToanTP_DonVi_ByTime_All(string tuNgay, string denNgay, string name, string sort)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));
                var nameParameter = new SqlParameter("Name", name);
                var sortParameter = new SqlParameter("Sort", sort);
                var result = context.Database.SqlQuery<ThongKe>("ThongKeToanTP_DonVi_ByTime_All @TuNgay,@DenNgay,@Name,@Sort",
                    tuNgayParameter, denNgayParameter, nameParameter, sortParameter).ToList();

                return result;
            }
        }
        /// <summary>
        /// Biểu đồ thống kê tỷ lệ hài lòng, không hài lòng, bình thường theo thời gian của từng đơn vị
        /// </summary>
        public static ThongKe ThongKeToanTP_DonVi_ByTime(string tuNgay, string denNgay, string maDonVi)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));
                var maDonViParameter = new SqlParameter("MaDV", maDonVi);

                var result = context.Database.SqlQuery<ThongKe>("ThongKeToanTP_DonVi_ByTime @TuNgay,@DenNgay,@MaDV",
                    tuNgayParameter, denNgayParameter, maDonViParameter).FirstOrDefault();

                return result;
            }
        }
        /// <summary>
        /// Bản biểu thống kê theo tỷ lệ hài lòng, không hài lòng, bình thường theo thời gian  của từng đơn vị
        /// </summary>
        public static List<ThongKe> ThongKeToanTP_DonVi_ByDonVi_ByTime(string tuNgay, string denNgay, string donViID)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));
                var donViIDParameter = new SqlParameter("DonVi", donViID);

                var result = context.Database.SqlQuery<ThongKe>("ThongKeToanTP_DonVi_ByDonVi_ByTime @TuNgay,@DenNgay,@DonVi",
                    tuNgayParameter, denNgayParameter, donViIDParameter).ToList();

                return result;
            }
        }


        /// <summary>
        /// Báo cáo thống kê theo các nhóm tiêu chí của toàn thành phố
        /// </summary>
        public static List<ThongKe> ThongKeNhomTieuChiTP_ByTime(string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));

                var result = context.Database.SqlQuery<ThongKe>("ThongKeNhomTieuChiTP_ByTime @TuNgay,@DenNgay",
                    tuNgayParameter, denNgayParameter).ToList();

                return result;
            }
        }

        /// <summary>
        /// Báo cáo thống kê theo các nhóm tiêu chí theo từng đơn vị
        /// </summary>
        public static List<ThongKe> ThongKeNhomTieuChiDonVi_ByTime(string tuNgay, string denNgay, string donViID)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));
                var donViIDParameter = new SqlParameter("DonVi", donViID);

                var result = context.Database.SqlQuery<ThongKe>("ThongKeNhomTieuChiDonVi_ByTime @TuNgay,@DenNgay,@DonVi",
                    tuNgayParameter, denNgayParameter, donViIDParameter).ToList();

                return result;
            }
        }
        /// <summary>
        /// Thống kê 2 thang gan nhat
        /// </summary>
        public static List<ThongKe> ThongKeToanTP_BanBieu_ThreeMonth(string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));

                var result = context.Database.SqlQuery<ThongKe>("ThongKeToanTP_BanBieu_ThreeMonth @TuNgay,@DenNgay",
                    tuNgayParameter, denNgayParameter).ToList();

                return result;
            }
        }
        /// <summary>
        /// Detail month/ year
        /// </summary>
        public static List<ThongKe> ThongKeToanTP_BanBieu_ByMonthYear(int month, int year)
        {
            using (var context = new DataModelEntities())
            {

                var monthParameter = new SqlParameter("Month", month);
                var yearParameter = new SqlParameter("Year", year);

                var result = context.Database.SqlQuery<ThongKe>("ThongKeToanTP_BanBieu_ByMonthYear @Month,@Year",
                    monthParameter, yearParameter).ToList();

                return result;
            }
        }
        /// <summary>
        /// Detail month/ year
        /// </summary>
        public static List<ThongKe> ThongKeToanTP_BanBieu_ByDonViMonthYear(string madonvi, int month, int year)
        {
            using (var context = new DataModelEntities())
            {
                var mdvParameter = new SqlParameter("MaDonVi", madonvi);
                var monthParameter = new SqlParameter("Month", month);
                var yearParameter = new SqlParameter("Year", year);

                var result = context.Database.SqlQuery<ThongKe>("ThongKeToanTP_BanBieu_ByDonViMonthYear @MaDonVi,@Month,@Year", mdvParameter, monthParameter, yearParameter).ToList();

                return result;
            }
        }
        public static List<ThongKe> ThongKeNoiDungGopYPhanMem(string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));

                var result = context.Database.SqlQuery<ThongKe>("ThongKeNoiDungGopYPhanMem @TuNgay,@DenNgay",
                    tuNgayParameter, denNgayParameter).ToList();

                return result;
            }
        }

        public static List<ThongKe> ThongKeToanTP_3Thang_BieuDoCot(string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));

                var result = context.Database.SqlQuery<ThongKe>("ThongKeToanTP_3Thang_BieuDoCot @TuNgay,@DenNgay",
                    tuNgayParameter, denNgayParameter).ToList();

                return result;
            }
        }
        public static List<ThongKe> ThongKeNoiDungGopYCauHoi(string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));

                var result = context.Database.SqlQuery<ThongKe>("ThongKeNoiDungGopYCauHoi @TuNgay,@DenNgay",
                    tuNgayParameter, denNgayParameter).ToList();

                return result;
            }
        }
        public static ThongKe ThongKeTyLeGopYPhanMem(string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));

                var result = context.Database.SqlQuery<ThongKe>("ThongKeTyLeGopYPhanMem @TuNgay,@DenNgay",
                    tuNgayParameter, denNgayParameter).FirstOrDefault();

                return result;
            }
        }
        public static ThongKe ThongKeNoiDungGopYPhanMemResult(string tuNgay, string denNgay)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));

                var result = context.Database.SqlQuery<ThongKe>("ThongKeNoiDungGopYPhanMemResult @TuNgay,@DenNgay",
                    tuNgayParameter, denNgayParameter).FirstOrDefault();

                return result;
            }
        }

        #endregion

        /// <summary>
        /// Bản biểu thống kê theo tỷ lệ hài lòng, không hài lòng, bình thường theo thời gian  của từng đơn vị
        /// </summary>
        public static List<ThongKe> ThongKe_TheoDonVi_ByDonVi_ByTime(string tuNgay, string denNgay, string userName)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));
                var userNameParameter = new SqlParameter("userName", userName);

                var result = context.Database.SqlQuery<ThongKe>("ThongKe_DonVi_ByDonVi_ByTime @TuNgay,@DenNgay,@userName",
                    tuNgayParameter, denNgayParameter, userNameParameter).ToList();

                return result;
            }
        }
        /// <summary>
        /// Biểu đồ thống kê tỷ lệ hài lòng, không hài lòng, bình thường theo thời gian của từng đơn vị
        /// </summary>
        public static ThongKe ThongKe_TheoDonVi_ByTime(string tuNgay, string denNgay, string username)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));
                var usernameParameter = new SqlParameter("userName", username);

                var result = context.Database.SqlQuery<ThongKe>("ThongKe_DonVi_ByTime @TuNgay,@DenNgay,@userName",
                    tuNgayParameter, denNgayParameter, usernameParameter).FirstOrDefault();

                return result;
            }
        }
        public static List<ThongKe> ThongKeSoLuotDanhGia(string tuNgay, string denNgay, string username)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("TuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("DenNgay", denNgayDate) : new SqlParameter("DenNgay", typeof(DateTime));
                var usernameParameter = new SqlParameter("UserName", username);

                var result = context.Database.SqlQuery<ThongKe>("ThongKeSoLuotDanhGia @TuNgay,@DenNgay,@UserName",
                    tuNgayParameter, denNgayParameter, usernameParameter).ToList();

                return result;
            }
        }
        public static List<ThongKe> ThongKeSoLuotDanhGia_By_DonVi(string tuNgay, string denNgay, string madonvi, string malienthong, string username)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("TuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("DenNgay", denNgayDate) : new SqlParameter("DenNgay", typeof(DateTime));
                var madonviParameter = new SqlParameter("MaDonVi", madonvi);
                var malienthongParameter = new SqlParameter("MaLienThong", malienthong);
                var usernameParameter = new SqlParameter("UserName", username);

                var result = context.Database.SqlQuery<ThongKe>("ThongKeSoLuotDanhGia_By_DonVi @TuNgay,@DenNgay,@MaDonVi,@MaLienThong,@UserName",
                    tuNgayParameter, denNgayParameter, madonviParameter, malienthongParameter, usernameParameter).ToList();

                return result;
            }
        }
        public static List<KetQuaDanhGiaHoSo> KetQuaDanhGiaHosoByDanhGiaID(string DanhGiaID)
        {
            using (var context = new DataModelEntities())
            {
                var danhgia = new SqlParameter("DanhGiaID", DanhGiaID);
                var result = context.Database.SqlQuery<KetQuaDanhGiaHoSo>("KetQuaDanhGiaHosoByDanhGiaID @DanhGiaID", danhgia).ToList();
                return result;
            }
        }
        public static ThongTinHoSo KetQuaDanhGiaThongTinHoSoByDanhGiaID(string DanhGiaID, string hoso)
        {
            using (var context = new DataModelEntities())
            {
                var danhgia = new SqlParameter("DanhGiaID", DanhGiaID);
                var result = context.Database.SqlQuery<ThongTinHoSo>("KetQuaDanhGiaThongTinHoSoByDanhGiaID @DanhGiaID", danhgia).FirstOrDefault();
                return result;
            }
        }
        public static List<ThongKe> ThongKeToanTP_BanBieu_ThreeMonthDonVi(string tuNgay, string denNgay, string userName)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));
                var usernameParameter = new SqlParameter("UserName", userName);

                var result = context.Database.SqlQuery<ThongKe>("ThongKeToanTP_BanBieu_ThreeMonth_DonVi @TuNgay,@DenNgay,@UserName",
                    tuNgayParameter, denNgayParameter, usernameParameter).ToList();

                return result;
            }
        }
        public static List<ThongKe> ThongKeToanTP_3Thang_BieuDoCot_DonVi(string tuNgay, string denNgay, string userName)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));
                var usernameParameter = new SqlParameter("UserName", userName);

                var result = context.Database.SqlQuery<ThongKe>("ThongKeToanTP_3Thang_BieuDoCot_DonVi @TuNgay,@DenNgay,@UserName",
                    tuNgayParameter, denNgayParameter, usernameParameter).ToList();

                return result;
            }
        }
        public static List<ThongKe> ThongKeNhomTieuChiDonVi_ByTime_UserName(string tuNgay, string denNgay, string userName)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));
                var usernameParameter = new SqlParameter("UserName", userName);

                var result = context.Database.SqlQuery<ThongKe>("ThongKeNhomTieuChiDonVi_ByTime_UserName @TuNgay,@DenNgay,@UserName",
                    tuNgayParameter, denNgayParameter, usernameParameter).ToList();

                return result;
            }
        }
        public static List<ThongKe> ThongKeGopYTungDonVi(string tuNgay, string denNgay, string userName)
        {
            using (var context = new DataModelEntities())
            {
                DateTime tuNgayDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denNgayDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var tuNgayParameter = tuNgay != null ?
                          new SqlParameter("TuNgay", tuNgayDate) : new SqlParameter("tuNgay", typeof(DateTime));
                var denNgayParameter = denNgay != null ?
                     new SqlParameter("denNgay", denNgayDate) : new SqlParameter("denNgay", typeof(DateTime));
                var usernameParameter = new SqlParameter("UserName", userName);

                var result = context.Database.SqlQuery<ThongKe>("ThongKeGopYTungDonVi @TuNgay,@DenNgay,@UserName",
                    tuNgayParameter, denNgayParameter, usernameParameter).ToList();

                return result;
            }
        }
    }
}