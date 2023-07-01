using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = null!;
        public string RoleId { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }
}
