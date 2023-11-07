namespace QuizHub.Application.QuizSessions.DTO;

using AutoMapper;

using QuizHub.Domain.Entities;
using QuizHub.Application.QuizSessionResults.DTO;

public record QuizSessionDTO
{
    public required long Id { get; set; }
    public required long UserId { get; set; }
    public required Guid QuizId { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required ICollection<QuizSessionResultDTO> Results { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<QuizSession, QuizSessionDTO>();
        }
    }
}
