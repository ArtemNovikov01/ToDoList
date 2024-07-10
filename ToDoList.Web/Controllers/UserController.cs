using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.ToDo.Commands.Delete;
using ToDoList.Application.User.Commands.Create;
using ToDoList.Application.User.Commands.Delete;
using ToDoList.Application.User.Queries.GetInfo;
using ToDoList.Application.User.Queries.GetList;

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
    public async Task Create(CreateUserCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }

    [HttpPost("delete")]
    public async Task Delete(DeleteUserCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }

    [HttpPost("getInfo")]
    public async Task<GetUserInfoResponse> GetInfo(GetInfoUserQuery command)
    {
        return await _mediator.Send(command, HttpContext.RequestAborted);
    }

    [HttpPost("getList")]
    public async Task<GetListUserResponse> GetList(GetListUserQuery command)
    {
        return await _mediator.Send(command, HttpContext.RequestAborted);
    }
}
