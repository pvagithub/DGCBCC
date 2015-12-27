using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebMVC.Bussiness;
using WebMVC.Entities;

namespace CBCC.Models
{
    public class UsersInRolesModel : Membership
    {
        public List<SelectListItem> ListDanhSachRole { get; set; }
        public List<int> ListRoleId { get; set; }

        public UsersInRolesModel()
        {
            ListDanhSachRole = new List<SelectListItem>();
            ListDanhSachRole.AddRange(UserService.RolesGetAllList().Select(x => new SelectListItem { Text = x.RoleName, Value = x.RoleId.ToString() }).ToList());
        }
    }
}