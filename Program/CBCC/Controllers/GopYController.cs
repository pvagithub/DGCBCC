using CBCC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
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
            NameOfKey.Add("ChapNhan", "Chấp nhận");
            NameOfKey.Add("QuaNhieu", "Quá nhiều");
            NameOfKey.Add("KhoHieu", "Câu từ khó hiểu");
            NameOfKey.Add("KhoSD", "Khó sử dụng");
        }
        // GET: GopY
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public bool SaveGopY(List<JsonObject> lsdata)
        {
            int count = 0;
            var lsAnswers = lsdata.Where(p => (p.name.StartsWith("1") || p.name.StartsWith("2") || p.name.StartsWith("3") || p.name.StartsWith("4")) && p.value != null).ToList();
            try
            {
                for (int i = 0; i < lsAnswers.Count; i++)
                {
                    var GopY = new WebMVC.Entities.GopY()
                   {
                       IDCau = i + 1,
                       MaTL = NameOfKey.ContainsKey(lsAnswers[i].value) ? lsAnswers[i].value : "Khac",
                       NoiDungTL = NameOfKey.ContainsKey(lsAnswers[i].value) ?  NameOfKey[lsAnswers[i].value] :lsAnswers[i].value
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
    }
}