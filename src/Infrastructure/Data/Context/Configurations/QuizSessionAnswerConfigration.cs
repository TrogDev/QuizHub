namespace QuizHub.Infrastructure.Data.Context.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizHub.Domain.Entities;

public class QuizSessionResultConfigration : IEntityTypeConfiguration<QuizSessionResult>
{
    public void Configure(EntityTypeBuilder<QuizSessionResult> builder)
    {
        builder.HasMany(e => e.Answers).WithMany();
    }
}
