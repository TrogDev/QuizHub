namespace QuizHub.Application.Quizzes.DTO;

using AutoMapper;

using QuizHub.Domain.Entities;

public record QuizDTO
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public string? Image { get; init; }
    public int? MinuteLimit { get; set; }

    public required long AuthorId { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Quiz, QuizDTO>();
        }
    }
}
