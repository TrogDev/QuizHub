namespace QuizHub.Application.Quizzes.Commands.DeleteQuiz;

using MediatR;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Exceptions;

public record DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUser user;
    private readonly IPermission<Quiz> permission;

    public DeleteQuizCommandHandler(IUnitOfWork unitOfWork, IUser user, IPermission<Quiz> permission)
    {
        this.unitOfWork = unitOfWork;
        this.user = user;
        this.permission = permission;
    }

    public async Task Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
    {
        Quiz quiz = await get(request.Id);
        await delete(quiz);
    }

    private async Task<Quiz> get(Guid id)
    {
        Quiz? quiz = await unitOfWork.Quizzes.Get(id);

        if (quiz is null)
        {
            throw new EntityNotFoundException();
        }
        if (!permission.IsCanDelete(user, quiz))
        {
            throw new ForbiddenException();
        }

        return quiz;
    }

    private async Task delete(Quiz quiz)
    {
        await unitOfWork.Quizzes.Delete(quiz);
        await unitOfWork.SaveChanges();
    }
}
