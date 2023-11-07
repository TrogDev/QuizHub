namespace QuizHub.Domain.Entities;

using QuizHub.Domain.Common;

public class QuizSessionResult: BaseEntity<long>
{
    public long QuizSessionId { get; set; }
    public QuizSession QuizSession { get; set; }

    public long QuestionId { get; set; }
    public Question Question { get; set; }

    public ICollection<Answer> Answers { get; set; }

    public bool IsCorrect { get; set; }
}
