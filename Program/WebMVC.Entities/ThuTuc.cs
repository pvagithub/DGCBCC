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

    public partial class ThuTuc
    {
        public int ThuTucID { get; set; }
        public string TenThucTuc { get; set; }
        public string MaThuTuc { get; set; }
        public string Description { get; set; }
        public Nullable<int> LinhVucID { get; set; }
        public string Image { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual LinhVuc LinhVuc { get; set; }
    }
}
