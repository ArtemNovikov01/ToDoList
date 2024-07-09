using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.ToDo.Commands.Delete;
using ToDoList.Application.User.Commands.Create;

namespace ToDoList.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task Create(CreateToDoUserCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }

    [HttpPost("delete")]
    public async Task Delete(DeleteUserCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }
}
