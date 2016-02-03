using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;

namespace CBCC.Areas.Admin.Controllers
{
    public class GenDataController : Controller
    {
        public JsonResult ClearDataBase()
        {
            bool result = false;
            try
            {
                DanhMucService.ClearDataBase();
                result = true;
            }
            catch (System.Exception e)
            {
                result = false;
                throw e;
            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenDataTest()
        {
            bool result = false;
            try
            {
                ClearDataBase();
                GenHoSo();
                GendataQuiTrinhHoSo();
                GendataDanhGia();

                GenHoSo();
                GendataQuiTrinhHoSo_ChuaDanhGia();
                result = true;
            }
            catch (System.Exception e)
            {
                result = false;
                throw e;
            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenHoSo()
        {
            bool result = false;
            try
            {
                /// Match DonVi Va LinhVuc

                var LstDonVi = DanhMucService.DonViGetAllList();
                Random r = new Random();
                ////Tao ho so

                foreach (var item in LstDonVi)
                {
                    System.Threading.Thread.Sleep(3000);
                    var donvi = (DonVi)item;
                    var lstThuTuc = DanhMucService.ThuTucGetAllList_ByDonViID(donvi.DonViID);

                    var lstHoSo = new List<HoSo>();
                    for (int i = 0; i < 50; i++)
                    {
                        var thutuc = lstThuTuc.OrderBy(x => r.Next()).Take(1).First();

                        var hoso = new HoSo();
                        hoso.DonViID = donvi.DonViID;
                        hoso.TenDonVi = donvi.TenDonVi;
                        hoso.SoBienNhan = DateTime.Now.Ticks.ToString() + "_" + i.ToString() + "_" + donvi.DonViID.ToString();
                        hoso.LinhVucID = thutuc.LinhVuc.LinhVucID;
                        hoso.TenLinhVuc = thutuc.LinhVuc.TenLinhVuc;
                        hoso.ThuTucID = thutuc.ThuTucID;
                        hoso.TenThuTuc = thutuc.TenThucTuc;
                        hoso.NgayNhan = DateTime.Now;

                        lstHoSo.Add(hoso);
                    }

                    HoSoService.HoSoListCreate(lstHoSo);
                }

                result = true;
            }
            catch (System.Exception e)
            {
                result = false;
                throw e;
            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GendataQuiTrinhHoSo()
        {
            bool result = false;
            try
            {
                Random r = new Random();

                var Lst_HoSo = HoSoService.HoSoGetAll();

                foreach (var item in Lst_HoSo)
                {
                    var Lst_QuaTrinh = new List<QuaTrinhXuLy>();
                    var Lst_ThongTinNguoiXuLy = new List<ThongTinNguoiXuLy>();

                    var hoso = (HoSo)item;
                    var quatrinh = new WebMVC.Entities.QuaTrinhXuLy();
                    quatrinh.HoSoID = hoso.ID.ToString();
                    quatrinh.DonViID = hoso.DonViID;
                    quatrinh.NguoiXuLy = "TamPV";
                    quatrinh.NguoiXuLyID = 1;
                    quatrinh.NgayNhan = DateTime.Now.AddDays(-10);
                    quatrinh.TinhTrang = "Tiếp nhận";
                    quatrinh.KetQua = "Đã tiếp nhận";

                    var quatrinh1 = new WebMVC.Entities.QuaTrinhXuLy();
                    quatrinh1.HoSoID = hoso.ID.ToString();
                    quatrinh1.DonViID = hoso.DonViID;
                    quatrinh1.NguoiXuLyID = 2;

                    quatrinh1.NgayNhan = DateTime.Now.AddDays(-9);
                    quatrinh1.NguoiXuLy = "TamPV";
                    quatrinh1.TinhTrang = "Xử lý";
                    quatrinh1.KetQua = "Đã Xử lý";

                    var quatrinh2 = new WebMVC.Entities.QuaTrinhXuLy();
                    quatrinh2.HoSoID = hoso.ID.ToString();
                    quatrinh2.DonViID = hoso.DonViID;
                    quatrinh2.NguoiXuLyID = 3;
                    quatrinh2.NguoiXuLy = "Trinm";
                    quatrinh2.NgayNhan = DateTime.Now.AddDays(-8);
                    quatrinh2.TinhTrang = "Trả kết quả";
                    quatrinh2.KetQua = "Đã tra kết quả";

                    Lst_QuaTrinh.Add(quatrinh);
                    Lst_QuaTrinh.Add(quatrinh1);
                    Lst_QuaTrinh.Add(quatrinh2);

                    HoSoService.QuaTrinhListCreate(Lst_QuaTrinh);

                    var nguoiXuLy = new ThongTinNguoiXuLy();
                    nguoiXuLy.HoSoID = hoso.ID.ToString();
                    nguoiXuLy.DonViID = hoso.DonViID;
                    nguoiXuLy.HoTen = "Nguyễn Văn Tâm";
                    nguoiXuLy.NgaySinh = DateTime.Parse("1/1/1987");
                    nguoiXuLy.ChucVu = "Chuyên Viên";
                    nguoiXuLy.PhongBan = "Phòng tiếp nhận hô sơ";

                    var nguoiXuLy1 = new ThongTinNguoiXuLy();
                    nguoiXuLy1.HoSoID = hoso.ID.ToString();
                    nguoiXuLy1.DonViID = hoso.DonViID;
                    nguoiXuLy1.HoTen = "Lê Quốc Việt";
                    nguoiXuLy1.NgaySinh = DateTime.Parse("1/1/1990");
                    nguoiXuLy1.ChucVu = "Chuyên Viên";
                    nguoiXuLy1.PhongBan = "Phòng tiếp nhận hô sơ";

                    var nguoiXuLy2 = new ThongTinNguoiXuLy();
                    nguoiXuLy2.HoSoID = hoso.ID.ToString();
                    nguoiXuLy2.DonViID = hoso.DonViID;
                    nguoiXuLy2.HoTen = "Nguyễn Minh Trí";
                    nguoiXuLy2.NgaySinh = DateTime.Parse("1/1/1989");
                    nguoiXuLy2.ChucVu = "Chuyên Viên";
                    nguoiXuLy2.PhongBan = "Phòng tiếp nhận hô sơ";

                    Lst_ThongTinNguoiXuLy.Add(nguoiXuLy);
                    Lst_ThongTinNguoiXuLy.Add(nguoiXuLy1);
                    Lst_ThongTinNguoiXuLy.Add(nguoiXuLy2);

                    HoSoService.ThongTinNguoiXuLyListCreate(Lst_ThongTinNguoiXuLy);
                }

                result = true;
            }
            catch (System.Exception e)
            {
                result = false;
                throw e;
            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GendataQuiTrinhHoSo_ChuaDanhGia()
        {
            bool result = false;
            try
            {
                Random r = new Random();

                var Lst_HoSo = HoSoService.HoSoGetAll();

                foreach (var item in Lst_HoSo.Where(x => x.DaDanhGia == null).ToList())
                {
                    var Lst_QuaTrinh = new List<QuaTrinhXuLy>();
                    var Lst_ThongTinNguoiXuLy = new List<ThongTinNguoiXuLy>();

                    var hoso = (HoSo)item;
                    var quatrinh = new WebMVC.Entities.QuaTrinhXuLy();
                    quatrinh.HoSoID = hoso.ID.ToString();
                    quatrinh.DonViID = hoso.DonViID;
                    quatrinh.NguoiXuLy = "TamPV";
                    quatrinh.NguoiXuLyID = 1;
                    quatrinh.NgayNhan = DateTime.Now.AddDays(-10);
                    quatrinh.TinhTrang = "Tiếp nhận";
                    quatrinh.KetQua = "Đã tiếp nhận";

                    var quatrinh1 = new WebMVC.Entities.QuaTrinhXuLy();
                    quatrinh1.HoSoID = hoso.ID.ToString();
                    quatrinh1.DonViID = hoso.DonViID;
                    quatrinh1.NguoiXuLyID = 2;

                    quatrinh1.NgayNhan = DateTime.Now.AddDays(-9);
                    quatrinh1.NguoiXuLy = "Vietlq";
                    quatrinh1.TinhTrang = "Xử lý";
                    quatrinh1.KetQua = "Đã Xử lý";

                    var quatrinh2 = new WebMVC.Entities.QuaTrinhXuLy();
                    quatrinh2.HoSoID = hoso.ID.ToString();
                    quatrinh2.DonViID = hoso.DonViID;
                    quatrinh2.NguoiXuLyID = 3;
                    quatrinh2.NguoiXuLy = "Trinm";
                    quatrinh2.NgayNhan = DateTime.Now.AddDays(-8);
                    quatrinh2.TinhTrang = "Trả kết quả";
                    quatrinh2.KetQua = "Đã tra kết quả";

                    Lst_QuaTrinh.Add(quatrinh);
                    Lst_QuaTrinh.Add(quatrinh1);
                    Lst_QuaTrinh.Add(quatrinh2);

                    HoSoService.QuaTrinhListCreate(Lst_QuaTrinh);

                    var nguoiXuLy = new ThongTinNguoiXuLy();
                    nguoiXuLy.HoSoID = hoso.ID.ToString();
                    nguoiXuLy.DonViID = hoso.DonViID;
                    nguoiXuLy.HoTen = "Nguyễn Văn Tâm";
                    nguoiXuLy.NgaySinh = DateTime.Parse("1/1/1987");
                    nguoiXuLy.ChucVu = "Chuyên Viên";
                    nguoiXuLy.PhongBan = "Phòng tiếp nhận hô sơ";

                    var nguoiXuLy1 = new ThongTinNguoiXuLy();
                    nguoiXuLy1.HoSoID = hoso.ID.ToString();
                    nguoiXuLy1.DonViID = hoso.DonViID;
                    nguoiXuLy1.HoTen = "Lê Quốc Việt";
                    nguoiXuLy1.NgaySinh = DateTime.Parse("1/1/1990");
                    nguoiXuLy1.ChucVu = "Chuyên Viên";
                    nguoiXuLy1.PhongBan = "Phòng tiếp nhận hô sơ";

                    var nguoiXuLy2 = new ThongTinNguoiXuLy();
                    nguoiXuLy2.HoSoID = hoso.ID.ToString();
                    nguoiXuLy2.DonViID = hoso.DonViID;
                    nguoiXuLy2.HoTen = "Nguyễn Minh Trí";
                    nguoiXuLy2.NgaySinh = DateTime.Parse("1/1/1989");
                    nguoiXuLy2.ChucVu = "Chuyên Viên";
                    nguoiXuLy2.PhongBan = "Phòng tiếp nhận hô sơ";

                    Lst_ThongTinNguoiXuLy.Add(nguoiXuLy);
                    Lst_ThongTinNguoiXuLy.Add(nguoiXuLy1);
                    Lst_ThongTinNguoiXuLy.Add(nguoiXuLy2);

                    HoSoService.ThongTinNguoiXuLyListCreate(Lst_ThongTinNguoiXuLy);
                }

                result = true;
            }
            catch (System.Exception e)
            {
                result = false;
                throw e;
            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Gendata()
        {
            bool result = false;
            try
            {
                /// Match DonVi Va LinhVuc

                var LstDonVi = DanhMucService.DonViGetAllList();

                var LstLinhVuc = DanhMucService.LinhVucGetAllList();

                Random r = new Random();

                DanhMucService.LinhVucDonViDeleteAll();

                foreach (var donvi in LstDonVi)
                {
                    IEnumerable<LinhVuc> linhvucRan = LstLinhVuc.OrderBy(x => r.Next()).Take(10);
                    List<LinhVucDonVi> lstLinhVucDonVi = new List<LinhVucDonVi>();
                    foreach (var lv in linhvucRan)
                    {
                        var tmp = new LinhVucDonVi();

                        tmp.Active = true;
                        tmp.LinhVucID = lv.LinhVucID;
                        tmp.DonViID = donvi.DonViID;
                        lstLinhVucDonVi.Add(tmp);
                    }

                    DanhMucService.DonViLinhVucListCreate(lstLinhVucDonVi);
                }

                result = true;

                /// Tao Danh gia cho tung ho so
                /// Tao KetQuaDanhGia
            }
            catch (System.Exception e)
            {
                result = false;
                throw e;
            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GendataDanhGia()
        {
            bool result = false;
            try
            {
                var Lst_CauHoi = DanhMucService.TieuChiGetAll();

                var Lst_HoSo = HoSoService.HoSoGetAll();

                foreach (var item in Lst_HoSo.Where(x => x.DaDanhGia == null).ToList())
                {
                    var hoso = (HoSo)item;
                    var danhgia = new WebMVC.Entities.DanhGia();
                    danhgia.HoSoID = hoso.HoSoID;
                    danhgia.SoBienNhan = hoso.SoBienNhan;
                    danhgia.DonViID = hoso.DonViID;
                    danhgia.TenDonVi = hoso.TenDonVi;
                    danhgia.LinhVucID = hoso.LinhVucID;
                    danhgia.TenLinhVuc = hoso.TenLinhVuc;
                    danhgia.ThuTucID = hoso.ThuTucID;
                    danhgia.TenThuTuc = hoso.TenThuTuc;
                    danhgia.TenThuTuc = hoso.TenThuTuc;
                    danhgia.NgayDanhGia = DateTime.Now;

                    var lstKQDG = new List<KetQuaDanhGia>();

                    foreach (var cauhoi in Lst_CauHoi)
                    {
                        if (cauhoi.TypeInput == 1)
                        {
                            var kq = new KetQuaDanhGia();
                            var lstCauTraLoi = DanhMucService.TieuChiCauTraLoiGetAll_ByTieuChiID(cauhoi.ID, true).Select(x => x.CauTraLoiID);
                            Random r = new Random();

                            var IdCauTraLoiRan = lstCauTraLoi.OrderBy(x => r.Next()).Take(1).First();

                            kq.TieuChiID = cauhoi.ID;
                            kq.CauTraLoiID = IdCauTraLoiRan;
                            kq.CauTraLoiID = IdCauTraLoiRan;
                            kq.TypeInput = 1;
                            lstKQDG.Add(kq);
                        }
                        else
                        {
                            var kq = new KetQuaDanhGia();

                            kq.TieuChiID = cauhoi.ID;
                            kq.TypeInput = 2;
                            lstKQDG.Add(kq);
                        }
                    }

                    DanhGiaService.SaveKetQuaDanhGia(danhgia, lstKQDG, item.ID,1,"0");
                }

                result = true;
            }
            catch (System.Exception e)
            {
                result = false;
                throw e;
            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
    }
}