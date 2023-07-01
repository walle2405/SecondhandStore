using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class PostType
    {
        public PostType()
        {
            Posts = new HashSet<Post>();
        }

        public int PostTypeId { get; set; }
        public string PostTypeName { get; set; } = null!;

        public virtual ICollection<Post> Posts { get; set; }
    }
}
