using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Post
    {
        public Post()
        {
            ExchangeOrders = new HashSet<ExchangeOrder>();
            Reviews = new HashSet<Review>();
        }

        public int PostId { get; set; }
        public string AccountId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool PostStatus { get; set; }
        public int CategoryId { get; set; }
        public string PostType { get; set; } = null!;
        public int PointCost { get; set; }
        public DateTime PostDate { get; set; }
        public int PostPriority { get; set; }
        public DateTime PostExpiryDate { get; set; }
        public double Price { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<ExchangeOrder> ExchangeOrders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
