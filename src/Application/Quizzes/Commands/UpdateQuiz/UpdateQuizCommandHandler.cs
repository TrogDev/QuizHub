namespace QuizHub.Application.Quizzes.Commands.UpdateQuizz;

using MediatR;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Exceptions;

public record UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUser user;
    private readonly IPermission<Quiz> permission;

    public UpdateQuizCommandHandler(IUnitOfWork unitOfWork, IUser user, IPermission<Quiz> permission)
    {
        this.unitOfWork = unitOfWork;
        this.user = user;
        this.permission = permission;
    }

    public async Task Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
    {
        Quiz quiz = await get(request.Id);
        await edit(quiz, request);
    }

    private async Task<Quiz> get(Guid id)
    {
        Quiz? quiz = await unitOfWork.Quizzes.Get(id);

        if (quiz is null)
        {
            throw new EntityNotFoundException();
        }
        if (!permission.IsCanEdit(user, quiz))
        {
            throw new ForbiddenException();
        }

        return quiz;
    }

    private async Task edit(Quiz quiz, UpdateQuizCommand request)
    {
        quiz.Title = request.Title;
        quiz.Description = request.Description;
        quiz.Image = request.Image;
        quiz.MinuteLimit = request.MinuteLimit;

        await unitOfWork.Quizzes.Update(quiz);
        await unitOfWork.SaveChanges();
    }
}
