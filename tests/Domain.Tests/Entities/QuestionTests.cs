namespace QuizHub.Domain.Tests.Entities;

using QuizHub.Domain.Entities;
using QuizHub.Domain.Exceptions;

public class QuestionTests
{
    private readonly Question multipleSelectQuestion;
    private readonly List<Answer> multipleSelectIncorrectAnswers;
    private readonly List<Answer> multipleSelectCorrectAnswers;

    private readonly Question selectQuestion;
    private readonly List<Answer> selectIncorrectAnswers;
    private readonly Answer selectCorrectAnswer;

    public QuestionTests()
    {
        multipleSelectIncorrectAnswers = new List<Answer>()
        {
            new Answer() { Id = 1, IsCorrect = false },
            new Answer() { Id = 2, IsCorrect = false }
        };
        multipleSelectCorrectAnswers = new List<Answer>()
        {
            new Answer() { Id = 3, IsCorrect = true },
            new Answer() { Id = 4, IsCorrect = true }
        };
        multipleSelectQuestion = new Question
        {
            Id = 1,
            Answers = multipleSelectIncorrectAnswers.Concat(multipleSelectCorrectAnswers).ToList(),
            Type = Enums.QuestionType.MultipleSelect
        };

        selectIncorrectAnswers = new List<Answer>()
        {
            new Answer() { Id = 5, IsCorrect = false },
            new Answer() { Id = 6, IsCorrect = false }
        };
        selectCorrectAnswer = new Answer() { Id = 7, IsCorrect = true };
        selectQuestion = new Question
        {
            Id = 2,
            Answers = selectIncorrectAnswers.Append(selectCorrectAnswer).ToList(),
            Type = Enums.QuestionType.Select
        };
    }

    [Fact]
    public void Check_IncorrectAnswer_ReturnsFalse()
    {
        bool isAnswerCorrect = selectQuestion.IsAnswerCorrect(selectIncorrectAnswers.First());
        Assert.False(isAnswerCorrect);
    }

    [Fact]
    public void Check_CorrectAnswer_ReturnsTrue()
    {
        bool isAnswerCorrect = selectQuestion.IsAnswerCorrect(selectCorrectAnswer);
        Assert.True(isAnswerCorrect);
    }

    [Fact]
    public void Check_InvalidAnswerType_ThrowsException()
    {
        Assert.Throws<InvalidAnswerTypeException>(
            () => selectQuestion.IsAnswerCorrect(multipleSelectCorrectAnswers)
        );
    }

    [Fact]
    public void Check_MultipleIncorrectAnswers_ReturnsFalse()
    {
        bool isAnswerCorrect = multipleSelectQuestion.IsAnswerCorrect(
            multipleSelectIncorrectAnswers
        );
        Assert.False(isAnswerCorrect);
    }

    [Fact]
    public void Check_MiltipleCorrectAnswers_ReturnsTrue()
    {
        bool isAnswerCorrect = multipleSelectQuestion.IsAnswerCorrect(multipleSelectCorrectAnswers);
        Assert.True(isAnswerCorrect);
    }

    [Fact]
    public void Check_MiltiplePartialCorrectAnswers_ReturnsFalse()
    {
        bool isAnswerCorrect = multipleSelectQuestion.IsAnswerCorrect(
            multipleSelectCorrectAnswers.SkipLast(1)
        );
        Assert.False(isAnswerCorrect);
    }
}
