﻿using BlogPlatform.Features.Comments;
using BlogPlatform.Features.Posts.Common;
using BlogPlatform.Features.Rates;
using BlogPlatform.Features.Users.Common;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Common
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PostVersion> PostVersions { get; set; }
        public DbSet<Rate> Rates{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=blogplatform.db");
        }
    }
}
