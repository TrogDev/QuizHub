namespace QuizHub.Web.Api.Common.Requests.Questions;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using QuizHub.Domain.Enums;
using QuizHub.Application.Questions.Commands.UpdateQuestion;

public record UpdateQuestionRequest
{
    [FromRoute]
    public required Guid quizId { get; set; }
    [FromRoute]
    public required long Id { get; set; }

    [FromBody]
    public required UpdateQuestionBodyData BodyData { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateQuestionBodyData, UpdateQuestionCommand>();
            CreateMap<UpdateQuestionRequest, UpdateQuestionCommand>()
                .IncludeMembers(e => e.BodyData);
        }
    }
}

public class UpdateQuestionBodyData
{
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required QuestionType Type { get; set; }
    public required ICollection<UpdateAnswerData> Answers { get; set; }
}
