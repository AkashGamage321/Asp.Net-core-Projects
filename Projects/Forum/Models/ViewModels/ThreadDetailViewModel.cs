using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models.ViewModels
{
    public class ThreadDetailViewModel
    {
        public int ThreadId { get; set;}
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Upvotes { get; set;}
        public ICollection<ReplyViewModel>Replies { get; set; }
    }
}