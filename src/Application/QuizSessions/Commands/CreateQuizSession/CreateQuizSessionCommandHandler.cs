namespace QuizHub.Application.QuizSessions.Commands.CreateQuizSession;

using MediatR;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Exceptions;

public class CreateQuizSessionCommandHandler : IRequestHandler<CreateQuizSessionCommand, long>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUser user;

    public CreateQuizSessionCommandHandler(IUnitOfWork unitOfWork, IUser user)
    {
        this.unitOfWork = unitOfWork;
        this.user = user;
    }

    public async Task<long> Handle(
        CreateQuizSessionCommand request,
        CancellationToken cancellationToken
    )
    {
        await validateParrentQuiz(request.QuizId);
        QuizSession entity = await create(request);
        return entity.Id;
    }

    private async Task validateParrentQuiz(Guid id)
    {
        Quiz? quiz = await unitOfWork.Quizzes.Get(id);

        if (quiz is null)
        {
            throw new EntityNotFoundException();
        }
    }

    private async Task<QuizSession> create(CreateQuizSessionCommand request)
    {
        QuizSession entity = new QuizSession() { QuizId = request.QuizId, UserId = user.Id };
        await unitOfWork.QuizSessions.Create(entity);
        await unitOfWork.SaveChanges();
        return entity;
    }
}
