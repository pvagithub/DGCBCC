using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebMVC.Bussiness;
using WebMVC.Entities;

namespace CBCC.Models
{
    public class DonViLinhVucModel : LinhVucDonVi
    {
        public List<SelectListItem> ListDonVi { get; set; }
        public List<SelectListItem> ListLinhVuc { get; set; }
        public List<LinhVucDonVi> ListLinhVucDonVi { get; set; }

        public DonViLinhVucModel()
        {
            ListLinhVucDonVi = new List<LinhVucDonVi>();

            ListDonVi = new List<SelectListItem>();
            ListDonVi.AddRange(DanhMucService.DonViGetAllList().Select(x => new SelectListItem { Text = x.TenDonVi, Value = x.DonViID.ToString() }).ToList());

            var itemNull = new SelectListItem { Text = "Chọn đơn vị", Value = string.Empty };
            ListDonVi.Insert(0, itemNull);

            ListLinhVuc = new List<SelectListItem>();
            ListLinhVuc.AddRange(DanhMucService.LinhVucGetAllList().Select(x => new SelectListItem { Text = x.TenLinhVuc, Value = x.LinhVucID.ToString() }).ToList());

            itemNull = new SelectListItem { Text = "Chọn lĩnh vực", Value = string.Empty };
            ListLinhVuc.Insert(0, itemNull);
        }
    }
}