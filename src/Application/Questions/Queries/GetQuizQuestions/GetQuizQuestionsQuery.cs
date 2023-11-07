namespace QuizHub.Application.Questions.Queries.GetQuizQuestions;

using MediatR;

using QuizHub.Application.Questions.DTO;

public record GetQuizQuestionsQuery : IRequest<IList<QuestionDTO>>
{
    public Guid QuizId { get; set; }
}
