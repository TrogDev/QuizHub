namespace QuizHub.Application.QuizSessionResults.Commands.SendAnswer;

using FluentValidation;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions.Data;

public class SendAnswerCommandValidator : AbstractValidator<SendAnswerCommand>
{
    private readonly IUnitOfWork unitOfWork;

    public SendAnswerCommandValidator(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;

        RuleFor(e => e.AnswerIds).NotEmpty().MustAsync(isAnswersExist).WithMessage("Not all answers exist in this question");
    }

    private async Task<bool> isAnswersExist(SendAnswerCommand command, ICollection<long> answerIds, CancellationToken cancellationToken)
    {
        IList<Answer> validAnswers = await unitOfWork.Answers.Find(command.QuestionId, answerIds);
        return validAnswers.Count == answerIds.Count;
    }
}
