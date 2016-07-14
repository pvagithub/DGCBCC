using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;
using System.Linq;
using System.Web;
using CBCC.Helper;
using CBCC.Areas.Admin.Models;
namespace CBCC.Areas.Admin.Controllers
{
    public class ThongKeNhomTieuChiController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin,Manage")]
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");
            ViewBag.NhomTieuChiTP = ThongKeService.ThongKeNhomTieuChiTP_ByTime(ViewBag.TuNgay, ViewBag.DenNgay) as List<ThongKe>;
            ViewBag.NhomTieuChi = ThongKeService.ThongKeNhomTieuChiDonVi_ByTime(ViewBag.TuNgay, ViewBag.DenNgay, "0") as List<ThongKe>;
            var lsDonVi = DanhMucService.DonViGetAllList().Where(x => x.Active == true).ToList();
            return View(lsDonVi);
        }
        [HttpPost]
        public ActionResult Index(string tuNgay, string denNgay)
        {
            tuNgay = string.IsNullOrWhiteSpace(tuNgay) ? DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy") : tuNgay;
            denNgay = string.IsNullOrWhiteSpace(denNgay) ? DateTime.Now.ToString("dd/MM/yyyy") : denNgay;
            #region thong ke toan tinh
            ViewBag.TuNgay = tuNgay;
            ViewBag.DenNgay = denNgay;
            ViewBag.NhomTieuChiTP = ThongKeService.ThongKeNhomTieuChiTP_ByTime(tuNgay, denNgay) as List<ThongKe>;
            // ban bieu
            ViewBag.NhomTieuChi = ThongKeService.ThongKeNhomTieuChiDonVi_ByTime(tuNgay, denNgay, "0") as List<ThongKe>;
            #endregion
            
            var lsDonVi = DanhMucService.DonViGetAllList().Where(x => x.Active == true).ToList();
            return View(lsDonVi);
            
        }
        [HttpGet]
        public JsonResult GetReports(string tuNgay, string denNgay, int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;
            tuNgay = string.IsNullOrWhiteSpace(tuNgay) ? DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy") : tuNgay;
            denNgay = string.IsNullOrWhiteSpace(denNgay) ? DateTime.Now.ToString("dd/MM/yyyy") : denNgay;
            var records = GetReports(tuNgay,denNgay, page, limit, sortBy, direction, searchString, out total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        private List<TKNhomTieuChi> GetReports(string tuNgay, string denNgay, int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            int? id = string.IsNullOrEmpty(searchString)? 0:int.Parse(searchString);
            var list = ThongKeService.ThongKeNhomTieuChiDonVi_ByTime(tuNgay, denNgay, "0").Where(p=>p.DonViID==id).ToList();
            
            var records = ConverKTNhomTieuChi(list).AsQueryable();
            total = records.Count();
            //if (!string.IsNullOrWhiteSpace(searchString))
            //{
            //    records = records.Where(p => p.TenDonVi.Contains(searchString));
            //}

            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    records = CodeHelper.OrderBy(records, sortBy);
                }
                else
                {
                    records = CodeHelper.OrderByDescending(records, sortBy);
                }
            }

            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value);
            }
            return records.ToList();
        }

        private List<TKNhomTieuChi> ConverKTNhomTieuChi(List<ThongKe> list)
        {
            int index = 1;
            List<TKNhomTieuChi> ls = new List<TKNhomTieuChi>();
            foreach (var item in list)
            {
                string[] lsttieuChi = item.KetQuaDanhGia.Split(';');

                for (int i = 1; i < lsttieuChi.Length; i++)
                {
                    TKNhomTieuChi tk = new TKNhomTieuChi();
                    string[] cauhoi = lsttieuChi[i].Split('☺');
                    for (int y = 1; y < cauhoi.Length; y++)
                    {
                        string[] cautraloi = cauhoi[y].Split(new[] { "__" }, StringSplitOptions.None);
                        for (int z = 0; z < cautraloi.Length; z++)
                        {
                            var dap = cautraloi[z].Split('_');
                            if (dap.Length < 2)
                            {
                                continue;
                            }
                            tk.ID = index;
                            tk.MaDonVi = item.DonViID;
                            tk.TenTieuChi = cauhoi[0];
                            if (dap[0].Equals("Hài Lòng"))
                            {
                                tk.HaiLong = dap[1];
                                continue;
                            }
                            if (dap[0].Equals("Bình Thường"))
                            {
                                tk.BinhThuong = dap[1];
                                continue;
                            }
                            if (dap[0].Equals("Không Hài Lòng"))
                            {
                                tk.KhongHaiLong = dap[1];
                            }
                        }
                       
                    }
                    index++;
                    ls.Add(tk);
                }
               
            }
            return ls;
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);   
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }
}
