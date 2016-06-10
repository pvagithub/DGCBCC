using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBCC.Areas.Admin.Models
{
    public class TKNhomTieuChi
    {
        public int ID { get; set; }
        public int? MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string TenTieuChi { get; set; }
        public string HaiLong { get; set; }
        public string BinhThuong { get; set; }
        public string KhongHaiLong { get; set; }
    }
}