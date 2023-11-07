namespace QuizHub.Application.Questions.Commands.DeleteQuestion;

using MediatR;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Exceptions;

public record DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUser user;
    private readonly IPermission<Question> permission;

    public DeleteQuestionCommandHandler(
        IUnitOfWork unitOfWork,
        IUser user,
        IPermission<Question> permission
    )
    {
        this.unitOfWork = unitOfWork;
        this.user = user;
        this.permission = permission;
    }

    public async Task Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        Question question = await get(request.QuizId, request.Id);
        await delete(question);
    }

    private async Task<Question> get(Guid quizId, long id)
    {
        Question? question = await unitOfWork.Questions.Find(quizId, id);

        if (question is null)
        {
            throw new EntityNotFoundException();
        }
        if (!permission.IsCanDelete(user, question))
        {
            throw new ForbiddenException();
        }

        return question;
    }

    private async Task delete(Question question)
    {
        await unitOfWork.Questions.Delete(question);
        await unitOfWork.SaveChanges();
    }
}
