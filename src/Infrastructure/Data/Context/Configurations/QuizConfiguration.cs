namespace QuizHub.Infrastructure.Data.Context.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizHub.Domain.Entities;
using QuizHub.Infrastructure.Identity;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.Property(e => e.Title).HasMaxLength(200).IsRequired();
        builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(e => e.AuthorId);
        builder.HasMany(e => e.Questions).WithOne(e => e.Quiz).OnDelete(DeleteBehavior.Cascade);
    }
}
