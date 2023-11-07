namespace QuizHub.Domain.Exceptions;

public class InvalidAnswerTypeException : Exception
{
    public InvalidAnswerTypeException() : base("Answer type not supported for this question type") {  }
}
