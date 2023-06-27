using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; } = null!;
        public int StarRating { get; set; }
        public string FeedbackUserId { get; set; } = null!;
        public string FeedbackUsername { get; set; } = null!;

        public virtual Account FeedbackUser { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
