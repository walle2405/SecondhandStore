using System;
using System.Collections.Generic;

#nullable disable

namespace SecondhandStore.Models
{
    public partial class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
