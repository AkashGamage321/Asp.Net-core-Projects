using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Forum.Models
{
    public class ThreadModel
    {
        [Key]
        public int ThreadId { get; set; }
        [ForeignKey("User")]
        public int UserId { get;set; }
        public UserModel User { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId {get; set ;}
        public CategoryModel Category { get;set;}
        public string Title { get;set; }
        public string Content { get;set; }
        public DateTime CreateDate { get; set; }
        public int UpvoteCount { get; set; }

        public ICollection<PostModel> Posts { get; set; }

    }
}