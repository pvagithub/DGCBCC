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
    
    public partial class DanhGia
    {
        public DanhGia()
        {
            this.KetQuaDanhGias = new HashSet<KetQuaDanhGia>();
        }
    
        public long ID { get; set; }
        public string HoSoID { get; set; }
        public string SoBienNhan { get; set; }
        public Nullable<int> DonViID { get; set; }
        public string TenDonVi { get; set; }
        public Nullable<int> LinhVucID { get; set; }
        public string TenLinhVuc { get; set; }
        public Nullable<int> ThuTucID { get; set; }
        public string TenThuTuc { get; set; }
        public string PhongBan { get; set; }
        public Nullable<System.DateTime> NgayDanhGia { get; set; }
        public Nullable<bool> DanhGiaTrucTiep { get; set; }
    
        public virtual ICollection<KetQuaDanhGia> KetQuaDanhGias { get; set; }
    }
}
