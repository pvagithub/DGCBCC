using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;
using CBCC.Models;
using PagedList;

namespace CBCC.Controllers
{
    public class ThongKeController : Controller
    {
        public ActionResult Index()
        {
            var filter = new FilterModel();
            ViewData["filter"] = filter;
            return View();
        }
        public ActionResult LinhVuc(int id, int? namID, int? quyID, int? thangID, int? tuanID, string tuNgay, string denNgay)
        {
            var filter = new FilterModel();
            filter.DonViID = id;
            filter.namID = namID;
            filter.quyID = quyID;
            filter.thangID = thangID;
            filter.tuanID = tuanID;
            filter.tuNgay = tuNgay;
            filter.denNgay = denNgay;
            ViewData["filter"] = filter;

            return View();
        }
        public ActionResult ThuTuc(int id, int linhVucID, int? namID, int? quyID, int? thangID, int? tuanID, string tuNgay, string denNgay)
        {
            var filter = new FilterModel();
            filter.DonViID = id;
            filter.LinhVucID = linhVucID;
            filter.namID = namID;
            filter.quyID = quyID;
            filter.thangID = thangID;
            filter.tuanID = tuanID;
            filter.tuNgay = tuNgay;
            filter.denNgay = denNgay;
            ViewData["filter"] = filter;
            return View();
        }

