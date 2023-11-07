namespace QuizHub.Infrastructure.Data.Context.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizHub.Domain.Entities;
using QuizHub.Infrastructure.Identity;

public class QuizSessionConfiguration : IEntityTypeConfiguration<QuizSession>
{
    public void Configure(EntityTypeBuilder<QuizSession> builder)
    {
        builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(e => e.UserId);
    }
}
