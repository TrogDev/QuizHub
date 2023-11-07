namespace QuizHub.Application.Questions.Queries.GetQuizQuestions;

using AutoMapper;

using MediatR;

using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Exceptions;
using QuizHub.Application.Questions.DTO;
using QuizHub.Domain.Entities;

public record GetQuizQuestionsQueryHander
    : IRequestHandler<GetQuizQuestionsQuery, IList<QuestionDTO>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetQuizQuestionsQueryHander(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<IList<QuestionDTO>> Handle(
        GetQuizQuestionsQuery request,
        CancellationToken cancellationToken
    )
    {
        await validateParrentQuiz(request.QuizId);
        IList<Question> questions = await unitOfWork.Questions.GetFromQuizWithAnswers(
            request.QuizId
        );
        return mapper.Map<IList<QuestionDTO>>(questions);
    }

    public async Task validateParrentQuiz(Guid quizId)
    {
        _ = await unitOfWork.Quizzes.Get(quizId) ?? throw new EntityNotFoundException();
    }
}
