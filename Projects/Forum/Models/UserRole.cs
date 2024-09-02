using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        public string UserId { get; set; }
        public UserModel User { get; set; }
        
    }
}