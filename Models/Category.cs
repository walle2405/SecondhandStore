using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Post> Posts { get; set; }
    }
}
