using System.Web.Mvc;
using WebMVC.Bussiness;

namespace CBCC.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]
        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            return View();
        }
    }
}