namespace QuizHub.Infrastructure.Data.Context.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizHub.Domain.Entities;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.Property(e => e.Title).HasMaxLength(200).IsRequired();
        builder.HasMany(e => e.Answers).WithOne(e => e.Question).OnDelete(DeleteBehavior.Cascade);
    }
}
