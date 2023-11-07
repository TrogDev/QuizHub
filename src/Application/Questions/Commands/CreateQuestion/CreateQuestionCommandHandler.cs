namespace QuizHub.Application.Questions.Commands.CreateQuestion;

using MediatR;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Exceptions;

public record CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, long>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUser user;
    private readonly IPermission<Quiz> permission;

    public CreateQuestionCommandHandler(
        IUnitOfWork unitOfWork,
        IUser user,
        IPermission<Quiz> permission
    )
    {
        this.unitOfWork = unitOfWork;
        this.user = user;
        this.permission = permission;
    }

    public async Task<long> Handle(
        CreateQuestionCommand request,
        CancellationToken cancellationToken
    )
    {
        await validateParrentQuiz(request.QuizId);
        Question question = await create(request);
        return question.Id;
    }

    private async Task validateParrentQuiz(Guid id)
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
    }

    private async Task<Question> create(CreateQuestionCommand request)
    {
        Question question = await createQuestion(request);
        question.Answers = createAnswers(question, request.Answers);
        await unitOfWork.SaveChanges();
        return question;
    }

    private async Task<Question> createQuestion(CreateQuestionCommand request)
    {
        var question = new Question()
        {
            Title = request.Title,
            Content = request.Content,
            Type = request.Type,
            QuizId = request.QuizId
        };
        await unitOfWork.Questions.Create(question);
        return question;
    }

    private ICollection<Answer> createAnswers(Question question, ICollection<CreateAnswerData> data)
    {
        return data.Select(e => new Answer()
        {
            Text = e.Text,
            IsCorrect = e.IsCorrect,
            Question = question
        }).ToList();
    }
}
