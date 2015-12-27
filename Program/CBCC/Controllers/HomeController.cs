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

namespace CBCC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region thong ke toan tinh
            GetThongKeToanTP_Result thongke;
            thongke = ThongKeService.ThongKeToanThanhPho(DateTime.Now.Year);
            if (thongke == null)
            {
                thongke = new GetThongKeToanTP_Result();
                thongke.HaiLong = 100;
                thongke.KhongHaiLong = 0;
                thongke.BinhThuong = 0;
            }
            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { PlotShadow = false })
                .SetTitle(new Title { Text = "" })
                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ Highcharts.numberFormat(this.y,2) +' %'; }" })
                .SetExporting(new Exporting { Enabled = false })
                .SetPlotOptions(new PlotOptions
                {
                    Pie = new PlotOptionsPie
                    {
                        AllowPointSelect = true,
                        Cursor = Cursors.Pointer,
                        DataLabels = new PlotOptionsPieDataLabels
                        {
                            Color = ColorTranslator.FromHtml("#000000"),
                            ConnectorColor = ColorTranslator.FromHtml("#000000"),
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ Highcharts.numberFormat(this.y,2) +' %'; }"
                        }
                    }
                })
                .SetSeries(new Series
                {
                    Type = ChartTypes.Pie,
                    Name = "Tỷ lệ hài lòng",
                    Data = new Data(new object[]
                    {
                        new object[] { "Bình thường",  (double)thongke.BinhThuong },
                        new object[] { "Không hài lòng",  (double)(100 - (thongke.BinhThuong + thongke.HaiLong)) },
                        new DotNet.Highcharts.Options.Point
                        {
                            Name = "Hài lòng",
                            Y = (double)thongke.HaiLong,
                            Sliced = true,
                            Selected = true
                        }
                    })
                });
            ViewBag.PieChart = chart;
            #endregion

            #region thong ke don vi nam
            // basic bar nam
            List<GetThongKeToanTP_DonVi_Nam_Result> thongkenam;
            thongkenam = ThongKeService.ThongKeToanThanhPho_Nam(DateTime.Now.Year);

            string[] arrDonVi = new string[thongkenam.Count];
            for (int i = 0; i < thongkenam.Count; i++)
            {
                arrDonVi[i] = thongkenam[i].TenDonVi;
            }

            object[] arrHaiLong = new object[thongkenam.Count];
            for (int i = 0; i < thongkenam.Count; i++)
            {
                arrHaiLong[i] = thongkenam[i].HaiLong;
            }

            object[] arrBinhThuong = new object[thongkenam.Count];
            for (int i = 0; i < thongkenam.Count; i++)
            {
                arrBinhThuong[i] = thongkenam[i].BinhThuong;
            }

            object[] arrKhongHaiLong = new object[thongkenam.Count];
            for (int i = 0; i < thongkenam.Count; i++)
            {
                arrKhongHaiLong[i] = thongkenam[i].KhongHaiLong;
            }

            Highcharts basicbar = new Highcharts("basicbar")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Bar, Height = arrBinhThuong.Length * 50 + 200})
                .SetTitle(new Title { Text = "" })
                //.SetSubtitle(new Subtitle { Text = "Thống kê " + DateTime.Now.Year })
                .SetExporting(new Exporting { Enabled = false })
                .SetXAxis(new XAxis
                {
                    Categories = arrDonVi,
                    Title = new XAxisTitle { Text = string.Empty }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Title = new YAxisTitle
                    {
                        Text = "Phần trăm (%)",
                        Align = AxisTitleAligns.High
                    },
                    Labels = new YAxisLabels
                    {
                        Overflow = "justify"
                    },
                    TickInterval = 10
                })
                .SetTooltip(new Tooltip { ValueSuffix = "%" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        DataLabels = new PlotOptionsBarDataLabels { Enabled = true }
                    }
                })
                .SetCredits(new Credits { Enabled = false })
                .SetSeries(new[]
                {
                    new Series { Name = "Bình thường", Data = new Data(arrBinhThuong) },
                    new Series { Name = "Không hài lòng", Data = new Data(arrKhongHaiLong) },
                    new Series { Name = "Hài lòng", Data = new Data(arrHaiLong) }
                });
            ViewBag.BasicBarNam = basicbar;
            #endregion

            #region thong ke don vi quy
            // basic bar quy
            List<GetThongKeToanTP_DonVi_Quy_Result> thongkequy;
            int quarter = 1;
            int month = DateTime.Now.Month;
            if (month >= 1 && month <= 3) { quarter = 1; }
            if (month >= 4 && month <= 6) { quarter = 2; }
            if (month >= 7 && month <= 9) { quarter = 3; }
            if (month >= 10 && month <= 12) { quarter = 4; }
            thongkequy = ThongKeService.ThongKeToanThanhPho_Quy(DateTime.Now.Year, quarter);

            string[] arrDonViQuy = new string[thongkequy.Count];
            for (int i = 0; i < thongkequy.Count; i++)
            {
                arrDonViQuy[i] = thongkequy[i].TenDonVi;
            }

            object[] arrHaiLongQuy = new object[thongkequy.Count];
            for (int i = 0; i < thongkequy.Count; i++)
            {
                arrHaiLongQuy[i] = thongkequy[i].HaiLong;
            }

            object[] arrBinhThuongQuy = new object[thongkequy.Count];
            for (int i = 0; i < thongkequy.Count; i++)
            {
                arrBinhThuongQuy[i] = thongkequy[i].BinhThuong;
            }

            object[] arrKhongHaiLongQuy = new object[thongkequy.Count];
            for (int i = 0; i < thongkequy.Count; i++)
            {
                arrKhongHaiLongQuy[i] = thongkequy[i].KhongHaiLong;
            }

            Highcharts basicbarQuy = new Highcharts("basicbarQuy")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Bar, Height = arrBinhThuongQuy.Length * 50 + 200 })
                .SetTitle(new Title { Text = "" })
                //.SetSubtitle(new Subtitle { Text = "Thống kê " + DateTime.Now.Year })
                .SetExporting(new Exporting { Enabled = false })
                .SetXAxis(new XAxis
                {
                    Categories = arrDonViQuy,
                    Title = new XAxisTitle { Text = string.Empty }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Title = new YAxisTitle
                    {
                        Text = "Phần trăm (%)",
                        Align = AxisTitleAligns.High
                    },
                    Labels = new YAxisLabels
                    {
                        Overflow = "justify"
                    },
                    TickInterval = 10
                })
                .SetTooltip(new Tooltip { ValueSuffix = "%" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        DataLabels = new PlotOptionsBarDataLabels { Enabled = true }
                    }
                })
                .SetCredits(new Credits { Enabled = false })
                .SetSeries(new[]
                {
                    new Series { Name = "Bình thường", Data = new Data(arrBinhThuongQuy) },
                    new Series { Name = "Không hài lòng", Data = new Data(arrKhongHaiLongQuy) },
                    new Series { Name = "Hài lòng", Data = new Data(arrHaiLongQuy) }
                });
            ViewBag.BasicBarQuy = basicbarQuy;
            #endregion

            #region thong ke don vi thang
            // basic bar thang
            List<GetThongKeToanTP_DonVi_Thang_Result> thongkethang;
            thongkethang = ThongKeService.ThongKeToanThanhPho_Thang(DateTime.Now.Year, DateTime.Now.Month);

            string[] arrDonViThang = new string[thongkethang.Count];
            for (int i = 0; i < thongkethang.Count; i++)
            {
                arrDonViThang[i] = thongkethang[i].TenDonVi;
            }

            object[] arrHaiLongThang = new object[thongkethang.Count];
            for (int i = 0; i < thongkethang.Count; i++)
            {
                arrHaiLongThang[i] = thongkethang[i].HaiLong;
            }

            object[] arrBinhThuongThang = new object[thongkethang.Count];
            for (int i = 0; i < thongkethang.Count; i++)
            {
                arrBinhThuongThang[i] = thongkethang[i].BinhThuong;
            }

            object[] arrKhongHaiLongThang = new object[thongkethang.Count];
            for (int i = 0; i < thongkethang.Count; i++)
            {
                arrKhongHaiLongThang[i] = thongkethang[i].KhongHaiLong;
            }

            Highcharts basicbarthang = new Highcharts("basicbarthang")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Bar, Height = arrBinhThuongThang.Length * 50 + 200})
                .SetTitle(new Title { Text = "" })
                //.SetSubtitle(new Subtitle { Text = "Thống kê " + DateTime.Now.Year })
                .SetExporting(new Exporting { Enabled = false })
                .SetXAxis(new XAxis
                {
                    Categories = arrDonViThang,
                    Title = new XAxisTitle { Text = string.Empty }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Title = new YAxisTitle
                    {
                        Text = "Phần trăm (%)",
                        Align = AxisTitleAligns.High
                    },
                    Labels = new YAxisLabels
                    {
                        Overflow = "justify"
                    },
                    TickInterval = 10
                })
                .SetTooltip(new Tooltip { ValueSuffix = "%" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        DataLabels = new PlotOptionsBarDataLabels { Enabled = true }
                    }
                })
                .SetCredits(new Credits { Enabled = false })
                .SetSeries(new[]
                {
                    new Series { Name = "Bình thường", Data = new Data(arrBinhThuongThang) },
                    new Series { Name = "Không hài lòng", Data = new Data(arrKhongHaiLongThang) },
                    new Series { Name = "Hài lòng", Data = new Data(arrHaiLongThang) }
                });
            ViewBag.BasicBarThang = basicbarthang;
            #endregion

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}