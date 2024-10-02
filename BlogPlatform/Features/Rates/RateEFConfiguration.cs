using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Features.Rates
{
    public class RateEfConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.ToTable("Rates");

            builder.HasKey(r => r.RateId);

            builder.Property(r => r.RateId)
                   .ValueGeneratedOnAdd();

            builder.Property(r => r.RateNumber)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(u => u.CreatedAt)
                   .IsRequired();

            builder.HasOne(r => r.Post)
                   .WithMany(p => p.Rates)
                   .HasForeignKey(r => r.PostId);

            builder.HasOne(r => r.User)
                   .WithMany(u => u.Rates)
                   .HasForeignKey(r => r.UserId);
        }
    }
}
