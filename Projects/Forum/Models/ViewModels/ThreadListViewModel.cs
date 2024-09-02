using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models.ViewModels
{
    public class ThreadListViewModel
    {
        public int ThreadId{ get; set; }
        public string Title { get; set; }
        public string Author { get; set;}
        public int NumberOfReplies { get; set; }
        public DateTime LastReplayDate { get;set; }
    }
}