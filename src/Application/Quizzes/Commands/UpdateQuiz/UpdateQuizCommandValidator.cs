namespace QuizHub.Application.Quizzes.Commands.UpdateQuizz;

using FluentValidation;

public class UpdateQuizCommandValidator : AbstractValidator<UpdateQuizCommand>
{
    public UpdateQuizCommandValidator()
    {
        RuleFor(e => e.Title).MaximumLength(200).NotEmpty();
        RuleFor(e => e.Image).MaximumLength(255);
    }
}
