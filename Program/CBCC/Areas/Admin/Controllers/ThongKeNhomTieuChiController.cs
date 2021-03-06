﻿using DotNet.Highcharts;
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
using Aspose.Cells;
using System.IO;
namespace CBCC.Areas.Admin.Controllers
{
    public class ThongKeNhomTieuChiController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin,Manage")]
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");

            var list = ThongKeService.ThongKeNhomTieuChiTP_ByTime(ViewBag.TuNgay, ViewBag.DenNgay) as List<ThongKe>;
            var records = ConverKTNhomTieuChi(list).ToList();
            ViewBag.NhomTieuChiTP = records;

            list = ThongKeService.ThongKeNhomTieuChiDonVi_ByTime(ViewBag.TuNgay, ViewBag.DenNgay, string.Empty) as List<ThongKe>;
            records = ConverKTNhomTieuChi(list).ToList();
            ViewBag.NhomTieuChi = records;
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
            var list = ThongKeService.ThongKeNhomTieuChiTP_ByTime(ViewBag.TuNgay, ViewBag.DenNgay) as List<ThongKe>;
            var records = ConverKTNhomTieuChi(list).ToList();
            ViewBag.NhomTieuChiTP = records;
            // ban bieu
            list = ThongKeService.ThongKeNhomTieuChiDonVi_ByTime(ViewBag.TuNgay, ViewBag.DenNgay, string.Empty) as List<ThongKe>;
            records = ConverKTNhomTieuChi(list).ToList();
            ViewBag.NhomTieuChi = records;
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
            var records = GetReports(tuNgay, denNgay, page, limit, sortBy, direction, searchString, out total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        private List<TKNhomTieuChi> GetReports(string tuNgay, string denNgay, int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            int? id = string.IsNullOrEmpty(searchString) ? 0 : int.Parse(searchString);
            var list = ThongKeService.ThongKeNhomTieuChiDonVi_ByTime(tuNgay, denNgay, "0").Where(p => p.DonViID == id).ToList();

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
                            tk.TenDonVi = item.TenDonVi;
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
        public ActionResult ExportExcelNTC(string tuNgay, string denNgay)
        {
            var list = ThongKeService.ThongKeNhomTieuChiTP_ByTime(tuNgay, denNgay) as List<ThongKe>;
            var records = ConverKTNhomTieuChi(list).ToList();
            var dt = CodeHelper.ConvertToDataTable(records);
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            cells[4, 0].PutValue("Tên tiêu chí");
            cells[4, 1].PutValue("Hài lòng");
            cells[4, 2].PutValue("Bình thường");
            cells[4, 3].PutValue("Không hài lòng");

            int row = 5;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cells[row + i, 0].PutValue(dt.Rows[i]["TenTieuChi"]);
                cells[row + i, 1].PutValue(dt.Rows[i]["HaiLong"]);
                cells[row + i, 2].PutValue(dt.Rows[i]["BinhThuong"]);
                cells[row + i, 3].PutValue(dt.Rows[i]["KhongHaiLong"]);
            }

            // format title
            Range range = cells.CreateRange(4, 0, 1, 4);
            Style style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(5, 0, dt.Rows.Count, 4);
            style = new Style();
            style.HorizontalAlignment = TextAlignmentType.Left;
            style.VerticalAlignment = TextAlignmentType.Left;

            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 0].PutValue("Thống kê nhóm tiêu chí - Toàn thành phố");
            style = new Style();
            style.Font.Color = Color.Black;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Font.Size = 20;
            cells[1, 0].SetStyle(style);
            cells.Merge(1, 0, 2, 10);

            sheet.AutoFitColumns();
            MemoryStream dstStream = new MemoryStream();
            workbook.Save(dstStream, Aspose.Cells.SaveFormat.Xlsx);

            Response.Buffer = true;
            Response.Clear();
            Response.ClearHeaders();
            Response.ContentType = "application/vnd.ms-excel";
            Response.CacheControl = "public";
            Response.AddHeader("Pragma", "public");
            Response.AddHeader("Expires", "0");
            Response.AddHeader("Cache-Control", "must-revalidate, post-check=0, pre-check=0");
            Response.AddHeader("Content-Description", "Excel File Download");
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeNhomTieuChi_ToanTP_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
            return View();
        }
        public ActionResult ExportExcelNTCTungDonVi(string tuNgay, string denNgay)
        {
            var list = ThongKeService.ThongKeNhomTieuChiDonVi_ByTime(tuNgay, denNgay, string.Empty) as List<ThongKe>;
            var records = ConverKTNhomTieuChi(list).ToList();
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];
            int cautraloi = 0;
            // format ten column
            Cells cells = sheet.Cells;
            cells[4, 0].PutValue("Tên tiêu chí");
            cells.Merge(4, 0, 2, 1);
            for (int i = 0; i < records.Count; i += 12)
            {
                cells[4, 1 + cautraloi].PutValue(records[i].TenDonVi);
                cells.Merge(4, 1 + cautraloi, 1, 3);
                cells[5, 1 + cautraloi].PutValue("Hài lòng");
                cells[5, 1 + cautraloi + 1].PutValue("Bình thường");
                cells[5, 1 + cautraloi + 2].PutValue("Không hài lòng");
                cautraloi += 3;
            }
           
            for (int i = 0; i < 12; i++)
            {
                cautraloi = 0;
                cells[6 + i, 0].PutValue(records[i].TenTieuChi);
                for (int j = 0; j < records.Count; j++)
                {
                    if (records[i].TenTieuChi == records[j].TenTieuChi)
                    {
                        cells[6 + i, 1 + cautraloi].PutValue(records[j].HaiLong);
                        cells[6 + i, 1 + cautraloi + 1].PutValue(records[j].BinhThuong);
                        cells[6 + i, 1 + cautraloi + 2].PutValue(records[j].KhongHaiLong);
                        cautraloi += 3;
                    }
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, 2, cautraloi + 1);
            Style style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.Color = Color.Yellow;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(6, 0, 12, cautraloi + 1);
            style = new Style();
            style.HorizontalAlignment = TextAlignmentType.Left;
            style.VerticalAlignment = TextAlignmentType.Left;

            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 0].PutValue("Thống kê nhóm tiêu chí - Từng đơn vị");
            style = new Style();
            style.Font.Color = Color.Black;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Font.Size = 20;
            cells[1, 0].SetStyle(style);
            cells.Merge(1, 0, 2, 10);

            sheet.AutoFitColumns();
            MemoryStream dstStream = new MemoryStream();
            workbook.Save(dstStream, Aspose.Cells.SaveFormat.Xlsx);

            Response.Buffer = true;
            Response.Clear();
            Response.ClearHeaders();
            Response.ContentType = "application/vnd.ms-excel";
            Response.CacheControl = "public";
            Response.AddHeader("Pragma", "public");
            Response.AddHeader("Expires", "0");
            Response.AddHeader("Cache-Control", "must-revalidate, post-check=0, pre-check=0");
            Response.AddHeader("Content-Description", "Excel File Download");
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeNhomTieuChi_ToanTP_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
            return View();
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
