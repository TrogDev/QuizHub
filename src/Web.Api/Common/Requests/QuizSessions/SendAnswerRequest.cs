namespace QuizHub.Web.Api.Common.Requests.QuizSessions;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using QuizHub.Application.QuizSessionResults.Commands.SendAnswer;

public class SendAnswerRequest
{
    [FromRoute]
    public required Guid QuizId { get; set; }
    [FromRoute(Name = "id")]
    public required long SessionId { get; set; }
    [FromForm]
    public required long QuestionId { get; set; }
    [FromForm]
    public required ICollection<long> AnswerIds { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<SendAnswerRequest, SendAnswerCommand>();
        }
    }
}