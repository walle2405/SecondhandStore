using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int PostId { get; set; }
        public int FeedbackUserId { get; set; }
        public string FeedbackUsername { get; set; }
        public string Content { get; set; }
        public int StarRating { get; set; }

        public virtual Account FeedbackUser { get; set; }
        public virtual Post Post { get; set; }
    }
}
