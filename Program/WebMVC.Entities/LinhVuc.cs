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
    
    public partial class LinhVuc
    {
        public LinhVuc()
        {
            this.LinhVucDonVis = new HashSet<LinhVucDonVi>();
            this.ThuTucs = new HashSet<ThuTuc>();
        }
    
        public int LinhVucID { get; set; }
        public string MaLinhVuc { get; set; }
        public string TenLinhVuc { get; set; }
        public string MoTa { get; set; }
        public string Image { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual ICollection<LinhVucDonVi> LinhVucDonVis { get; set; }
        public virtual ICollection<ThuTuc> ThuTucs { get; set; }
    }
}
