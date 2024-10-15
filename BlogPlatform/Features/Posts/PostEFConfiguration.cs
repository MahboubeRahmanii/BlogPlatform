using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BlogPlatform.Features.Posts.Common;

namespace BlogPlatform.Features.Posts
{
    public class PostEfConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Content)
                   .IsRequired();

            builder.Property(p => p.CreatedAt)
                   .IsRequired();

            builder.HasMany(p => p.PostVersions)
                   .WithOne(pv => pv.Post)
                   .HasForeignKey(pv => pv.PostId);

        }
    }
}
