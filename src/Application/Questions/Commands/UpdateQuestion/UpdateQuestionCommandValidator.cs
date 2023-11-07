namespace QuizHub.Application.Questions.Commands.CreateQuestion;

using FluentValidation;

public class UpdateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public UpdateQuestionCommandValidator()
    {
        RuleFor(e => e.Title).MaximumLength(200).NotEmpty();
        RuleFor(e => e.Answers)
            .NotEmpty()
            .Must(isСontainsCorrectAnswer)
            .WithMessage("Answers not contains the correct option");
    }

    private bool isСontainsCorrectAnswer(ICollection<CreateAnswerData> answers)
    {
        return answers.Any(e => e.IsCorrect);
    }
}
