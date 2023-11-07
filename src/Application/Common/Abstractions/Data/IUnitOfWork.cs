namespace QuizHub.Application.Common.Abstractions.Data;

using QuizHub.Application.Common.Abstractions.Data.DAO;

public interface IUnitOfWork
{
    IQuizDAO Quizzes { get; }
    IQuestionDAO Questions { get; }
    IAnswerDAO Answers { get; }
    IQuizSessionDAO QuizSessions { get; }
    IQuizSessionResultDAO QuizSessionResults { get; }

    Task SaveChanges();
}
