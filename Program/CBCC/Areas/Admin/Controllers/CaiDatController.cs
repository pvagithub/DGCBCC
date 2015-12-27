using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;
using WebMVC.Framework.Utilities;

namespace CBCC.Areas.Admin.Controllers
{
    public class CaiDatController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]
        //
        // GET: /Admin/CaiDat/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CapNhatDanhGia()
        {
            bool result = false;
            try
            {
                var kq = CreateData();
                string strSave = JsonConvert.SerializeObject(kq);

                string pathFile = AppDomain.CurrentDomain.BaseDirectory + @"\SPA\data\csharp.js";

                if (FileUtility.ReadAndEditFile(pathFile, strSave))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (System.Exception)
            {
                result = false;
            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        #region Cập nhật đánh giá

        #region Class

        public class DanhGia
        {
            public quiz quiz { get; set; }

            public List<CauHoi> questions { get; set; }
        }

        public class quiz
        {
            public int Id { get; set; }

            public string name { get; set; }

            public string description { get; set; }
        }

        public class CauHoi
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int QuestionTypeId { get; set; }

            public List<Option> Options { get; set; }

            public QuestionType QuestionType { get; set; }
        }

        public class Option
        {
            public int Id { get; set; }

            public int QuestionId { get; set; }

            public string Name { get; set; }

            public bool IsAnswer { get; set; }
        }

        public class QuestionType
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public bool IsActive { get; set; }
        }

        #endregion Class

        public DanhGia CreateData()
        {
            var _questions = new DanhGia();

            ///Thông tin tile trên header bài đánh giá
            quiz thongTin = new quiz();
            thongTin.Id = 1;
            thongTin.name = "";
            //thongTin.description = "Phần mềm đánh giá trực tuyến";
            _questions.quiz = thongTin;

            ///Get toàn bộ câu hỏi và thông tin các option của cau hỏi
            var Lst_CauHoi = DanhMucService.TieuChiGetAll();

            //Get all các cau tra loi

            var Lst_CauHoiTraLoi = DanhMucService.CauTraLoiGetAll();

            var lstCauHoi = new List<CauHoi>();
            for (int i = 0; i < Lst_CauHoi.Count; i++)
            {
                bool sortTang = Convert.ToBoolean(ConfigurationSettings.AppSettings["Sort_GiaTri"]);
                var lstTraloi = DanhMucService.TieuChiCauTraLoiGetAll_ByTieuChiID(Lst_CauHoi[i].ID, sortTang);

                var lstOption = new List<Option>();

                if (Lst_CauHoi[i].TypeInput == 1)
                {
                    foreach (var item in lstTraloi)
                    {
                        var _option = new Option { Id = item.CauTraLoi.ID, QuestionId = Lst_CauHoi[i].ID, Name = item.CauTraLoi.TenCauTraLoi, IsAnswer = false };
                        lstOption.Add(_option);
                    }
                    var _QuestionType = new QuestionType { Id = 1, Name = "Multiple Choice", IsActive = true };
                    var cauhoi = new CauHoi { Id = Lst_CauHoi[i].ID, Name = Lst_CauHoi[i].TenTieuChi, QuestionTypeId = 1, Options = lstOption, QuestionType = _QuestionType };

                    lstCauHoi.Add(cauhoi);
                }
                else
                {
                    var _QuestionType = new QuestionType { Id = 2, Name = "Multiple Choice", IsActive = true };

                    var _option = new Option { Id = Lst_CauHoi[i].ID, QuestionId = Lst_CauHoi[i].ID, Name = "", IsAnswer = false };
                    lstOption.Add(_option);

                    var cauhoi = new CauHoi { Id = Lst_CauHoi[i].ID, Name = Lst_CauHoi[i].TenTieuChi, QuestionTypeId = Lst_CauHoi[i].TypeInput.Value, Options = lstOption, QuestionType = _QuestionType };

                    lstCauHoi.Add(cauhoi);
                }
            }

            _questions.questions = lstCauHoi;

            return _questions;
        }

        #endregion Cập nhật đánh giá

    }
}