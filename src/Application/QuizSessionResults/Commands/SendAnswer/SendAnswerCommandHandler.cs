namespace QuizHub.Application.QuizSessionResults.Commands.SendAnswer;

using MediatR;

using AutoMapper;

using QuizHub.Domain.Entities;
using QuizHub.Domain.Enums;
using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Exceptions;
using QuizHub.Application.Common.Specifications;
using QuizHub.Application.QuizSessionResults.DTO;

//TODO: Декомпозировать
public class SendAnswerCommandHandler : IRequestHandler<SendAnswerCommand, QuizSessionResultDTO>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUser user;
    private readonly IPermission<QuizSession> permission;
    private readonly IMapper mapper;

    public SendAnswerCommandHandler(
        IUnitOfWork unitOfWork,
        IUser user,
        IPermission<QuizSession> permission,
        IMapper mapper
    )
    {
        this.unitOfWork = unitOfWork;
        this.user = user;
        this.permission = permission;
        this.mapper = mapper;
    }

    public async Task<QuizSessionResultDTO> Handle(
        SendAnswerCommand request,
        CancellationToken cancellationToken
    )
    {
        await checkConflict(request.SessionId, request.QuestionId);

        QuizSessionResult result = await create(request);

        return mapper.Map<QuizSessionResultDTO>(result);
    }

    private async Task checkConflict(long sessionId, long questionId)
    {
        if (await unitOfWork.QuizSessionResults.isExists(sessionId, questionId))
        {
            throw new EntityArleadyExistsException();
        }
    }

    private async Task<QuizSessionResult> create(SendAnswerCommand request)
    {
        QuizSession quizSession = await getSession(request.QuizId, request.SessionId);
        Question question = await getQuestion(request.QuizId, request.QuestionId);

        IList<Answer> answers = await getAnswers(question, request.AnswerIds);
        bool isCorrect = isAnswerCorrect(question, answers);

        var result = new QuizSessionResult()
        {
            IsCorrect = isCorrect,
            Answers = answers,
            Question = question,
            QuizSession = quizSession
        };

        await unitOfWork.QuizSessionResults.Create(result);
        await unitOfWork.SaveChanges();

        return result;
    }

    private async Task<QuizSession> getSession(Guid quizId, long id)
    {
        QuizSession? session = await unitOfWork.QuizSessions.Find(quizId, id);

        if (session is null)
        {
            throw new EntityNotFoundException();
        }
        if (new IsOpenQuizSessionSpecification().IsSpecified(session))
        {
            throw new QuizSessionInvalidException();
        }
        if (!permission.IsCanEdit(user, session))
        {
            throw new ForbiddenException();
        }

        return session;
    }

    private async Task<Question> getQuestion(Guid quizId, long id)
    {
        Question? question = await unitOfWork.Questions.Find(quizId, id);

        if (question is null)
        {
            throw new EntityNotFoundException();
        }

        return question;
    }

    private async Task<IList<Answer>> getAnswers(Question question, ICollection<long> answerIds)
    {
        if (question.Type == QuestionType.Select)
        {
            answerIds = answerIds.Take(1).ToList();
        }
        else if (question.Type == QuestionType.MultipleSelect)
        {
            answerIds = answerIds.Distinct().ToList();
        }
        return await unitOfWork.Answers.Find(question.Id, answerIds);
    }

    private bool isAnswerCorrect(Question question, IList<Answer> answers)
    {
        return question.Type switch
        {
            QuestionType.Select => question.IsAnswerCorrect(answers.First()),
            QuestionType.MultipleSelect => question.IsAnswerCorrect(answers),
            _ => throw new NotSupportedException()
        };
    }
}
