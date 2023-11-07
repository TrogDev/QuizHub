namespace QuizHub.Application.Tests.Quizzes.Commands;

using FluentValidation.Results;

using QuizHub.Application.Quizzes.Queries.GetUserQuizzes;

public class GetUserQuizzesQueryTests
{
    [Fact]
    public void Validate_InvalidCommand_ReturnsErrors()
    {
        var validator = new GetUserQuizzesQueryValidator();

        int invalidPage = -1; //must be positive
        int invalidPerPage = GetUserQuizzesQueryValidator.MaxPerPage + 1;

        var query = new GetUserQuizzesQuery()
        {
            Page = invalidPage,
            PerPage = invalidPerPage
        };

        ValidationResult result = validator.Validate(query);
        IEnumerable<string> invalidProperties = result.Errors.Select(e => e.PropertyName);

        Assert.Contains(nameof(query.Page), invalidProperties);
        Assert.Contains(nameof(query.PerPage), invalidProperties);
    }
}
