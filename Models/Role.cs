using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public string RoleId { get; set; } = null!;
        public string RoleName { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
