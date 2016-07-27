using Aspose.Cells;
using CBCC.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;

namespace CBCC.Areas.Admin.Controllers
{
    public class SoLuotDanhGiaController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin,Manage,User")]
        //
        // GET: /Admin/CaiDat/
        public ActionResult Index(string tuNgay = "", string denNgay = "", bool back = false)
        {
            ViewBag.TuNgay = string.IsNullOrWhiteSpace(tuNgay) ? (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy") : tuNgay;
            ViewBag.DenNgay = string.IsNullOrWhiteSpace(denNgay) ? (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy") : denNgay;
            var thongke = ThongKeService.ThongKeSoLuotDanhGia(ViewBag.TuNgay, ViewBag.DenNgay, User.Identity.Name);
            ViewBag.Result = thongke;
            return View();
        }
        [HttpPost]
        public ActionResult Index(string tuNgay, string denNgay)
        {
            ViewBag.TuNgay = string.IsNullOrWhiteSpace(tuNgay) ? (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy") : tuNgay;
            ViewBag.DenNgay = string.IsNullOrWhiteSpace(denNgay) ? (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy") : denNgay;
            var thongke = ThongKeService.ThongKeSoLuotDanhGia(ViewBag.TuNgay, ViewBag.DenNgay, User.Identity.Name);
            ViewBag.Result = thongke;
            return View();
        }
        public ActionResult ThongKeSoLuotDanhGiaDonVi(string tuNgay, string denNgay, string madonvi, string malienthong)
        {
            ViewBag.TuNgay = string.IsNullOrWhiteSpace(tuNgay) ? (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy") : tuNgay;
            ViewBag.DenNgay = string.IsNullOrWhiteSpace(denNgay) ? (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy") : denNgay;
            var thongke = ThongKeService.ThongKeSoLuotDanhGia_By_DonVi(ViewBag.TuNgay, ViewBag.DenNgay, madonvi, malienthong, User.Identity.Name);
            ViewBag.Result = thongke;
            ViewBag.Madonvi = madonvi;
            ViewBag.Malienthong = malienthong;
            return View();
        }
        public ActionResult KetQuaDanhGiaHosoByDanhGiaID(string danhGiaID)
        {
            ViewBag.HoSo = ThongKeService.KetQuaDanhGiaThongTinHoSoByDanhGiaID(danhGiaID, string.Empty);
            ViewBag.Result = ThongKeService.KetQuaDanhGiaHosoByDanhGiaID(danhGiaID);
            return View();
        }
        public ActionResult ExportExcel(string tuNgay, string denNgay)
        {
            var lst = ThongKeService.ThongKeSoLuotDanhGia(tuNgay, denNgay, User.Identity.Name);
            var dt = CodeHelper.ConvertToDataTable(lst);
            int t = lst.Sum(x => int.Parse(x.SoBienNhan));
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            cells[4, 0].PutValue("STT");
            cells[4, 1].PutValue("Đơn vị");
            cells[4, 2].PutValue("Số lượt đánh giá");

            int row = 5;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cells[row + i, 0].PutValue(i + 1);
                cells[row + i, 1].PutValue(dt.Rows[i]["TenDonVi"]);
                cells[row + i, 2].PutValue(int.Parse(dt.Rows[i]["SoBienNhan"].ToString()));
                if (i == dt.Rows.Count - 1)
                {
                    cells[row + i + 1, 0].PutValue(i + 2);
                    cells[row + i + 1, 1].PutValue("Tổng cộng");
                    cells[row + i + 1, 2].PutValue(t);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, 1, 3);
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
            range = cells.CreateRange(5, 0, dt.Rows.Count + 1, 3);
            style = new Style();
            style.HorizontalAlignment = TextAlignmentType.Left;
            style.VerticalAlignment = TextAlignmentType.Left;

            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 0].PutValue("Số lượt đánh giá");
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
            Response.AddHeader("Content-Disposition", "attachment; filename=SoLuotDanhGia_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
            return View();
        }
        public ActionResult ExportExcelBienNhan(string tuNgay, string denNgay, string madonvi, string malienthong)
        {
            var lst = ThongKeService.ThongKeSoLuotDanhGia_By_DonVi(tuNgay, denNgay, madonvi, malienthong, User.Identity.Name);
            string hl = Math.Round(lst.Average(x => x.HaiLong), 2) + "%";
            string bt = Math.Round(lst.Average(x => x.BinhThuong), 2) + "%";
            string khl = Math.Round(lst.Average(x => x.KhongHaiLong), 2) + "%";
            var dt = CodeHelper.ConvertToDataTable(lst);
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            cells[4, 0].PutValue("STT");
            cells[4, 1].PutValue("Số biên nhận");
            cells[4, 2].PutValue("Hài lòng");
            cells[4, 3].PutValue("Bình thường");
            cells[4, 4].PutValue("Không hài lòng");

            int row = 5;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cells[row + i, 0].PutValue(i + 1);
                cells[row + i, 1].PutValue(dt.Rows[i]["SoBienNhan"]);
                cells[row + i, 2].PutValue(dt.Rows[i]["HaiLong"] + "% (" + dt.Rows[i]["SCHaiLong"] + ")");
                cells[row + i, 3].PutValue(dt.Rows[i]["BinhThuong"] + "% (" + dt.Rows[i]["SCBinhThuong"] + ")");
                cells[row + i, 4].PutValue(dt.Rows[i]["KhongHaiLong"] + "% (" + dt.Rows[i]["SCKhongHaiLong"] + ")");
                if (i == dt.Rows.Count - 1)
                {
                    cells[row + i + 1, 0].PutValue(i + 2);
                    cells[row + i + 1, 1].PutValue("Tổng trung bình");
                    cells[row + i + 1, 2].PutValue(hl);
                    cells[row + i + 1, 3].PutValue(bt);
                    cells[row + i + 1, 4].PutValue(khl);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, 1, 5);
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
            range = cells.CreateRange(5, 0, dt.Rows.Count + 1, 5);
            style = new Style();
            style.HorizontalAlignment = TextAlignmentType.Left;
            style.VerticalAlignment = TextAlignmentType.Left;

            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 0].PutValue("Số lượt đánh giá - Danh sách số biên nhận");
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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKe3Thang-SBN_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
            return View();
        }
    }
}