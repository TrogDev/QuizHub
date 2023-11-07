namespace QuizHub.Application.Common.Abstractions.Data.DAO;

using QuizHub.Domain.Entities;

public interface IQuestionDAO : IBaseDAO<Question, long>
{
    Task<Question?> GetWithQuiz(long id);
    public Task<Question?> Find(Guid quizId, long id);
    Task<IList<Question>> GetFromQuizWithAnswers(Guid quizId);
}
