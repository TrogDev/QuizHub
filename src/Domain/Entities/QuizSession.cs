namespace QuizHub.Domain.Entities;

using QuizHub.Domain.Common;

public class QuizSession : BaseEntity<long>
{
    public long UserId { get; set; }

    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<QuizSessionResult> Results { get; set; }

    public bool IsClosed { get; set; } = false;
}
