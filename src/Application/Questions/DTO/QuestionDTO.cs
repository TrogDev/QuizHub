namespace QuizHub.Application.Questions.DTO;

using AutoMapper;

using QuizHub.Domain.Entities;
using QuizHub.Domain.Enums;

public record QuestionDTO
{
    public required long Id { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required QuestionType Type { get; set; }
    public required Guid QuizId { get; set; }
    public required ICollection<AnswerDTO> Answers { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Question, QuestionDTO>();
        }
    }
}
