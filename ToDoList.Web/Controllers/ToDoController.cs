using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.ToDo.Commands.Completed;
using ToDoList.Application.ToDo.Commands.Delete;
using ToDoList.Application.ToDo.Commands.Update;
using ToDoList.Application.ToDo.Queries.GetInfo;
using ToDoList.Application.ToDo.Queries.GetList;
using ToDoList.Application.User.Commands.Create;

namespace ToDoList.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly IMediator _mediator;

    public ToDoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task Create(CreateToDoUserCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }

    [HttpPost("update")]
    public async Task Update(UpdateUserCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }

    [HttpPost("delete")]
    public async Task Update(DeleteUserCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }

    [HttpPost("getInfo")]
    public async Task GetInfo(GetInfoToDoQuery query)
    {
        await _mediator.Send(query, HttpContext.RequestAborted);
    }

    [HttpPost("getList")]
    public async Task GetInfo(GetListToDoQuery query)
    {
        await _mediator.Send(query, HttpContext.RequestAborted);
    }

    [HttpPost("changeStatus")]
    public async Task ChangeStatus(ChangeStatusToDoCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }
}
