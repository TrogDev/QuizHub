namespace QuizHub.Application.Quizzes.Queries.GetUserQuizzes;

using FluentValidation;

public class GetUserQuizzesQueryValidator : AbstractValidator<GetUserQuizzesQuery>
{
    public const int MaxPerPage = 30;

    public GetUserQuizzesQueryValidator()
    {
        RuleFor(e => e.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be a positive number.");
        RuleFor(e => e.PerPage)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PerPage must be a positive number.")
            .LessThanOrEqualTo(MaxPerPage)
            .WithMessage($"PerPage allowed no more than {MaxPerPage}.");
    }
}
