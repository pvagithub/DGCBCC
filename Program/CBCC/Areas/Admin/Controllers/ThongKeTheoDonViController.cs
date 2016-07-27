using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;
using System.Linq;
using CBCC.Helper;
using Aspose.Cells;
using System.Drawing;
using System.IO;

namespace CBCC.Areas.Admin.Controllers
{
    public class ThongKeTheoDonViController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "User")]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.DonVi = DanhMucService.DonViGetAllList().Where(x => x.Active == true).ToList();
            ViewBag.MaDonVi = (ViewBag.DonVi as List<DonVi>) != null ? (ViewBag.DonVi as List<DonVi>)[0].MaDonVi : string.Empty;

        }
        public ActionResult Index()
        {
            ViewBag.TuNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).ToString("dd/MM/yyyy");
            ViewBag.DenNgay = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))).ToString("dd/MM/yyyy");
            ThongKe thongke;
            string username = HttpContext.User.Identity.Name;
            thongke = ThongKeService.ThongKe_TheoDonVi_ByTime(ViewBag.TuNgay, ViewBag.DenNgay, username);

            if (thongke == null || thongke.HaiLong == null)
            {
                thongke = new ThongKe();
                thongke.HaiLong = 100;
                thongke.KhongHaiLong = 0;
                thongke.BinhThuong = 0;
                thongke.SCBinhThuong = "0/0";
                thongke.SCHaiLong = "0/0";
                thongke.SCKhongHaiLong = "0/0";
            }
            ViewBag.HaiLong = thongke.HaiLong;
            ViewBag.KhongHaiLong = thongke.KhongHaiLong;
            ViewBag.BinhThuong = thongke.BinhThuong;
            ViewBag.SCBinhThuong = thongke.SCBinhThuong;
            ViewBag.SCHaiLong = thongke.SCHaiLong;
            ViewBag.SCKhongHaiLong = thongke.SCKhongHaiLong;

            // ban bieu
            ViewBag.BanBieu = ThongKeService.ThongKe_TheoDonVi_ByDonVi_ByTime(ViewBag.TuNgay, ViewBag.DenNgay, username);
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username, string tuNgay, string denNgay)
        {
            tuNgay = string.IsNullOrWhiteSpace(tuNgay) ? DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy") : tuNgay;
            denNgay = string.IsNullOrWhiteSpace(denNgay) ? DateTime.Now.ToString("dd/MM/yyyy") : denNgay;
            #region thong ke toan tinh
            ThongKe thongke;
            string suser = HttpContext.User.Identity.Name;
            thongke = ThongKeService.ThongKe_TheoDonVi_ByTime(tuNgay, denNgay, suser);
            if (thongke == null || thongke.HaiLong == null)
            {
                thongke = new ThongKe();
                thongke.HaiLong = 100;
                thongke.KhongHaiLong = 0;
                thongke.BinhThuong = 0;
                thongke.SCBinhThuong = "0/0";
                thongke.SCHaiLong = "0/0";
                thongke.SCKhongHaiLong = "0/0";
            }
            ViewBag.TuNgay = tuNgay;
            ViewBag.DenNgay = denNgay;
            // ViewBag.MaDonVi = cbDonVi;
            ViewBag.HaiLong = thongke.HaiLong;
            ViewBag.KhongHaiLong = thongke.KhongHaiLong;
            ViewBag.BinhThuong = thongke.BinhThuong;
            ViewBag.SCBinhThuong = thongke.SCBinhThuong;
            ViewBag.SCHaiLong = thongke.SCHaiLong;
            ViewBag.SCKhongHaiLong = thongke.SCKhongHaiLong;
            // ban bieu
            ViewBag.BanBieu = ThongKeService.ThongKe_TheoDonVi_ByDonVi_ByTime(tuNgay, denNgay, suser);
            #endregion
            return View();
        }
        public ActionResult Banbieu(string tuNgay, string denNgay)
        {
            var dt = CodeHelper.ConvertToDataTable(ThongKeService.ThongKe_TheoDonVi_ByDonVi_ByTime(tuNgay, denNgay, User.Identity.Name));
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            cells[4, 0].PutValue("STT");
            cells[4, 1].PutValue("Tháng/Năm");
            cells[4, 2].PutValue("Hài lòng");
            cells[4, 3].PutValue("Bình thường");
            cells[4, 4].PutValue("Không hài lòng");

            int row = 5;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cells[row + i, 0].PutValue(i + 1);
                cells[row + i, 1].PutValue(dt.Rows[i]["TenDonVi"]);
                cells[row + i, 2].PutValue(dt.Rows[i]["HaiLong"] + "% (" + dt.Rows[i]["SCHaiLong"] + ")");
                cells[row + i, 3].PutValue(dt.Rows[i]["BinhThuong"] + "% (" + dt.Rows[i]["SCBinhThuong"] + ")");
                cells[row + i, 4].PutValue(dt.Rows[i]["KhongHaiLong"] + "% (" + dt.Rows[i]["SCKhongHaiLong"] + ")");
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
            range = cells.CreateRange(5, 0, dt.Rows.Count, 5);
            style = new Style();
            style.HorizontalAlignment = TextAlignmentType.Left;
            style.VerticalAlignment = TextAlignmentType.Left;

            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 0].PutValue("Thống kê  theo đơn vị - Bản biểu");
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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeTheoDonViBanBieu_" + DateTime.Now.ToString("ddMMyyyhhmmss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
            return View();
        }
    }
}
