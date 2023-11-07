namespace QuizHub.Web.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MediatR;
using AutoMapper;

using QuizHub.Application.Questions.Commands.CreateQuestion;
using QuizHub.Application.Questions.Commands.DeleteQuestion;
using QuizHub.Application.Questions.Commands.UpdateQuestion;
using QuizHub.Application.Questions.Queries.GetQuizQuestions;
using QuizHub.Web.Api.Common.Requests.Questions;

[ApiController]
[Route("/quizzes/{quizId:Guid}/questions")]
public class QuestionController : ControllerBase
{
    private readonly ISender mediator;
    private readonly IMapper mapper;

    public QuestionController(ISender mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetFromQuiz([FromRoute] GetQuizQuestionsQuery command)
    {
        return Ok(await mediator.Send(command));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateQuestionRequest request)
    {
        CreateQuestionCommand command = mapper.Map<CreateQuestionCommand>(request);

        return StatusCode(201, await mediator.Send(command));
    }

    [HttpPut]
    [Route("{id:long}")]
    [Authorize]
    public async Task<IActionResult> Delete(UpdateQuestionRequest request)
    {
        UpdateQuestionCommand command = mapper.Map<UpdateQuestionCommand>(request);

        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id:long}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] DeleteQuestionCommand command)
    {
        await mediator.Send(command);

        return NoContent();
    }
}
