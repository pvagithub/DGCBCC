using CBCC.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Bussiness;
using WebMVC.Entities;
using WebMVC.Framework.Utilities;

namespace CBCC.Areas.Admin.Controllers
{
    public class MembershipController : Controller
    {
        [MyMembershipProvider.AccessDeniedAuthorize(Roles = "Admin,Manage,User")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<Membership> roles = new List<Membership>();
            var dsRole = new List<UsersInRole>();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "UserName_desc" : "";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.DanhSachRole = UserService.GetAllRole();

            var member = UserService.MembershipGetByUserName(User.Identity.Name);
            var capnguoidung = member.CapNguoiDung != null ? member.CapNguoiDung : "";
            ViewBag.LenCDV = capnguoidung.Length;
            if (capnguoidung.Length == 2)
                roles = UserService.MembershipGetAllList();
            else
                if (capnguoidung.Length == 4)
                {
                    if (!string.IsNullOrWhiteSpace(member.MaDonVi))
                    {
                        roles = UserService.MembershipGetAllList().Where(x => x.CapNguoiDung.Length >= capnguoidung.Length && x.MaDonVi == member.MaDonVi).ToList();
                    }
                    else
                    {
                        roles = UserService.MembershipGetAllList().Where(x => x.CapNguoiDung.Length >= capnguoidung.Length).ToList();
                    }
                }
                else
                {
                    roles = UserService.MembershipGetAllList().Where(x => x.CapNguoiDung.Length > 4 && x.Username == User.Identity.Name).ToList();
                }
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
                roles = roles.Where(s => s.Username.Contains(searchString) || s.Email.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "Email":
                    roles = roles.OrderBy(s => s.Email).ToList();
                    break;

                case "Email_desc":
                    roles = roles.OrderByDescending(s => s.Email).ToList();
                    break;

                case "UserName_desc":
                    roles = roles.OrderByDescending(s => s.Username).ToList();
                    break;

                default:
                    roles = roles.OrderBy(s => s.Username).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.Page = (pageNumber - 1) * pageSize;
            return View(roles.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Admin/Membership/Details/5
        public ActionResult Details(int id = 0)
        {
            Membership membership = UserService.MembershipGetByID(id);

            if (membership == null)
            {
                return HttpNotFound();
            }
            return View(membership);
        }

        // GET: /Admin/Membership/Create
        public ActionResult Create()
        {
            var member = UserService.MembershipGetByUserName(User.Identity.Name);
            string mdvold = "";
            string mdvnew = "";
            if (!string.IsNullOrWhiteSpace(member.MaDonVi))
            {
                var arrMDV = member.MaDonVi.Split('_');
                mdvold = arrMDV[0].ToString().Trim();
                mdvnew = arrMDV[1].ToString().Trim();
            }
            if (member.CapNguoiDung.Length == 2)
                ViewBag.lsDonVi = DanhMucService.DonViGetAllList().Where(x => x.Active == true).ToList();
            else
            {
                if (!string.IsNullOrWhiteSpace(member.MaDonVi))
                {
                    ViewBag.lsDonVi = DanhMucService.DonViGetAllList().Where(x => x.Active == true && (x.MaDonVi == mdvold || x.MaLienThong == mdvnew || x.ParentDonViID == null)).ToList();
                }
                else { ViewBag.lsDonVi = DanhMucService.DonViGetAllList().Where(x => x.Active == true).ToList(); }
            }
            var item = new UsersInRolesModel();
            ViewBag.NguoiDung = CapNguoiDung(member.CapNguoiDung.Length);
            return PartialView("_Create", item);
        }

        //
        // POST: /Admin/Membership/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(UsersInRolesModel membership)
        {
            if (ModelState.IsValid)
            {
                var item = new Membership();
                item.Username = membership.Username;
                item.Password = TextUtility.GetMd5Hash(membership.Password.Trim());
                item.Email = membership.Email;
                item.Comment = membership.Comment;
                item.MaDonVi = membership.MaDonVi;
                item.CapNguoiDung = membership.CapNguoiDung.Length > 4 ? membership.CapNguoiDung.Trim() + "" + membership.Username.Trim() : membership.CapNguoiDung.Trim();
                int id = UserService.MembershipCreate(item);
                if (membership.ListRoleId != null && membership.ListRoleId.Count > 0)
                {
                    UserService.CreateUserRoleByListRoleId(membership.ListRoleId, id);
                }
                return RedirectToAction("Index");
            }
            else
                return PartialView("_Create", membership);
        }

        public ActionResult Edit(int id)
        {
            var member = UserService.MembershipGetByUserName(User.Identity.Name);
            string mdvold = "";
            string mdvnew = "";
            if (!string.IsNullOrWhiteSpace(member.MaDonVi))
            {
                var arrMDV = member.MaDonVi.Split('_');
                mdvold = arrMDV[0].ToString().Trim();
                mdvnew = arrMDV[1].ToString().Trim();
            }
            if (member.CapNguoiDung.Length == 2)
                ViewBag.lsDonVi = DanhMucService.DonViGetAllList().Where(x => x.Active == true).ToList();
            else
            {
                if (!string.IsNullOrWhiteSpace(member.MaDonVi))
                {
                    ViewBag.lsDonVi = DanhMucService.DonViGetAllList().Where(x => x.Active == true && (x.MaDonVi == mdvold || x.MaLienThong == mdvnew || x.ParentDonViID == null)).ToList();
                }
                else { ViewBag.lsDonVi = DanhMucService.DonViGetAllList().Where(x => x.Active == true).ToList(); }
            }
            ViewBag.NguoiDung = CapNguoiDung(member.CapNguoiDung.Length);
            UsersInRolesModel userRole = new UsersInRolesModel();
            List<int> lstID = new List<int>();
            var item = UserService.GetMemberByUserId(id, out lstID);
            userRole.UserId = item.UserId;
            userRole.Username = item.Username;
            userRole.Password = item.Password;
            userRole.Email = item.Email;
            userRole.Comment = item.Comment;
            userRole.MaDonVi = item.MaDonVi;
            userRole.CapNguoiDung = item.CapNguoiDung.Length > 4 ? item.CapNguoiDung.Substring(0, 6) : item.CapNguoiDung;
            userRole.ListRoleId = lstID;
            return PartialView("_Edit", userRole);
        }

        [HttpPost]
        public ActionResult Edit(UsersInRolesModel membership)
        {
            if (ModelState.IsValid)
            {
                var item = new Membership();
                item.UserId = membership.UserId;
                item.Username = membership.Username;
                item.Password = TextUtility.GetMd5Hash(membership.Password.Trim());
                item.Email = membership.Email;
                item.Comment = membership.Comment;
                item.MaDonVi = membership.MaDonVi;
                item.CapNguoiDung = membership.CapNguoiDung.Length > 4 ? membership.CapNguoiDung.Trim() + "" + membership.Username.Trim() : membership.CapNguoiDung.Trim();
                UserService.MembershipUpdate(item, membership.Password.Trim());
                // xoa cac role cua user hien tai
                UserService.DeleteUserRoleByUserID(membership.UserId);
                if (membership.ListRoleId != null && membership.ListRoleId.Count > 0)
                {
                    // add role moi
                    UserService.CreateUserRoleByListRoleId(membership.ListRoleId, membership.UserId);
                }
                return RedirectToAction("Index");
            }
            else
                return PartialView("_Edit", membership);
        }

        public ActionResult Delete(int id = 0)
        {
            var item = UserService.MembershipGetByID(id);
            if (item == null) HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            UserService.MembershipDelete(id);
            return RedirectToAction("Index");
        }

        public JsonResult IsExistUser(int id, string userName)
        {
            bool isExist = UserService.IsExistUserName(id, userName);
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Profile()
        {
            string userName = User.Identity.Name;
            UsersInRolesModel userRole = new UsersInRolesModel();
            Membership member = UserService.GetUserByUserName(userName);
            List<int> lstID = new List<int>();
            var item = UserService.GetMemberByUserId(member.UserId, out lstID);

            userRole.UserId = member.UserId;
            userRole.Username = member.Username;
            userRole.Password = member.Password;
            userRole.Email = member.Email;
            userRole.Comment = member.Comment;
            userRole.MaDonVi = item.MaDonVi;
            userRole.CapNguoiDung = item.CapNguoiDung;
            userRole.ListRoleId = lstID;

            return View(userRole);
        }

        [HttpPost]
        public ActionResult Profile(Membership membership)
        {
            if (ModelState.IsValid)
            {
                var item = new Membership();
                item.UserId = membership.UserId;
                item.Username = membership.Username;
                item.Password = TextUtility.GetMd5Hash(membership.Password.Trim());
                item.Email = membership.Email;
                item.Comment = membership.Comment;
                item.MaDonVi = membership.MaDonVi;
                item.CapNguoiDung = membership.CapNguoiDung;
                UserService.MembershipUpdate(item, membership.Password.Trim());

                return RedirectToAction("Index");
            }
            else
                return View(membership);
        }

        //mã hóa md5 pass
        public static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }

        public static string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();

            //
            // GET: /User/Edit/5
        }
        private DataTable CapNguoiDung(int len)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ma", typeof(string));
            dt.Columns.Add("ten", typeof(string));

            if (len == 2)
            {
                var row = dt.NewRow();
                row[0] = "01";
                row[1] = "Quản trị hệ thống";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[0] = "0101";
                row[1] = "Quản trị đơn vị";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[0] = "010101";
                row[1] = "Người dùng";
                dt.Rows.Add(row);
            }
            if (len == 4)
            {
                var row = dt.NewRow();
                row[0] = "0101";
                row[1] = "Quản trị đơn vị";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[0] = "010101";
                row[1] = "Người dùng";
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}