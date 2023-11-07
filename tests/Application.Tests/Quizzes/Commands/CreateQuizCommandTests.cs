namespace QuizHub.Application.Tests.Quizzes.Commands;

using FluentValidation.Results;

using QuizHub.Application.Quizzes.Commands.CreateQuiz;

public class CreateQuizCommandTests
{
    [Fact]
    public void Validate_InvalidCommand_ReturnsErrors()
    {
        var validator = new CreateQuizCommandValidator();

        var invalidTitle = new string('_', 201); //max length 200
        var invalidImage = new string('_', 256); // max length 255

        var command = new CreateQuizCommand()
        {
            Title = invalidTitle,
            Image = invalidImage
        };

        ValidationResult result = validator.Validate(command);
        IEnumerable<string> invalidProperties = result.Errors.Select(e => e.PropertyName);

        Assert.Contains(nameof(command.Title), invalidProperties);
        Assert.Contains(nameof(command.Image), invalidProperties);
    }
}
