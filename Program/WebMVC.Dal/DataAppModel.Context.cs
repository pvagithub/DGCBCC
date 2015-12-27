﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebMVC.Dal
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using WebMVC.Entities;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DataModelEntities : DbContext
    {
        public DataModelEntities()
            : base("name=DataModelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CauTraLoi> CauTraLois { get; set; }
        public virtual DbSet<DanhGia> DanhGias { get; set; }
        public virtual DbSet<DonVi> DonVis { get; set; }
        public virtual DbSet<HoSo> HoSoes { get; set; }
        public virtual DbSet<InputTypeTieuChi> InputTypeTieuChis { get; set; }
        public virtual DbSet<KetQuaDanhGia> KetQuaDanhGias { get; set; }
        public virtual DbSet<LinhVuc> LinhVucs { get; set; }
        public virtual DbSet<LinhVucDonVi> LinhVucDonVis { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
        public virtual DbSet<Nam_BaoCao> Nam_BaoCao { get; set; }
        public virtual DbSet<NhomTieuChi> NhomTieuChis { get; set; }
        public virtual DbSet<QuaTrinhXuLy> QuaTrinhXuLies { get; set; }
        public virtual DbSet<Quy_BaoCao> Quy_BaoCao { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Thang_BaoCao> Thang_BaoCao { get; set; }
        public virtual DbSet<ThongKeTongHop> ThongKeTongHops { get; set; }
        public virtual DbSet<ThongTinNguoiXuLy> ThongTinNguoiXuLies { get; set; }
        public virtual DbSet<ThuTuc> ThuTucs { get; set; }
        public virtual DbSet<TieuChi> TieuChis { get; set; }
        public virtual DbSet<TieuChiCauTraLoi> TieuChiCauTraLois { get; set; }
        public virtual DbSet<TK_DonVi_Nam> TK_DonVi_Nam { get; set; }
        public virtual DbSet<TK_DonVi_Quy> TK_DonVi_Quy { get; set; }
        public virtual DbSet<TK_DonVi_Thang> TK_DonVi_Thang { get; set; }
        public virtual DbSet<TK_DonVi_Tuan> TK_DonVi_Tuan { get; set; }
        public virtual DbSet<TK_LinhVuc_Nam> TK_LinhVuc_Nam { get; set; }
        public virtual DbSet<TK_LinhVuc_Quy> TK_LinhVuc_Quy { get; set; }
        public virtual DbSet<TK_LinhVuc_Thang> TK_LinhVuc_Thang { get; set; }
        public virtual DbSet<TK_LinhVuc_Tuan> TK_LinhVuc_Tuan { get; set; }
        public virtual DbSet<TK_ThuTuc_Nam> TK_ThuTuc_Nam { get; set; }
        public virtual DbSet<TK_ThuTuc_Quy> TK_ThuTuc_Quy { get; set; }
        public virtual DbSet<TK_ThuTuc_Thang> TK_ThuTuc_Thang { get; set; }
        public virtual DbSet<TK_ThuTuc_Tuan> TK_ThuTuc_Tuan { get; set; }
        public virtual DbSet<TK_TongHop_Nam> TK_TongHop_Nam { get; set; }
        public virtual DbSet<TK_TongHop_Quy> TK_TongHop_Quy { get; set; }
        public virtual DbSet<TK_TongHop_Thang> TK_TongHop_Thang { get; set; }
        public virtual DbSet<TK_TongHop_Tuan> TK_TongHop_Tuan { get; set; }
        public virtual DbSet<Tuan_BaoCao> Tuan_BaoCao { get; set; }
        public virtual DbSet<UsersInRole> UsersInRoles { get; set; }
    
        public virtual int GenTuanThongKeTheoNam(Nullable<int> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GenTuanThongKeTheoNam", yearParameter);
        }
    
        public virtual ObjectResult<GetThongKeToanTP_Result> GetThongKeToanTP(Nullable<int> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetThongKeToanTP_Result>("GetThongKeToanTP", yearParameter);
        }
    
        public virtual ObjectResult<GetThongKeToanTP_DonVi_Nam_Result> GetThongKeToanTP_DonVi_Nam(Nullable<int> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetThongKeToanTP_DonVi_Nam_Result>("GetThongKeToanTP_DonVi_Nam", yearParameter);
        }
    
        public virtual ObjectResult<GetThongKeToanTP_DonVi_Thang_Result> GetThongKeToanTP_DonVi_Thang(Nullable<int> year, Nullable<int> thang)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var thangParameter = thang.HasValue ?
                new ObjectParameter("Thang", thang) :
                new ObjectParameter("Thang", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetThongKeToanTP_DonVi_Thang_Result>("GetThongKeToanTP_DonVi_Thang", yearParameter, thangParameter);
        }
    
        public virtual ObjectResult<GetThongKeToanTP_DonVi_Quy_Result> GetThongKeToanTP_DonVi_Quy(Nullable<int> year, Nullable<int> quy)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var quyParameter = quy.HasValue ?
                new ObjectParameter("Quy", quy) :
                new ObjectParameter("Quy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetThongKeToanTP_DonVi_Quy_Result>("GetThongKeToanTP_DonVi_Quy", yearParameter, quyParameter);
        }
    
        public virtual int ThongKeDonVi_TuNgay_DenNgay(Nullable<System.DateTime> tuNgay, Nullable<System.DateTime> denNgay)
        {
            var tuNgayParameter = tuNgay.HasValue ?
                new ObjectParameter("TuNgay", tuNgay) :
                new ObjectParameter("TuNgay", typeof(System.DateTime));
    
            var denNgayParameter = denNgay.HasValue ?
                new ObjectParameter("DenNgay", denNgay) :
                new ObjectParameter("DenNgay", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeDonVi_TuNgay_DenNgay", tuNgayParameter, denNgayParameter);
        }
    
        public virtual int ThongKeLinhVuc_TuNgay_DenNgay(Nullable<int> donViID, Nullable<System.DateTime> tuNgay, Nullable<System.DateTime> denNgay)
        {
            var donViIDParameter = donViID.HasValue ?
                new ObjectParameter("DonViID", donViID) :
                new ObjectParameter("DonViID", typeof(int));
    
            var tuNgayParameter = tuNgay.HasValue ?
                new ObjectParameter("TuNgay", tuNgay) :
                new ObjectParameter("TuNgay", typeof(System.DateTime));
    
            var denNgayParameter = denNgay.HasValue ?
                new ObjectParameter("DenNgay", denNgay) :
                new ObjectParameter("DenNgay", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeLinhVuc_TuNgay_DenNgay", donViIDParameter, tuNgayParameter, denNgayParameter);
        }
    
        public virtual int ThongKeThuTuc_TuNgay_DenNgay(Nullable<int> donViID, Nullable<int> linhVucID, Nullable<System.DateTime> tuNgay, Nullable<System.DateTime> denNgay)
        {
            var donViIDParameter = donViID.HasValue ?
                new ObjectParameter("DonViID", donViID) :
                new ObjectParameter("DonViID", typeof(int));
    
            var linhVucIDParameter = linhVucID.HasValue ?
                new ObjectParameter("LinhVucID", linhVucID) :
                new ObjectParameter("LinhVucID", typeof(int));
    
            var tuNgayParameter = tuNgay.HasValue ?
                new ObjectParameter("TuNgay", tuNgay) :
                new ObjectParameter("TuNgay", typeof(System.DateTime));
    
            var denNgayParameter = denNgay.HasValue ?
                new ObjectParameter("DenNgay", denNgay) :
                new ObjectParameter("DenNgay", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeThuTuc_TuNgay_DenNgay", donViIDParameter, linhVucIDParameter, tuNgayParameter, denNgayParameter);
        }
    
        public virtual int GenTuanThongKeTheoNam_bak(Nullable<int> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GenTuanThongKeTheoNam_bak", yearParameter);
        }
    
        public virtual int Insert_ThongKeTongHop(Nullable<int> year, Nullable<decimal> haiLong, Nullable<decimal> khongHaiLong, Nullable<decimal> binhThuong)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var haiLongParameter = haiLong.HasValue ?
                new ObjectParameter("HaiLong", haiLong) :
                new ObjectParameter("HaiLong", typeof(decimal));
    
            var khongHaiLongParameter = khongHaiLong.HasValue ?
                new ObjectParameter("KhongHaiLong", khongHaiLong) :
                new ObjectParameter("KhongHaiLong", typeof(decimal));
    
            var binhThuongParameter = binhThuong.HasValue ?
                new ObjectParameter("BinhThuong", binhThuong) :
                new ObjectParameter("BinhThuong", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_ThongKeTongHop", yearParameter, haiLongParameter, khongHaiLongParameter, binhThuongParameter);
        }
    
        public virtual int InsertName_BaoCao(Nullable<int> nam, Nullable<System.DateTime> ngayDauNam, Nullable<System.DateTime> ngayCuoiNam, Nullable<int> createdUserID, Nullable<System.DateTime> createdDate, Nullable<int> lastUpdUserID, Nullable<System.DateTime> lastUpdDate)
        {
            var namParameter = nam.HasValue ?
                new ObjectParameter("Nam", nam) :
                new ObjectParameter("Nam", typeof(int));
    
            var ngayDauNamParameter = ngayDauNam.HasValue ?
                new ObjectParameter("NgayDauNam", ngayDauNam) :
                new ObjectParameter("NgayDauNam", typeof(System.DateTime));
    
            var ngayCuoiNamParameter = ngayCuoiNam.HasValue ?
                new ObjectParameter("NgayCuoiNam", ngayCuoiNam) :
                new ObjectParameter("NgayCuoiNam", typeof(System.DateTime));
    
            var createdUserIDParameter = createdUserID.HasValue ?
                new ObjectParameter("CreatedUserID", createdUserID) :
                new ObjectParameter("CreatedUserID", typeof(int));
    
            var createdDateParameter = createdDate.HasValue ?
                new ObjectParameter("CreatedDate", createdDate) :
                new ObjectParameter("CreatedDate", typeof(System.DateTime));
    
            var lastUpdUserIDParameter = lastUpdUserID.HasValue ?
                new ObjectParameter("LastUpdUserID", lastUpdUserID) :
                new ObjectParameter("LastUpdUserID", typeof(int));
    
            var lastUpdDateParameter = lastUpdDate.HasValue ?
                new ObjectParameter("LastUpdDate", lastUpdDate) :
                new ObjectParameter("LastUpdDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertName_BaoCao", namParameter, ngayDauNamParameter, ngayCuoiNamParameter, createdUserIDParameter, createdDateParameter, lastUpdUserIDParameter, lastUpdDateParameter);
        }
    
        public virtual int InsertQuy_BaoCao(Nullable<int> namID, Nullable<int> quy, Nullable<System.DateTime> ngayDauQuy, Nullable<System.DateTime> ngayCuoiQuy, Nullable<int> createdUserID, Nullable<System.DateTime> createdDate, Nullable<int> lastUpdUserID, Nullable<System.DateTime> lastUpdDate)
        {
            var namIDParameter = namID.HasValue ?
                new ObjectParameter("NamID", namID) :
                new ObjectParameter("NamID", typeof(int));
    
            var quyParameter = quy.HasValue ?
                new ObjectParameter("Quy", quy) :
                new ObjectParameter("Quy", typeof(int));
    
            var ngayDauQuyParameter = ngayDauQuy.HasValue ?
                new ObjectParameter("NgayDauQuy", ngayDauQuy) :
                new ObjectParameter("NgayDauQuy", typeof(System.DateTime));
    
            var ngayCuoiQuyParameter = ngayCuoiQuy.HasValue ?
                new ObjectParameter("NgayCuoiQuy", ngayCuoiQuy) :
                new ObjectParameter("NgayCuoiQuy", typeof(System.DateTime));
    
            var createdUserIDParameter = createdUserID.HasValue ?
                new ObjectParameter("CreatedUserID", createdUserID) :
                new ObjectParameter("CreatedUserID", typeof(int));
    
            var createdDateParameter = createdDate.HasValue ?
                new ObjectParameter("CreatedDate", createdDate) :
                new ObjectParameter("CreatedDate", typeof(System.DateTime));
    
            var lastUpdUserIDParameter = lastUpdUserID.HasValue ?
                new ObjectParameter("LastUpdUserID", lastUpdUserID) :
                new ObjectParameter("LastUpdUserID", typeof(int));
    
            var lastUpdDateParameter = lastUpdDate.HasValue ?
                new ObjectParameter("LastUpdDate", lastUpdDate) :
                new ObjectParameter("LastUpdDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertQuy_BaoCao", namIDParameter, quyParameter, ngayDauQuyParameter, ngayCuoiQuyParameter, createdUserIDParameter, createdDateParameter, lastUpdUserIDParameter, lastUpdDateParameter);
        }
    
        public virtual int InsertThang_BaoCao(Nullable<int> quyID, Nullable<int> namID, Nullable<int> thang, Nullable<System.DateTime> ngayDauThang, Nullable<System.DateTime> ngayCuoiThang, Nullable<int> createdUserID, Nullable<System.DateTime> createdDate, Nullable<int> lastUpdUserID, Nullable<System.DateTime> lastUpdDate)
        {
            var quyIDParameter = quyID.HasValue ?
                new ObjectParameter("QuyID", quyID) :
                new ObjectParameter("QuyID", typeof(int));
    
            var namIDParameter = namID.HasValue ?
                new ObjectParameter("NamID", namID) :
                new ObjectParameter("NamID", typeof(int));
    
            var thangParameter = thang.HasValue ?
                new ObjectParameter("Thang", thang) :
                new ObjectParameter("Thang", typeof(int));
    
            var ngayDauThangParameter = ngayDauThang.HasValue ?
                new ObjectParameter("NgayDauThang", ngayDauThang) :
                new ObjectParameter("NgayDauThang", typeof(System.DateTime));
    
            var ngayCuoiThangParameter = ngayCuoiThang.HasValue ?
                new ObjectParameter("NgayCuoiThang", ngayCuoiThang) :
                new ObjectParameter("NgayCuoiThang", typeof(System.DateTime));
    
            var createdUserIDParameter = createdUserID.HasValue ?
                new ObjectParameter("CreatedUserID", createdUserID) :
                new ObjectParameter("CreatedUserID", typeof(int));
    
            var createdDateParameter = createdDate.HasValue ?
                new ObjectParameter("CreatedDate", createdDate) :
                new ObjectParameter("CreatedDate", typeof(System.DateTime));
    
            var lastUpdUserIDParameter = lastUpdUserID.HasValue ?
                new ObjectParameter("LastUpdUserID", lastUpdUserID) :
                new ObjectParameter("LastUpdUserID", typeof(int));
    
            var lastUpdDateParameter = lastUpdDate.HasValue ?
                new ObjectParameter("LastUpdDate", lastUpdDate) :
                new ObjectParameter("LastUpdDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertThang_BaoCao", quyIDParameter, namIDParameter, thangParameter, ngayDauThangParameter, ngayCuoiThangParameter, createdUserIDParameter, createdDateParameter, lastUpdUserIDParameter, lastUpdDateParameter);
        }
    
        public virtual int InsertTuan_BaoCao(Nullable<int> year, Nullable<int> quarter, Nullable<int> month, Nullable<int> weekOfYear, Nullable<System.DateTime> startOfWeek, Nullable<System.DateTime> endOfWeek)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var quarterParameter = quarter.HasValue ?
                new ObjectParameter("Quarter", quarter) :
                new ObjectParameter("Quarter", typeof(int));
    
            var monthParameter = month.HasValue ?
                new ObjectParameter("Month", month) :
                new ObjectParameter("Month", typeof(int));
    
            var weekOfYearParameter = weekOfYear.HasValue ?
                new ObjectParameter("WeekOfYear", weekOfYear) :
                new ObjectParameter("WeekOfYear", typeof(int));
    
            var startOfWeekParameter = startOfWeek.HasValue ?
                new ObjectParameter("StartOfWeek", startOfWeek) :
                new ObjectParameter("StartOfWeek", typeof(System.DateTime));
    
            var endOfWeekParameter = endOfWeek.HasValue ?
                new ObjectParameter("EndOfWeek", endOfWeek) :
                new ObjectParameter("EndOfWeek", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertTuan_BaoCao", yearParameter, quarterParameter, monthParameter, weekOfYearParameter, startOfWeekParameter, endOfWeekParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int ThongKeDonVi_Nam(Nullable<int> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeDonVi_Nam", yearParameter);
        }
    
        public virtual int ThongKeDonVi_Quy(Nullable<int> year, Nullable<int> quy)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var quyParameter = quy.HasValue ?
                new ObjectParameter("Quy", quy) :
                new ObjectParameter("Quy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeDonVi_Quy", yearParameter, quyParameter);
        }
    
        public virtual int ThongKeDonVi_Thang(Nullable<int> year, Nullable<int> quy, Nullable<int> thang)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var quyParameter = quy.HasValue ?
                new ObjectParameter("Quy", quy) :
                new ObjectParameter("Quy", typeof(int));
    
            var thangParameter = thang.HasValue ?
                new ObjectParameter("Thang", thang) :
                new ObjectParameter("Thang", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeDonVi_Thang", yearParameter, quyParameter, thangParameter);
        }
    
        public virtual int ThongKeDonVi_Tuan(Nullable<int> year, Nullable<int> quy, Nullable<int> thang, Nullable<int> tuan)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var quyParameter = quy.HasValue ?
                new ObjectParameter("Quy", quy) :
                new ObjectParameter("Quy", typeof(int));
    
            var thangParameter = thang.HasValue ?
                new ObjectParameter("Thang", thang) :
                new ObjectParameter("Thang", typeof(int));
    
            var tuanParameter = tuan.HasValue ?
                new ObjectParameter("Tuan", tuan) :
                new ObjectParameter("Tuan", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeDonVi_Tuan", yearParameter, quyParameter, thangParameter, tuanParameter);
        }
    
        public virtual int ThongKeLinhVuc_Nam(Nullable<int> donViID, Nullable<int> year)
        {
            var donViIDParameter = donViID.HasValue ?
                new ObjectParameter("DonViID", donViID) :
                new ObjectParameter("DonViID", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeLinhVuc_Nam", donViIDParameter, yearParameter);
        }
    
        public virtual int ThongKeLinhVuc_Quy(Nullable<int> donViID, Nullable<int> year, Nullable<int> quy)
        {
            var donViIDParameter = donViID.HasValue ?
                new ObjectParameter("DonViID", donViID) :
                new ObjectParameter("DonViID", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var quyParameter = quy.HasValue ?
                new ObjectParameter("Quy", quy) :
                new ObjectParameter("Quy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeLinhVuc_Quy", donViIDParameter, yearParameter, quyParameter);
        }
    
        public virtual int ThongKeLinhVuc_Thang(Nullable<int> donViID, Nullable<int> year, Nullable<int> quy, Nullable<int> thang)
        {
            var donViIDParameter = donViID.HasValue ?
                new ObjectParameter("DonViID", donViID) :
                new ObjectParameter("DonViID", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var quyParameter = quy.HasValue ?
                new ObjectParameter("Quy", quy) :
                new ObjectParameter("Quy", typeof(int));
    
            var thangParameter = thang.HasValue ?
                new ObjectParameter("Thang", thang) :
                new ObjectParameter("Thang", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeLinhVuc_Thang", donViIDParameter, yearParameter, quyParameter, thangParameter);
        }
    
        public virtual int ThongKeLinhVuc_Tuan(Nullable<int> donViID, Nullable<int> year, Nullable<int> quy, Nullable<int> thang, Nullable<int> tuan)
        {
            var donViIDParameter = donViID.HasValue ?
                new ObjectParameter("DonViID", donViID) :
                new ObjectParameter("DonViID", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var quyParameter = quy.HasValue ?
                new ObjectParameter("Quy", quy) :
                new ObjectParameter("Quy", typeof(int));
    
            var thangParameter = thang.HasValue ?
                new ObjectParameter("Thang", thang) :
                new ObjectParameter("Thang", typeof(int));
    
            var tuanParameter = tuan.HasValue ?
                new ObjectParameter("Tuan", tuan) :
                new ObjectParameter("Tuan", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeLinhVuc_Tuan", donViIDParameter, yearParameter, quyParameter, thangParameter, tuanParameter);
        }
    
        public virtual int ThongKeThuTuc_Nam(Nullable<int> donViID, Nullable<int> linhVucID, Nullable<int> year)
        {
            var donViIDParameter = donViID.HasValue ?
                new ObjectParameter("DonViID", donViID) :
                new ObjectParameter("DonViID", typeof(int));
    
            var linhVucIDParameter = linhVucID.HasValue ?
                new ObjectParameter("LinhVucID", linhVucID) :
                new ObjectParameter("LinhVucID", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeThuTuc_Nam", donViIDParameter, linhVucIDParameter, yearParameter);
        }
    
        public virtual int ThongKeThuTuc_Quy(Nullable<int> donViID, Nullable<int> linhVucID, Nullable<int> year, Nullable<int> quy)
        {
            var donViIDParameter = donViID.HasValue ?
                new ObjectParameter("DonViID", donViID) :
                new ObjectParameter("DonViID", typeof(int));
    
            var linhVucIDParameter = linhVucID.HasValue ?
                new ObjectParameter("LinhVucID", linhVucID) :
                new ObjectParameter("LinhVucID", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var quyParameter = quy.HasValue ?
                new ObjectParameter("Quy", quy) :
                new ObjectParameter("Quy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeThuTuc_Quy", donViIDParameter, linhVucIDParameter, yearParameter, quyParameter);
        }
    
        public virtual int ThongKeThuTuc_Thang(Nullable<int> donViID, Nullable<int> linhVucID, Nullable<int> year, Nullable<int> quy, Nullable<int> thang)
        {
            var donViIDParameter = donViID.HasValue ?
                new ObjectParameter("DonViID", donViID) :
                new ObjectParameter("DonViID", typeof(int));
    
            var linhVucIDParameter = linhVucID.HasValue ?
                new ObjectParameter("LinhVucID", linhVucID) :
                new ObjectParameter("LinhVucID", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var quyParameter = quy.HasValue ?
                new ObjectParameter("Quy", quy) :
                new ObjectParameter("Quy", typeof(int));
    
            var thangParameter = thang.HasValue ?
                new ObjectParameter("Thang", thang) :
                new ObjectParameter("Thang", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeThuTuc_Thang", donViIDParameter, linhVucIDParameter, yearParameter, quyParameter, thangParameter);
        }
    
        public virtual int ThongKeThuTuc_Tuan(Nullable<int> donViID, Nullable<int> linhVucID, Nullable<int> year, Nullable<int> quy, Nullable<int> thang, Nullable<int> tuan)
        {
            var donViIDParameter = donViID.HasValue ?
                new ObjectParameter("DonViID", donViID) :
                new ObjectParameter("DonViID", typeof(int));
    
            var linhVucIDParameter = linhVucID.HasValue ?
                new ObjectParameter("LinhVucID", linhVucID) :
                new ObjectParameter("LinhVucID", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var quyParameter = quy.HasValue ?
                new ObjectParameter("Quy", quy) :
                new ObjectParameter("Quy", typeof(int));
    
            var thangParameter = thang.HasValue ?
                new ObjectParameter("Thang", thang) :
                new ObjectParameter("Thang", typeof(int));
    
            var tuanParameter = tuan.HasValue ?
                new ObjectParameter("Tuan", tuan) :
                new ObjectParameter("Tuan", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeThuTuc_Tuan", donViIDParameter, linhVucIDParameter, yearParameter, quyParameter, thangParameter, tuanParameter);
        }
    
        public virtual int ThongKeToanTP(Nullable<int> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeToanTP", yearParameter);
        }
    
        public virtual int ThongKeToanTP_DonVi_Nam(Nullable<int> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeToanTP_DonVi_Nam", yearParameter);
        }
    
        public virtual int ThongKeToanTP_DonVi_Quy(Nullable<int> year, Nullable<int> quy)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var quyParameter = quy.HasValue ?
                new ObjectParameter("Quy", quy) :
                new ObjectParameter("Quy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeToanTP_DonVi_Quy", yearParameter, quyParameter);
        }
    
        public virtual int ThongKeToanTP_DonVi_Thang(Nullable<int> year, Nullable<int> thang)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var thangParameter = thang.HasValue ?
                new ObjectParameter("Thang", thang) :
                new ObjectParameter("Thang", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeToanTP_DonVi_Thang", yearParameter, thangParameter);
        }
    
        public virtual int ThongKeToanTP_DonVi_Tuan(Nullable<int> year, Nullable<int> thang, Nullable<int> tuan)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var thangParameter = thang.HasValue ?
                new ObjectParameter("Thang", thang) :
                new ObjectParameter("Thang", typeof(int));
    
            var tuanParameter = tuan.HasValue ?
                new ObjectParameter("Tuan", tuan) :
                new ObjectParameter("Tuan", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThongKeToanTP_DonVi_Tuan", yearParameter, thangParameter, tuanParameter);
        }
    
        public virtual ObjectResult<KetQuaDanhGiaByDanhGiaID_Result> KetQuaDanhGiaByDanhGiaID(Nullable<long> danhGiaID)
        {
            var danhGiaIDParameter = danhGiaID.HasValue ?
                new ObjectParameter("DanhGiaID", danhGiaID) :
                new ObjectParameter("DanhGiaID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<KetQuaDanhGiaByDanhGiaID_Result>("KetQuaDanhGiaByDanhGiaID", danhGiaIDParameter);
        }
    }
}