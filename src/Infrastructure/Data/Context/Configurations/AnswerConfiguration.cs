namespace QuizHub.Infrastructure.Data.Context.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizHub.Domain.Entities;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.Property(e => e.Text)
            .HasMaxLength(200)
            .IsRequired();
    }
}
