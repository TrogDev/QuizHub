namespace QuizHub.Application.Questions.DTO;

using AutoMapper;

using QuizHub.Domain.Entities;

public record AnswerDTO
{
    public required long Id { get; set; }
    public required string Text { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Answer, AnswerDTO>();
        }
    }
}
