using System;
using System.Collections.Generic;

#nullable disable

namespace SecondhandStore.Models
{
    public partial class Role
    {
        public Role()
        {
            Permissions = new HashSet<Permission>();
        }

        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
