using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

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
