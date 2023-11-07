namespace QuizHub.Application.QuizSessionResults.DTO;

using AutoMapper;

using QuizHub.Domain.Entities;
using QuizHub.Application.Questions.DTO;

public record QuizSessionResultDTO
{
    public required long QuizSessionId { get; set; }
    public required long QuestionId { get; set; }
    public required ICollection<AnswerDTO> Answers { get; set; }
    public required bool IsCorrect { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<QuizSessionResult, QuizSessionResultDTO>();
        }
    }
}
