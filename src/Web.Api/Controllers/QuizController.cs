namespace QuizHub.Web.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MediatR;
using AutoMapper;

using QuizHub.Application.Quizzes.Commands.CreateQuiz;
using QuizHub.Application.Quizzes.Commands.UpdateQuizz;
using QuizHub.Application.Quizzes.Commands.DeleteQuiz;
using QuizHub.Application.Quizzes.Queries.GetUserQuizzes;
using QuizHub.Application.Quizzes.Queries.GetQuizById;
using QuizHub.Web.Api.Common.Requests.Quizzes;

[ApiController]
[Route("/quizzes")]
public class QuizController : ControllerBase
{
    private readonly ISender mediator;
    private readonly IMapper mapper;

    public QuizController(ISender mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] GetQuizByIdQuery query)
    {
        return Ok(await mediator.Send(query));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetFromUser([FromQuery] GetUserQuizzesQuery query)
    {
        return Ok(await mediator.Send(query));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromForm] CreateQuizCommand command)
    {
        return StatusCode(201, await mediator.Send(command));
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Update(UpdateQuizRequest request)
    {
        UpdateQuizCommand command = mapper.Map<UpdateQuizCommand>(request);

        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] DeleteQuizCommand command)
    {
        await mediator.Send(command);

        return NoContent();
    }
}
