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
    
    public partial class KetQuaDanhGia
    {
        public long ID { get; set; }
        public Nullable<long> DanhGiaID { get; set; }
        public Nullable<int> TieuChiID { get; set; }
        public Nullable<int> CauTraLoiID { get; set; }
        public Nullable<int> TypeInput { get; set; }
        public string CauTraLoiText { get; set; }
    
        public virtual CauTraLoi CauTraLoi { get; set; }
        public virtual DanhGia DanhGia { get; set; }
        public virtual TieuChi TieuChi { get; set; }
    }
}
