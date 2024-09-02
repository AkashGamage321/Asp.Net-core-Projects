using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models.ViewModels
{
    public class PostViewModel
    {

        public int PostId { get; set; }
        public int ThreadId { get; set; }
        public string Title { get; set; }
        public int categoryId { get; set; }
        
        public string Content { get; set; }
        public string Author { get; set; }
    }
}