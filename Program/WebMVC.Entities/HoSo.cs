//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebMVC.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class HoSo
    {
        public long ID { get; set; }
        public string HoSoID { get; set; }
        public string SoBienNhan { get; set; }
        public Nullable<int> DonViID { get; set; }
        public string TenDonVi { get; set; }
        public Nullable<int> LinhVucID { get; set; }
        public string TenLinhVuc { get; set; }
        public Nullable<int> ThuTucID { get; set; }
        public string TenThuTuc { get; set; }
        public string NguoiNop { get; set; }
        public string NguoiTiepNhan { get; set; }
        public Nullable<System.DateTime> NgayNhan { get; set; }
        public Nullable<System.DateTime> NgayHenTra { get; set; }
        public Nullable<int> SoNgayGiaiQuyet { get; set; }
        public string DiaChi { get; set; }
        public string TenToChuc { get; set; }
        public string DienThoai { get; set; }
        public Nullable<bool> DaDanhGia { get; set; }
        public Nullable<long> DanhGiaID { get; set; }
        public string MaDonVi { get; set; }
    }
    public partial class ThongTinHoSo
    {
        public string SoBienNhan { get; set; }
        public string NguoiNop { get; set; }
        public System.DateTime? NgayNhan { get; set; }
        public System.DateTime? NgayHenTra { get; set; }
    }
}
