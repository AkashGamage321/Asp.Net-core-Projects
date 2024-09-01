using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Forum.Models
{
    public class UserModel : IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set ;}
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }

        public DateTime CreatedDate { get; set ;}
        public ICollection<ThreadModel> Threads { get; set; }
        public ICollection<PostModel> Posts { get; set; }
        public ICollection<ReplyModel> Replies { get; set; }
        public ICollection<UpvoteModel> Upvotes { get; set; }
        
    }
}