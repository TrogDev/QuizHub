namespace QuizHub.Application.Quizzes.Commands.CreateQuiz;

using FluentValidation;

public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
{
    public CreateQuizCommandValidator()
    {
        RuleFor(e => e.Title).MaximumLength(200).NotEmpty();
        RuleFor(e => e.Image).MaximumLength(255);
    }
}
