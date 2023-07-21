using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Post
    {
        public Post()
        {
            ExchangeOrders = new HashSet<ExchangeOrder>();
            Images = new HashSet<Image>();
        }

        public int PostId { get; set; }
        public int AccountId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public int PostStatusId { get; set; }
        public bool IsDonated { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual Status PostStatus { get; set; } = null!;
        public virtual ICollection<ExchangeOrder> ExchangeOrders { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
