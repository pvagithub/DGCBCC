using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;

using WebMVC.Entities;

namespace CBCC.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin")]
        //
        // GET: /Admin/Role/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.RoleNameSortParm = String.IsNullOrEmpty(sortOrder) ? "RoleName_desc" : "";
            ViewBag.DescriptionSortParm = sortOrder == "Description" ? "Description_desc" : "Description";
            var roles = UserService.RolesGetAllList();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                roles = roles.Where(s => s.RoleName.Contains(searchString)
                                       || s.Description.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "Description":
                    roles = roles.OrderBy(s => s.Description).ToList(); ;
                    break;

                case "Description_desc":
                    roles = roles.OrderByDescending(s => s.Description).ToList(); ;
                    break;

                case "RoleName_desc":
                    roles = roles.OrderByDescending(s => s.RoleName).ToList(); ;
                    break;

                default:
                    roles = roles.OrderBy(s => s.RoleName).ToList(); ;
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(roles.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            var role = new Role();
            return PartialView("_Create", role);
        }
        [HttpPost]
        public ActionResult Create(Role mRole)
        {
            if (ModelState.IsValid)
            {
                var role = new Role()
                {
                    RoleName = mRole.RoleName,
                    Description = mRole.Description,
                    IsDelete = false
                };
                UserService.CreateRoles(role);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("_Create", mRole);
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var mRole = UserService.GetRoles(id);
            return PartialView("_Edit", mRole);
        }
        [HttpPost]
        public ActionResult Edit(Role mRole)
        {
            if (ModelState.IsValid)
            {
                UserService.UpdateRoles(mRole);
                return RedirectToAction("Index");
            }
            else
                return View();
        }
        public ActionResult Delete(int id = 0)
        {
            var item = UserService.GetRoles(id);
            if (item == null)
                HttpNotFound();
            return PartialView("_Delete", item);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserService.DeleteRols(id);
            return RedirectToAction("Index");
        }
    }
}