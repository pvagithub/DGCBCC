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
    
    public partial class LinhVucDonVi
    {
        public int ID { get; set; }
        public Nullable<int> DonViID { get; set; }
        public Nullable<int> LinhVucID { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual DonVi DonVi { get; set; }
        public virtual LinhVuc LinhVuc { get; set; }
    }
}
