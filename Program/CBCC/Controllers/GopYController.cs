using CBCC.Models.GopY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;

namespace CBCC.Controllers
{
    public class JsonObject
    {
        public string name { get; set; }
        public string value { get; set; }
    }
    public class NameOfKey
    {
        public readonly string ChapNhan = "Chấp nhận";
        public readonly string QuaNhieu = "Quá nhiều";
        public readonly string KhoHieu = "Câu từ khó hiểu";
        public readonly string KhoSD = "Khó sử dụng";
    }
    public class GopYController : Controller
    {
        public static Dictionary<string, string> NameOfKey = null;
        public GopYController()
        {
            NameOfKey = new Dictionary<string, string>();
            NameOfKey.Add("1", "Chấp nhận");
            NameOfKey.Add("2", "Quá nhiều");
            NameOfKey.Add("3", "Câu từ khó hiểu");
            NameOfKey.Add("4", "Khó sử dụng");
        }
        // GET: GopY
        public ActionResult Index(string sbn="")
        {
            ViewBag.IsGopY = true;
            List<GopYHTViewModel> list = new List<GopYHTViewModel>();
            var lsQuestions = GopYService.GetGopYQuestions();
            var lsAnswers = GopYService.GetGopYAnswers();
            foreach (var item in lsQuestions)
            {
                List<AnswerModel> lsAnwsersModel = new List<AnswerModel>();
                var idAns = item.IDAnswers.Split(',');
                for (int i = 0; i < idAns.Length; i++)
                {
                    var ans = lsAnswers.Where(p => p.ID == Convert.ToInt32(idAns[i])).FirstOrDefault();
                    lsAnwsersModel.Add(new AnswerModel()
                    {
                        ID = ans.ID,
                        Answer = ans.Answer,
                        Type = ans.Type==1?TypeControl.Checkbox:TypeControl.Textbox
                    });
                }
                GopYHTViewModel ht = new GopYHTViewModel()
                {
                    ID = item.ID,
                    Question = item.Question,
                    lsAnswers = lsAnwsersModel
                };
                list.Add(ht);
            }
            TempData["sbn"] = sbn;
            return View(list);
        }
        [HttpPost]
        public bool SaveGopY(List<JsonObject> lsdata)
        {
            int count = 0;
            var lsAnswers = lsdata.Where(p => (p.name.StartsWith("1") || p.name.StartsWith("2") || p.name.StartsWith("3") || p.name.StartsWith("4")) && p.value != null).ToList();
            try
            {
                var sbn = TempData["sbn"].ToString();
                for (int i = 0; i < lsAnswers.Count; i++)
                {
                    //var rs = NameOfKey.ContainsKey(lsAnswers[i].value);
                    //int n=0;
                    //bool isNumeric = int.TryParse(lsAnswers[i].value, out n);
                    var idKhac = lsdata.Where(p=>p.name.Equals("h_"+lsAnswers[i].name.Substring(0,1))).FirstOrDefault();

                    var GopY = new WebMVC.Entities.GopY()
                   {
                       IDCau = i + 1,
                       MaTL =lsAnswers[i].name.ToLower().Contains("khac")?idKhac.value:lsAnswers[i].name.Remove(0,1),
                       NoiDungTL = NameOfKey.ContainsKey(lsAnswers[i].value) ?  "" :lsAnswers[i].value,
                       CreatedDate = DateTime.Now
                   };
                    count += GopYService.SaveGopY(GopY);

                }
                //var otherAnswer = lsdata.Where(p => p.name.StartsWith("4") && p.value != null).FirstOrDefault();
                //var GopYKhac = new WebMVC.Entities.GopY()
                //{
                //    IDCau = 4,
                //    MaTL = otherAnswer.name,
                //    NoiDungTL = otherAnswer.value
                //};
                //count += GopYService.SaveGopY(GopYKhac);
            }
            catch (Exception ex)
            {
                return false;
            }


            return count == lsAnswers.Count;
        }

        public ActionResult Thongbao() { return View(); }
    }
}