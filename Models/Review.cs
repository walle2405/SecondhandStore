﻿using System;
using System.Collections.Generic;

namespace SecondhandStore.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int PostId { get; set; }
        public int FeedbackUserId { get; set; }
        public string FeedbackUsername { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int StarRating { get; set; }

        public virtual Account FeedbackUser { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