        public ActionResult DanhSachHoSo(int id, int linhVucID, int thuTucID, int? page)
        {
            var lstHoSo = ThongKeService.DanhSachHoSo(id, linhVucID, thuTucID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.Page = (pageNumber - 1) * pageSize;
            ViewBag.DonViID = id;
            ViewBag.LinhVucID = linhVucID;
            ViewBag.ThuTucID = thuTucID;
            return View(lstHoSo.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult HoSo(string id)
        {
            var hs = ThongKeService.ThongTinHoSoBySoBienNhan(id);
            List<QuaTrinhXuLy> qtxl = new List<QuaTrinhXuLy>();
            List<KetQuaDanhGiaByDanhGiaID_Result> ketQuaDanhGia = new List<KetQuaDanhGiaByDanhGiaID_Result>();
            List<ThongTinNguoiXuLy> xuLy = new List<ThongTinNguoiXuLy>();
            if (hs != null)
            {
                qtxl = ThongKeService.QuaTrinhXuLyByHoSoID(hs.ID.ToString());
                ketQuaDanhGia = ThongKeService.KetQuaDanhGiaByDanhGiaID(hs.DanhGiaID.Value.ToString());
                xuLy = ThongKeService.ThongTinNguoiXuLyByHoSoID(hs.ID.ToString());
            }
            ViewBag.HoSo = hs;
            ViewBag.QTXL = qtxl;
            ViewBag.KetQua = ketQuaDanhGia;
            ViewBag.XuLy = xuLy;
            return View();
        }

        #region ThongKe DonVi

        public ActionResult ThongKeDonVi(int? namID, int? quyID, int? thangID, int? tuanID, string tuNgay, string denNgay)
        {
            namID = namID == null ? 0 : namID;
            quyID = quyID == null ? 0 : quyID;
            thangID = thangID == null ? 0 : thangID;
            tuanID = tuanID == null ? 0 : tuanID;

            var filter = new FilterModel();
            filter.namID = namID;
            filter.quyID = quyID;
            filter.thangID = thangID;
            filter.tuanID = tuanID;
            filter.tuNgay = tuNgay;
            filter.denNgay = denNgay;
            ViewData["filter"] = filter;
            if (!string.IsNullOrEmpty(tuNgay) && !string.IsNullOrEmpty(denNgay))
            {
                var dataTuNgayDenNgay = ThongKeService.DonVi_BaoCao_Day(tuNgay, denNgay);
                if (dataTuNgayDenNgay.Count > 0)
                {
                    return PartialView("_Partial_DonVi_TuNgay_DenNgay", dataTuNgayDenNgay);
                }
            }
            else
            {
                var data = ThongKeService.DonVi_BaoCao_getList(namID, quyID, thangID, tuanID);

                if (data.Item1.Count > 0)
                {

                    return PartialView("_Partial_DonVi_Nam", data.Item1);
                }
                else if (data.Item2.Count > 0)
                {
                    return PartialView("_Partial_DonVi_Quy", data.Item2);
                }
                else if (data.Item3.Count > 0)
                {
                    return PartialView("_Partial_DonVi_Thang", data.Item3);
                }
                else
                {
                    return PartialView("_Partial_DonVi_Tuan", data.Item4);
                }
            }
            return View();
        }

        #endregion ThongKe DonVi

        #region Thong ke linh vuc
        public ActionResult ThongKeLinhVuc(int donViID, int? namID, int? quyID, int? thangID, int? tuanID, string tuNgay, string denNgay)
        {
            namID = namID == null ? 0 : namID;
            quyID = quyID == null ? 0 : quyID;
            thangID = thangID == null ? 0 : thangID;
            tuanID = tuanID == null ? 0 : tuanID;

            var filter = new FilterModel();
            filter.DonViID = donViID;
            filter.namID = namID;
            filter.quyID = quyID;
            filter.thangID = thangID;
            filter.tuanID = tuanID;
            filter.tuNgay = tuNgay;
            filter.denNgay = denNgay;
            ViewData["filter"] = filter;
            if (!string.IsNullOrEmpty(tuNgay) && !string.IsNullOrEmpty(denNgay))
            {
                var dataTuNgayDenNgay = ThongKeService.LinhVuc_BaoCao_Day(donViID, tuNgay, denNgay);
                if (dataTuNgayDenNgay.Count > 0)
                {
                    return PartialView("_Partial_LinhVuc_TuNgay_DenNgay", dataTuNgayDenNgay);
                }
            }
            else
            {
                var data = ThongKeService.LinhVuc_BaoCao_getList(donViID, namID, quyID, thangID, tuanID);

                if (data.Item1.Count > 0)
                {
                    return PartialView("_Partial_LinhVuc_Nam", data.Item1);
                }
                else if (data.Item2.Count > 0)
                {
                    return PartialView("_Partial_LinhVuc_Quy", data.Item2);
                }
                else if (data.Item3.Count > 0)
                {
                    return PartialView("_Partial_LinhVuc_Thang", data.Item3);
                }
                else
                {
                    return PartialView("_Partial_LinhVuc_Tuan", data.Item4);
                }
            }
            return View();
        }
        #endregion

        #region Thong ke thu tuc
        public ActionResult ThongKeThuTuc(int donViID, int linhVucID, int? namID, int? quyID, int? thangID, int? tuanID, string tuNgay, string denNgay)
        {
            namID = namID == null ? 0 : namID;
            quyID = quyID == null ? 0 : quyID;
            thangID = thangID == null ? 0 : thangID;
            tuanID = tuanID == null ? 0 : tuanID;

            var filter = new FilterModel();
            filter.DonViID = donViID;
            filter.LinhVucID = linhVucID;
            filter.namID = namID;
            filter.quyID = quyID;
            filter.thangID = thangID;
            filter.tuanID = tuanID;
            filter.tuNgay = tuNgay;
            filter.denNgay = denNgay;
            ViewData["filter"] = filter;

            if (!string.IsNullOrEmpty(tuNgay) && !string.IsNullOrEmpty(denNgay))
            {
                var dataTuNgayDenNgay = ThongKeService.ThuTuc_BaoCao_Day(donViID, linhVucID, tuNgay, denNgay);
                if (dataTuNgayDenNgay.Count > 0)
                {
                    return PartialView("_Partial_ThuTuc_TuNgay_DenNgay", dataTuNgayDenNgay);
                }
            }
            else
            {
                var data = ThongKeService.ThuTuc_BaoCao_getList(donViID, linhVucID, namID, quyID, thangID, tuanID);

                if (data.Item1.Count > 0)
                {
                    return PartialView("_Partial_ThuTuc_Nam", data.Item1);
                }
                else if (data.Item2.Count > 0)
                {
                    return PartialView("_Partial_ThuTuc_Quy", data.Item2);
                }
                else if (data.Item3.Count > 0)
                {
                    return PartialView("_Partial_ThuTuc_Thang", data.Item3);
                }
                else
                {
                    return PartialView("_Partial_ThuTuc_Tuan", data.Item4);
                }
            }
            return View();
        }
        #endregion

        #region ThongKe LinhVuc DonVi

        //public ActionResult ThongKeDonVi(int? namID, int? quyID, int? thangID, int? tuanID, string tuNgay, string denNgay)
        //{
        //    namID = namID == null ? 0 : namID;
        //    quyID = quyID == null ? 0 : quyID;
        //    thangID = thangID == null ? 0 : thangID;
        //    tuanID = tuanID == null ? 0 : tuanID;

        //    if (tuNgay != "" && denNgay != "")
        //    {
        //        var dataTuNgayDenNgay = ThongKeService.DonVi_BaoCao_Day(tuNgay, denNgay);
        //        if (dataTuNgayDenNgay.Count > 0)
        //        {
        //            return PartialView("_Partial_DonVi_TuNgay_DenNgay", dataTuNgayDenNgay);
        //        }
        //    }
        //    else
        //    {
        //        var data = ThongKeService.DonVi_BaoCao_getList(namID, quyID, thangID, tuanID);

        //        if (data.Item1.Count > 0)
        //        {
        //            return PartialView("_Partial_DonVi_Nam", data.Item1);
        //        }
        //        else if (data.Item2.Count > 0)
        //        {
        //            return PartialView("_Partial_DonVi_Quy", data.Item2);
        //        }
        //        else if (data.Item3.Count > 0)
        //        {
        //            return PartialView("_Partial_DonVi_Thang", data.Item3);
        //        }
        //        else
        //        {
        //            return PartialView("_Partial_DonVi_Tuan", data.Item4);
        //        }
        //    }
        //    return View();
        //}

        #endregion ThongKe DonVi

        #region Export To Excel

        #region don vi

        public ActionResult ExportToExcel(int? namID, int? quyID, int? thangID, int? tuanID, string tuNgay, string denNgay)
        {
            if (!string.IsNullOrEmpty(tuNgay) && !string.IsNullOrEmpty(denNgay))
            {
                var dataTuNgayDenNgay = ThongKeService.DonVi_BaoCao_Day(tuNgay, denNgay);
                GetData(dataTuNgayDenNgay);
            }
            else
            {
                var data = ThongKeService.DonVi_BaoCao_getList(namID, quyID, thangID, tuanID);
                if (data.Item1.Count > 0)
                {
                    GetData(data.Item1);
                }
                else if (data.Item2.Count > 0)
                {
                    GetData(data.Item2);
                }
                else if (data.Item3.Count > 0)
                {
                    GetData(data.Item3);
                }
                else if (data.Item4.Count > 0)
                {
                    GetData(data.Item4);
                }
            }
            return View("Index");
        }

        private void GetData(List<TK_DonVi_Nam> data)
        {

            string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên đơn vị");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenDonVi;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell          
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 2].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO ĐƠN VỊ");
            style = new Style();
            style.Font.Color = Color.Black;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Font.Size = 20;
            cells[1, 2].SetStyle(style);
            cells.Merge(1, 2, 2, 13);

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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeDonVi_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        private void GetData(List<TK_DonVi_Quy> data)
        {
            string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên đơn vị");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenDonVi;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell          
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 2].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO ĐƠN VỊ");
            style = new Style();
            style.Font.Color = Color.Black;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Font.Size = 20;
            cells[1, 2].SetStyle(style);
            cells.Merge(1, 2, 2, 13);

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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeDonVi_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        private void GetData(List<TK_DonVi_Thang> data)
        {
            string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên đơn vị");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenDonVi;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell          
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 2].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO ĐƠN VỊ");
            style = new Style();
            style.Font.Color = Color.Black;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Font.Size = 20;
            cells[1, 2].SetStyle(style);
            cells.Merge(1, 2, 2, 13);

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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeDonVi_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        private void GetData(List<TK_DonVi_Tuan> data)
        {
            string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên đơn vị");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenDonVi;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell          
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 2].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO ĐƠN VỊ");
            style = new Style();
            style.Font.Color = Color.Black;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Font.Size = 20;
            cells[1, 2].SetStyle(style);
            cells.Merge(1, 2, 2, 13);

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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeDonVi_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        private void GetData(List<ThongKeDonVi_TuNgay_DenNgay_Result> data)
        {
            string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên đơn vị");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenDonVi;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell          
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 2].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO ĐƠN VỊ");
            style = new Style();
            style.Font.Color = Color.Black;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Font.Size = 20;
            cells[1, 2].SetStyle(style);
            cells.Merge(1, 2, 2, 13);

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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeDonVi_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        #endregion

        #region linh vuc

        public ActionResult ExportToExcelLinhVuc(int donViID, int? namID, int? quyID, int? thangID, int? tuanID, string tuNgay, string denNgay)
        {
            if (!string.IsNullOrEmpty(tuNgay) && !string.IsNullOrEmpty(denNgay))
            {
                var dataTuNgayDenNgay = ThongKeService.LinhVuc_BaoCao_Day(donViID, tuNgay, denNgay);
                GetData(dataTuNgayDenNgay);
            }
            else
            {
                var data = ThongKeService.LinhVuc_BaoCao_getList(donViID, namID, quyID, thangID, tuanID);
                if (data.Item1.Count > 0)
                {
                    GetData(data.Item1);
                }
                else if (data.Item2.Count > 0)
                {
                    GetData(data.Item2);
                }
                else if (data.Item3.Count > 0)
                {
                    GetData(data.Item3);
                }
                else if (data.Item4.Count > 0)
                {
                    GetData(data.Item4);
                }
            }
            return View("Index");
        }

        private void GetData(List<TK_LinhVuc_Nam> data)
        {
            string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên lĩnh vực");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenLinhVuc;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 2].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO LĨNH VỰC");
            style = new Style();
            style.Font.Color = Color.Black;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Font.Size = 20;
            cells[1, 2].SetStyle(style);
            cells.Merge(1, 2, 2, 13);

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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeLinhVuc_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        private void GetData(List<TK_LinhVuc_Quy> data)
        {
            string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên lĩnh vực");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenLinhVuc;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 2].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO LĨNH VỰC");
            style = new Style();
            style.Font.Color = Color.Black;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Font.Size = 20;
            cells[1, 2].SetStyle(style);
            cells.Merge(1, 2, 2, 13);

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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeLinhVuc_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        private void GetData(List<TK_LinhVuc_Thang> data)
        {
            string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên lĩnh vực");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenLinhVuc;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 2].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO LĨNH VỰC");
            style = new Style();
            style.Font.Color = Color.Black;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Font.Size = 20;
            cells[1, 2].SetStyle(style);
            cells.Merge(1, 2, 2, 13);

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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeLinhVuc_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        private void GetData(List<TK_LinhVuc_Tuan> data)
        {
            string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên lĩnh vực");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenLinhVuc;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 2].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO LĨNH VỰC");
            style = new Style();
            style.Font.Color = Color.Black;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Font.Size = 20;
            cells[1, 2].SetStyle(style);
            cells.Merge(1, 2, 2, 13);

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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeLinhVuc_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        private void GetData(List<ThongKeLinhVuc_TuNgay_DenNgay_Result> data)
        {
            string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên lĩnh vực");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenLinhVuc;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 2].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO LĨNH VỰC");
            style = new Style();
            style.Font.Color = Color.Black;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Font.Size = 20;
            cells[1, 2].SetStyle(style);
            cells.Merge(1, 2, 2, 13);

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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeLinhVuc_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        #endregion

        #region thu tuc

        public ActionResult ExportToExcelThuTuc(int donViID, int linhVucID, int? namID, int? quyID, int? thangID, int? tuanID, string tuNgay, string denNgay)
        {
            if (!string.IsNullOrEmpty(tuNgay) && !string.IsNullOrEmpty(denNgay))
            {
                var dataTuNgayDenNgay = ThongKeService.ThuTuc_BaoCao_Day(donViID, linhVucID, tuNgay, denNgay);
                GetData(dataTuNgayDenNgay);
            }
            else
            {
                var data = ThongKeService.ThuTuc_BaoCao_getList(donViID, linhVucID, namID, quyID, thangID, tuanID);
                if (data.Item1.Count > 0)
                {
                    GetData(data.Item1);
                }
                else if (data.Item2.Count > 0)
                {
                    GetData(data.Item2);
                }
                else if (data.Item3.Count > 0)
                {
                    GetData(data.Item3);
                }
                else if (data.Item4.Count > 0)
                {
                    GetData(data.Item4);
                }
            }
            return View("Index");
        }

        private void GetData(List<TK_ThuTuc_Nam> data)
        {
            string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên thủ tục");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenThuTuc;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 0].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO THỦ TỤC");
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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeThuTuc_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        private void GetData(List<TK_ThuTuc_Quy> data)
        {
             string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên thủ tục");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenThuTuc;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 0].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO THỦ TỤC");
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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeThuTuc_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        private void GetData(List<TK_ThuTuc_Thang> data)
        {
             string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên thủ tục");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenThuTuc;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 0].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO THỦ TỤC");
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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeThuTuc_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        private void GetData(List<TK_ThuTuc_Tuan> data)
        {
             string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên thủ tục");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenThuTuc;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 0].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO THỦ TỤC");
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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeThuTuc_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        private void GetData(List<ThongKeThuTuc_TuNgay_DenNgay_Result> data)
        {
             string[] tenTieuChi = data[0].KeQuaDanhGia.Split(';');
            //Create a new Workbook.
            Workbook workbook = new Workbook();

            //Get the first worksheet.
            Worksheet sheet = workbook.Worksheets[0];

            // format ten column
            Cells cells = sheet.Cells;
            int cautraloi = 0;
            cells[4, 0].PutValue("STT");
            cells.Merge(4, 0, 2, 1);
            cells[4, 1].PutValue("Tên thủ tục");
            cells.Merge(4, 1, 2, 1);
            for (int i = 1; i < tenTieuChi.Length; i++)
            {
                cells[4, 2 + cautraloi].PutValue(tenTieuChi[i].Split('☺')[0]);
                string[] traloi = (tenTieuChi[i].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                cells.Merge(4, 2 + cautraloi, 1, traloi.Length - 1);
                for (int j = 0; j < traloi.Length - 1; j++)
                {
                    cells[5, 2 + cautraloi + j].PutValue(traloi[j + 1].Split('_')[0]);
                }
                cautraloi += (traloi.Length - 1);
            }
            int row = 0;
            for (int i = 0; i < data.Count; i++)
            {
                cautraloi = 0;
                string tenDonVi = data[i].TenThuTuc;
                tenTieuChi = data[i].KeQuaDanhGia.Split(';');
                row++;
                for (int j = 1; j < tenTieuChi.Length; j++)
                {
                    cells[6 + i, 0].PutValue(row);
                    cells[6 + i, 1].PutValue(tenDonVi);
                    string[] traloi = (tenTieuChi[j].Split('☺')[1]).Split(new[] { "__" }, StringSplitOptions.None);
                    for (int k = 0; k < traloi.Length - 1; k++)
                    {
                        cells[6 + i, 2 + cautraloi + k].PutValue(traloi[k + 1].Split('_')[1]);
                    }
                    cautraloi += (traloi.Length - 1);
                }
            }

            // format title
            Range range = cells.CreateRange(4, 0, row + 2, cautraloi + 2);
            Style style = new Style();
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(4, 0, 2, cautraloi + 2);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.ForegroundColor = System.Drawing.Color.FromArgb(91, 155, 213);
            style.Font.Color = Color.Yellow;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.IsBold = true;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format all cell
            range = cells.CreateRange(6, 2, row, cautraloi);
            style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Right;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            range.SetStyle(style);

            // format title
            cells[1, 0].PutValue("THỐNG KÊ KẾT QUẢ ĐÁNH GIÁ THEO THỦ TỤC");
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
            Response.AddHeader("Content-Disposition", "attachment; filename=ThongKeThuTuc_" + DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss") + ".xlsx");

            Response.BinaryWrite(dstStream.ToArray());
            Response.Flush();
            Response.End();
        }

        #endregion

        #endregion Export To Excel

    }
}