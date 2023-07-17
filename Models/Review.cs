using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int ReviewerId { get; set; }
        public int ReviewedId { get; set; }
        public int? RatingStar { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Account Reviewed { get; set; } = null!;
        public virtual Account Reviewer { get; set; } = null!;
    }
}
