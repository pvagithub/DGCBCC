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
    
    public partial class DonVi
    {
        public DonVi()
        {
            this.LinhVucDonVis = new HashSet<LinhVucDonVi>();
        }
    
        public int DonViID { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string MoTa { get; set; }
        public Nullable<int> ParentDonViID { get; set; }
        public string Image { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<bool> LoaiDonVi { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual ICollection<LinhVucDonVi> LinhVucDonVis { get; set; }
    }
}