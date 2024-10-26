﻿using BlogPlatform.Features.Comments.Common;
using BlogPlatform.Features.Posts.Common;
using BlogPlatform.Features.Rates;

namespace BlogPlatform.Features.Users.Common
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Post> Posts { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<Rate> Rates { get; set; } = new();
    }
}
