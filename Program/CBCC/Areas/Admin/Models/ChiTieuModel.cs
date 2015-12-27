using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebMVC.Bussiness;
using WebMVC.Entities;

namespace CBCC.Models
{
    public class TieuChiModel : TieuChi
    {
        public List<SelectListItem> ListNhomTieuChi { get; set; }


        public List<SelectListItem> LoaiInputList { get; set; }


        public List<SelectListItem> ListDanhSachTraLoi { get; set; }

        public List<string> ListIDCauTraLoi { get; set; }

        public TieuChiModel()
        {
            ListNhomTieuChi = new List<SelectListItem>();
            ListNhomTieuChi.AddRange(DanhMucService.NhomTieuChiGetAll().Select(x => new SelectListItem { Text = x.TenNhomTieuChi, Value = x.ID.ToString() }).ToList());

            var itemNull = new SelectListItem { Text = "Chọn nhóm tiêu chí", Value = string.Empty};
            ListNhomTieuChi.Insert(0, itemNull); 

            ListDanhSachTraLoi = new List<SelectListItem>();
            ListDanhSachTraLoi.AddRange(DanhMucService.CauTraLoiGetAll_ForBO().OrderBy(x=>x.TenVanTat).Select(x => new SelectListItem { Text = x.TenVanTat, Value = x.ID.ToString() }).ToList());


            LoaiInputList = new List<SelectListItem>();
            LoaiInputList.AddRange(DanhMucService.InputTypeGetAllList().Select(x => new SelectListItem { Text = x.Name, Value = x.Value.ToString() }).ToList());

            
          


        }
    }
}