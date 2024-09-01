using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class ReplyModel
    {
        [Key]
        public int ReplyId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserModel User { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public PostModel Post { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}