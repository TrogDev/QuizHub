namespace QuizHub.Application.Quizzes.Queries.GetQuizById;

using AutoMapper;

using MediatR;

using QuizHub.Application.Common.Abstractions;
using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Exceptions;
using QuizHub.Application.Common.Models;
using QuizHub.Application.Quizzes.DTO;
using QuizHub.Domain.Entities;

public record GetQuizByIdQueryHandler
    : IRequestHandler<GetQuizByIdQuery, QuizDTO>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetQuizByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<QuizDTO> Handle(
        GetQuizByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        Quiz quiz = await get(request.Id);
        return mapper.Map<QuizDTO>(quiz);
    }

    private async Task<Quiz> get(Guid id)
    {
        Quiz? quiz = await unitOfWork.Quizzes.Get(id);

        if (quiz is null)
        {
            throw new EntityNotFoundException();
        }

        return quiz;
    }
}
