using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BlogPlatform.Features.Posts.Common;

namespace BlogPlatform.Features.Posts
{
    public class PostVersionEfConfiguration : IEntityTypeConfiguration<PostVersion>
    {
        public void Configure(EntityTypeBuilder<PostVersion> builder)
        {
            builder.ToTable("PostVersions");

            builder.HasKey(pv => pv.Id);

            builder.Property(pv => pv.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(pv => pv.Content)
                   .IsRequired();

            builder.Property(pv => pv.Title)
                   .IsRequired();

            builder.Property(pv => pv.CreatedAt)
                   .IsRequired();

            builder.HasOne(pv => pv.Post)
                   .WithMany(p => p.PostVersions)
                   .HasForeignKey(pv => pv.PostId);
        }
    }
}
