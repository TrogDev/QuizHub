namespace QuizHub.Web.Api.Common.Requests.Quizzes;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using QuizHub.Application.Quizzes.Commands.UpdateQuizz;

public record UpdateQuizRequest
{
    [FromRoute]
    public required Guid Id { get; set; }
    [FromForm]
    public required string Title { get; set; }
    [FromForm]
    public string? Description { get; set; }
    [FromForm]
    public string? Image { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateQuizRequest, UpdateQuizCommand>();
        }
    }
}
