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
    
    public partial class CauTraLoi
    {
        public CauTraLoi()
        {
            this.KetQuaDanhGias = new HashSet<KetQuaDanhGia>();
            this.TieuChiCauTraLois = new HashSet<TieuChiCauTraLoi>();
        }
    
        public int ID { get; set; }
        public string TenCauTraLoi { get; set; }
        public Nullable<int> GiaTri { get; set; }
        public string TenVanTat { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual ICollection<KetQuaDanhGia> KetQuaDanhGias { get; set; }
        public virtual ICollection<TieuChiCauTraLoi> TieuChiCauTraLois { get; set; }
    }
}
