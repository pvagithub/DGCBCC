using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebMVC.Dal;
using WebMVC.Dal.Extensions;
using WebMVC.Entities;

namespace WebMVC.Bussiness
{
    public class UserService
    {
        #region Roles

        public static List<Role> RolesGetAllList()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();

                IQueryable<Role> query = context.Roles;

                return query.Where(x => x.IsDelete == false).OrderByDescending(x => x.RoleId).AsNoTracking().ToList();
            }
        }

        public static void CreateRoles(Role mRole)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                context.Roles.Add(mRole);
                context.SaveChanges();
            }
        }

        public static Role GetRoles(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.Roles.Find(id);
            }
        }

        public static void UpdateRoles(Role mRole)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                var item = context.Roles.Find(mRole.RoleId);
                item.RoleName = mRole.RoleName;
                item.Description = mRole.Description;
                context.SaveChanges();
            }
        }

        public static void DeleteRols(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                var item = context.Roles.Find(id);
                item.IsDelete = true;
                context.SaveChanges();
            }
        }

        #endregion Roles

        #region Membership

        public static Membership GetMemberByUserId(int id, out List<int> lstIDRole)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                // danh sách roleid lấy từ userid = id
                lstIDRole = context.UsersInRoles.Where(x => x.UserId == id).Select(x => x.RoleId.Value).ToList();
                return context.Memberships.Where(x => x.UserId == id).FirstOrDefault();
            }
        }

        public static Membership MembershipGetByID(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();

                return context.Memberships.Where(x => x.UserId == id).FirstOrDefault();
            }
        }
        public static Membership MembershipGetByUserName(string userName)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();

                return context.Memberships.Where(x => x.Username == userName).FirstOrDefault();
            }
        }
        public static List<Membership> MembershipGetAllList()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                IQueryable<Membership> query = context.Memberships;

                return query.Include(x => x.UsersInRoles).OrderByDescending(x => x.UserId).AsNoTracking().ToList();
            }
        }

        public static int MembershipCreate(Membership memberShip)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                context.Memberships.Add(memberShip);
                context.SaveChanges();
                return memberShip.UserId;
            }
        }
        public static bool IsExistUserName(int id, string userName)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                if (id == 0)
                {
                    var user = context.Memberships.Where(x => x.Username.Trim() == userName.Trim()).FirstOrDefault();
                    return (user != null);
                }
                else
                {
                    var user = context.Memberships.Where(x => x.Username.Trim() == userName.Trim()).FirstOrDefault();

                    if (user != null)
                    {
                        if (user.UserId == id)
                            return false;
                        else
                            return true;
                    }
                    else return false;
                }
            }
        }

        public static void MembershipUpdate(Membership memberShip, string newPassWord)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                var item = context.Memberships.Find(memberShip.UserId);
                item.Username = memberShip.Username;
                if (item.Password != newPassWord)
                    item.Password = memberShip.Password;
                item.Email = memberShip.Email;
                item.Comment = memberShip.Comment;
                context.SaveChanges();
            }
        }

        public static void MembershipDelete(int userId)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                var item = context.Memberships.Where(x => x.UserId == userId).FirstOrDefault();
                // xóa các role của user input
                context.UsersInRoles.RemoveRange(item.UsersInRoles);
                // xóa user từ userid input
                context.Memberships.Remove(item);
                context.SaveChanges();
            }
        }

        public static Membership GetUserByUserName(string userName)
        {
            var MembershipService = new MyMembershipProvider();
            return MembershipService.GetUserByUserName(userName);
        }

        #endregion Membership

        #region User_Roles

        public static void DeleteUserRoleByUserID(int id)
        {
            using (var context = new DataModelEntities())
            {
                context.ReadCommited();
                var item = context.UsersInRoles.Where(x => x.UserId == id).ToList();
                context.UsersInRoles.RemoveRange(item);
                context.SaveChanges();
            }
        }

        public static List<UsersInRole> GetAllRole()
        {
            using (var context = new DataModelEntities())
            {
                context.ReadUncommited();
                return context.UsersInRoles.Include(x => x.Role).ToList();
            }
        }

        public static void CreateUserRoleByListRoleId(List<int> lstID, int userId)
        {
            using (var context = new DataModelEntities())
            {
                List<UsersInRole> lstRole = new List<UsersInRole>();
                for (int i = 0; i < lstID.Count; i++)
                {
                    var user = new UsersInRole();
                    user.RoleId = lstID[i];
                    user.UserId = userId;
                    lstRole.Add(user);
                }
                if (lstRole.Count > 0)
                {
                    context.ReadCommited();
                    context.UsersInRoles.AddRange(lstRole);
                    context.SaveChanges();
                }
            }
        }

        #endregion User_Roles
    }
}