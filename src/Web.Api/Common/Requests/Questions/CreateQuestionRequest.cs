namespace QuizHub.Web.Api.Common.Requests.Questions;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using QuizHub.Domain.Enums;
using QuizHub.Application.Questions.Commands.CreateQuestion;

public record CreateQuestionRequest
{
    [FromRoute]
    public required Guid quizId { get; set; }

    [FromBody]
    public required CreateQuestionBodyData BodyData { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateQuestionBodyData, CreateQuestionCommand>();
            CreateMap<CreateQuestionRequest, CreateQuestionCommand>()
                .IncludeMembers(e => e.BodyData);
        }
    }
}

public class CreateQuestionBodyData
{
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required QuestionType Type { get; set; }
    public required ICollection<CreateAnswerData> Answers { get; set; }
}
