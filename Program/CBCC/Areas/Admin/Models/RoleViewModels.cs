using System;

namespace CBCC.Areas.Admin.Models
{


    public class RoleViewModels
    {
        public int RoleId { get; set; }
        public Nullable<System.Guid> ApplicationId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
