using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebMVC.Bussiness;
using WebMVC.Entities;

namespace CBCC.Models
{
    public class ThuTucModel : ThuTuc
    {
        public List<SelectListItem> ListLinhVuc { get; set; }
        public List<string> ListLinhVucID { get; set; }

        public ThuTucModel()
        {
            ListLinhVuc = new List<SelectListItem>();
            ListLinhVuc.AddRange(DanhMucService.LinhVucGetAllList().Select(x => new SelectListItem { Text = x.TenLinhVuc, Value = x.LinhVucID.ToString() }).ToList());

            var itemNull = new SelectListItem { Text = "Chọn lĩnh vực", Value = string.Empty };
            ListLinhVuc.Insert(0, itemNull);
        }
    }
}