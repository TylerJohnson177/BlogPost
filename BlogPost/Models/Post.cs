using System.Collections.Generic;
using System.Dynamic;

namespace BlogPost.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public virtual Blog blog { get; set; }
    }
}