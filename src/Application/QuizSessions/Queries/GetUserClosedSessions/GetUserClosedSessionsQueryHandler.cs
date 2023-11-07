namespace QuizHub.Application.QuizSessions.Queries.GetUserClosedSessions;

using MediatR;

using AutoMapper;

using QuizHub.Domain.Entities;
using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Exceptions;
using QuizHub.Application.QuizSessions.DTO;

public class GetUserClosedSessionsQueryHandler
    : IRequestHandler<GetUserClosedSessionsQuery, IList<QuizSessionDTO>>
{
    private readonly IUser user;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetUserClosedSessionsQueryHandler(IUser user, IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.user = user;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<IList<QuizSessionDTO>> Handle(
        GetUserClosedSessionsQuery request,
        CancellationToken cancellationToken
    )
    {
        await validateParrentQuiz(request.QuizId);
        IList<QuizSession> sessions = await get(request);
        return mapper.Map<IList<QuizSessionDTO>>(sessions);
    }

    public async Task validateParrentQuiz(Guid quizId)
    {
        Quiz? quiz = await unitOfWork.Quizzes.Get(quizId);

        if (quiz is null)
        {
            throw new EntityNotFoundException();
        }
    }

    public async Task<IList<QuizSession>> get(GetUserClosedSessionsQuery request)
    {
        return await unitOfWork.QuizSessions.FindAllUserClosed(user.Id, request.QuizId);
    }
}
