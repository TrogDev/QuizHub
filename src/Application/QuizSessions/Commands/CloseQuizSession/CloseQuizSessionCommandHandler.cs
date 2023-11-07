namespace QuizHub.Application.QuizSessions.Commands.CloseQuizSession;

using MediatR;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Exceptions;

public class CloseQuizSessionCommandHandler : IRequestHandler<CloseQuizSessionCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUser user;
    private readonly IPermission<QuizSession> permission;

    public CloseQuizSessionCommandHandler(IUnitOfWork unitOfWork, IUser user, IPermission<QuizSession> permission)
    {
        this.unitOfWork = unitOfWork;
        this.user = user;
        this.permission = permission;
    }

    public async Task Handle(
        CloseQuizSessionCommand request,
        CancellationToken cancellationToken
    )
    {
        QuizSession entity = await get(request);
        checkPermission(entity);
        await close(entity);
    }

    private async Task<QuizSession> get(CloseQuizSessionCommand request)
    {
        QuizSession? entity = await unitOfWork.QuizSessions.Find(request.QuizId, request.Id);

        if (entity is null)
        {
            throw new EntityNotFoundException();
        }

        return entity;
    }

    private void checkPermission(QuizSession entity)
    {
        if (!permission.IsCanEdit(user, entity))
        {
            throw new ForbiddenException();
        }
    }

    private async Task close(QuizSession entity)
    {
        entity.IsClosed = true;
        await unitOfWork.QuizSessions.Update(entity);
        await unitOfWork.SaveChanges();
    }
}
