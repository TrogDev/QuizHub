namespace QuizHub.Domain.Entities;

using QuizHub.Domain.Common;

public class Answer : BaseEntity<long>
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }

    public long QuestionId { get; set; }
    public Question Question { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Answer entity)
        {
            return false;
        }

        return Id == entity.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
