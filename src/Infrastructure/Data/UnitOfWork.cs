namespace QuizHub.Infrastructure.Data;

using QuizHub.Application.Common.Abstractions.Data;
using QuizHub.Application.Common.Abstractions.Data.DAO;
using QuizHub.Infrastructure.Data.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;

    public UnitOfWork(
        ApplicationDbContext context,
        IQuizDAO quizDao,
        IQuestionDAO questionDao,
        IAnswerDAO answerDAO,
        IQuizSessionDAO quizSessionDao,
        IQuizSessionResultDAO quizSessionResultDao
    )
    {
        this.context = context;
        Quizzes = quizDao;
        Questions = questionDao;
        Answers = answerDAO;
        QuizSessions = quizSessionDao;
        QuizSessionResults = quizSessionResultDao;
    }

    public IQuizDAO Quizzes { get; }
    public IQuestionDAO Questions { get; }
    public IQuizSessionDAO QuizSessions { get; }
    public IAnswerDAO Answers { get; }
    public IQuizSessionResultDAO QuizSessionResults { get; }

    public async Task SaveChanges()
    {
        await context.SaveChangesAsync();
    }
}
