using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SecondhandStore.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public int PostId { get; set; }
        public string ImageUrl { get; set; } = null!;
        [JsonIgnore]
        public virtual Post Post { get; set; } = null!;
    }
}
