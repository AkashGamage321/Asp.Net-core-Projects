using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options )
        :base(options)
        {

        }
        public DbSet<UserModel> Users {get; set; }
        public DbSet<ThreadModel> Threads { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<ReplyModel> Replies { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<UpvoteModel> Upvotes { get; set; }
        public DbSet<AdminModel> Admins { get; set ;}
        public IEnumerable<object> UserRoles { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}