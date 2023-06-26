using System;
using System.Collections.Generic;

#nullable disable

namespace SecondhandStore.Models
{
    public partial class Post
    {
        public Post()
        {
            ExchangeOrders = new HashSet<ExchangeOrder>();
            ExchangeRequests = new HashSet<ExchangeRequest>();
            Reviews = new HashSet<Review>();
        }

        public int PostId { get; set; }
        public string AccountId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool PostStatus { get; set; }
        public int CategoryId { get; set; }
        public string PostType { get; set; }
        public int PointCost { get; set; }
        public DateTime PostDate { get; set; }
        public int PostPriority { get; set; }
        public DateTime PostExpiryDate { get; set; }
        public double Price { get; set; }

        public virtual Account Account { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ExchangeOrder> ExchangeOrders { get; set; }
        public virtual ICollection<ExchangeRequest> ExchangeRequests { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
    
}
