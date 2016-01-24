using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;

namespace CBCC.Models
{
    public class LinhVucModel : LinhVuc
    {
        public List<LinhVuc> ListLinhVuc { get;set;}
        public List<LinhVucDonVi> ListLinhVucDonVi { get; set; }
        public List<SelectListItem> ListDonVi { get; set; }
        public List<string> ListDonViID { get; set; }

        public LinhVucModel()
        {
            ListLinhVuc = new List<LinhVuc>();
            ListDonVi = new List<SelectListItem>();
            ListDonVi.AddRange(DanhMucService.DonViGetAllList().Select(x => new SelectListItem { Text = x.TenDonVi, Value = x.DonViID.ToString() }).ToList());

            ListLinhVucDonVi = new List<LinhVucDonVi>();
            ListLinhVucDonVi = DanhMucService.DonViLinhVucGetAllList();

        }
    }
}