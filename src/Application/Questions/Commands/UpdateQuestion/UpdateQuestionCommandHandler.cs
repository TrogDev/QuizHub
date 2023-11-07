namespace QuizHub.Application.Questions.Commands.UpdateQuestion;

using MediatR;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Exceptions;

public record UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUser user;
    private readonly IPermission<Question> permission;

    public UpdateQuestionCommandHandler(
        IUnitOfWork unitOfWork,
        IUser user,
        IPermission<Question> permission
    )
    {
        this.unitOfWork = unitOfWork;
        this.user = user;
        this.permission = permission;
    }

    public async Task Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        Question question = await get(request.QuizId, request.Id);
        await update(question, request);
    }

    private async Task<Question> get(Guid quizId, long id)
    {
        Question? question = await unitOfWork.Questions.Find(quizId, id);

        if (question is null)
        {
            throw new EntityNotFoundException();
        }
        if (!permission.IsCanEdit(user, question))
        {
            throw new ForbiddenException();
        }

        return question;
    }

    private async Task update(Question question, UpdateQuestionCommand request)
    {
        question.Title = request.Title;
        question.Content = request.Content;
        question.Answers = request.Answers
            .Select(e => new Answer() { Text = e.Text, IsCorrect = e.IsCorrect })
            .ToList();

        await unitOfWork.Questions.Update(question);
        await unitOfWork.SaveChanges();
    }
}
