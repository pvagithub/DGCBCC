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
    
    public partial class Quy_BaoCao
    {
        public long ID { get; set; }
        public Nullable<int> NamID { get; set; }
        public Nullable<int> Quy { get; set; }
        public Nullable<System.DateTime> NgayDauQuy { get; set; }
        public Nullable<System.DateTime> NgayCuoiQuy { get; set; }
        public Nullable<int> CreatedUserID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LastUpdUserID { get; set; }
        public Nullable<System.DateTime> LastUpdDate { get; set; }
    }
}
