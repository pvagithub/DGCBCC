using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBCC.Models.GopY
{
    public enum TypeControl
    {
        Checkbox=1,
        Textbox = 2
    };
    public class GopYHTViewModel
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public List<AnswerModel> lsAnswers { get; set; }
    }
    public class AnswerModel
    {
        public int ID { get; set; }
        public string Answer { get; set; }
        public TypeControl Type { get; set; }
    }
}