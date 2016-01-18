using System.Collections.Generic;
using System.Linq;
using WebMVC.Dal;
using WebMVC.Dal.Extensions;
using WebMVC.Entities;

namespace WebMVC.Bussiness
{
    public class GopYService
    {
        public static int SaveGopY(GopY model)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                var item = context.Gopies.Add(model);
                return context.SaveChanges();
            }
        }
        public static List<GopYQuestion> GetGopYQuestions()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.GopYQuestions.ToList();
            }
        }
        public static List<GopYAnswer> GetGopYAnswers()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.GopYAnswers.ToList();
            }
        }
    }
}
