using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
            Permissions = new HashSet<Permission>();
        }

        public string RoleId { get; set; } = null!;
        public string RoleName { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
