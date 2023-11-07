namespace QuizHub.Web.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MediatR;

using AutoMapper;

using QuizHub.Application.Common.Exceptions;
using QuizHub.Application.QuizSessions.Queries.GetUserClosedSessions;
using QuizHub.Application.QuizSessions.Commands.CreateQuizSession;
using QuizHub.Application.QuizSessions.Commands.CloseQuizSession;
using QuizHub.Application.QuizSessionResults.Commands.SendAnswer;
using QuizHub.Web.Api.Common.Requests.QuizSessions;

[ApiController]
[Route("/quizzes/{quizId:Guid}/sessions")]
public class QuizSessionController : ControllerBase
{
    private readonly ISender mediator;
    private readonly IMapper mapper;

    public QuizSessionController(ISender mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetFromUser([FromRoute] GetUserClosedSessionsQuery query)
    {
        return Ok(await mediator.Send(query));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromRoute] CreateQuizSessionCommand command)
    {
        return StatusCode(201, await mediator.Send(command));
    }

    [HttpPost]
    [Route("{id:long}/close")]
    [Authorize]
    public async Task<IActionResult> Close([FromRoute] CloseQuizSessionCommand command)
    {
        await mediator.Send(command);

        return NoContent();
    }

    [HttpPost]
    [Route("{id:long}/answers")]
    [Authorize]
    public async Task<IActionResult> SendAnswer(SendAnswerRequest request)
    {
        SendAnswerCommand command = mapper.Map<SendAnswerCommand>(request);

        try
        {
            return Ok(await mediator.Send(command));
        }
        catch (EntityArleadyExistsException)
        {
            return Conflict("You arleady sent answer for this question");
        }
        catch (QuizSessionInvalidException)
        {
            return BadRequest("Session closed or expired");
        }
    }
}
