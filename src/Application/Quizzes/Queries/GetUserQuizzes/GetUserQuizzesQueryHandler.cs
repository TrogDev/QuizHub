namespace QuizHub.Application.Quizzes.Queries.GetUserQuizzes;

using AutoMapper;

using MediatR;

using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Models;
using QuizHub.Application.Quizzes.DTO;
using QuizHub.Domain.Entities;

public record GetUserQuizzesQueryHandler
    : IRequestHandler<GetUserQuizzesQuery, PaginatedList<QuizDTO>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUser user;
    private readonly IMapper mapper;

    public GetUserQuizzesQueryHandler(IUnitOfWork unitOfWork, IUser user, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.user = user;
        this.mapper = mapper;
    }

    public async Task<PaginatedList<QuizDTO>> Handle(
        GetUserQuizzesQuery request,
        CancellationToken cancellationToken
    )
    {
        PaginatedList<Quiz> quizzes = await unitOfWork.Quizzes.GetFromUser(user.Id, request.PerPage, request.Page);
        return new PaginatedList<QuizDTO>()
        {
            Count = quizzes.Count,
            Items = mapper.Map<ICollection<Quiz>, ICollection<QuizDTO>>(quizzes.Items)
        };
    }
}
