namespace QuizHub.Application.Quizzes.Commands.CreateQuiz;

using MediatR;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Abstractions.Data;

public record CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, Guid>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUser user;
    private readonly IPermission<Quiz> permission;

    public CreateQuizCommandHandler(IUnitOfWork unitOfWork, IUser user, IPermission<Quiz> permission)
    {
        this.unitOfWork = unitOfWork;
        this.user = user;
        this.permission = permission;
    }

    public async Task<Guid> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        Quiz quiz = await create(request);
        return quiz.Id;
    }

    private async Task<Quiz> create(CreateQuizCommand request)
    {
        var quiz = new Quiz()
        {
            Title = request.Title,
            Description = request.Description,
            Image = request.Image,
            MinuteLimit = request.MinuteLimit,
            AuthorId = user.Id
        };
        await unitOfWork.Quizzes.Create(quiz);
        await unitOfWork.SaveChanges();
        return quiz;
    }
}
