using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class PostModel
    {
        [Key]
        public int PostId { get; set; }
        [ForeignKey("Thread")]
        public int ThreadId { get; set; }
        public ThreadModel Thread { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpvoteCount { get; set; }
        public ICollection<ReplyModel> Replies { get; set; }
        public ICollection<UpvoteModel> Upvotes { get; set; }
    }
}