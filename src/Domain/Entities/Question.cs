namespace QuizHub.Domain.Entities;

using QuizHub.Domain.Common;
using QuizHub.Domain.Enums;
using QuizHub.Domain.Exceptions;

public class Question : BaseEntity<long>
{
    public string Title { get; set; }
    public string? Content { get; set; }
    public QuestionType Type { get; set; }

    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public bool IsAnswerCorrect(Answer answer)
    {
        if (Type != QuestionType.Select)
        {
            throw new InvalidAnswerTypeException();
        }
        return Answers.Where(e => e.IsCorrect).Any(e => e == answer);
    }

    public bool IsAnswerCorrect(IEnumerable<Answer> answers)
    {
        if (Type != QuestionType.MultipleSelect)
        {
            throw new InvalidAnswerTypeException();
        }
        return isAnswersEquals(Answers.Where(e => e.IsCorrect), answers);
    }

    private bool isAnswersEquals(IEnumerable<Answer> answers1, IEnumerable<Answer> answers2)
    {
        var answersSet1 = new HashSet<Answer>(answers1);
        var answersSet2 = new HashSet<Answer>(answers2);
        return answersSet1.SetEquals(answersSet2);
    }
}
