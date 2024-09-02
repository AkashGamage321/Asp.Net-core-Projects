using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models.ViewModels
{
    public class ThreadViewModel
    {
        [Required]
        public int ThreadId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The title must be at least {2} and at most {1} characters long.", MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int NumberOfReplies { get; set; }

        public int Upvotes { get; set; }

        public ICollection<ReplyViewModel> Replies { get; set; }
    }
}