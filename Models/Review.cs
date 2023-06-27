using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int PostId { get; set; }
<<<<<<< HEAD
        public string FeedbackUserId { get; set; }
        public string FeedbackUsername { get; set; }
        public string Content { get; set; }
        public int StarRating { get; set; }
=======
        public string Content { get; set; } = null!;
        public int StarRating { get; set; }
        public string FeedbackUserId { get; set; } = null!;
        public string FeedbackUsername { get; set; } = null!;
>>>>>>> 0328c1e978e95085f75972c075248f9a53d59742

        public virtual Account FeedbackUser { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
