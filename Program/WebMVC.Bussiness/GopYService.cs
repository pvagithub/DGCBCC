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
    }
}
