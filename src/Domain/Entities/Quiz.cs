namespace QuizHub.Domain.Entities;

using QuizHub.Domain.Common;

public class Quiz: BaseEntity<Guid>
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int? MinuteLimit { get; set; }

    public long AuthorId { get; set; }

    public ICollection<Question> Questions { get; set; } = new List<Question>();
}
