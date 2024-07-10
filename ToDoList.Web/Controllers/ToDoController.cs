using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.ToDo.Commands.ChangeStatus;
using ToDoList.Application.ToDo.Commands.Create;
using ToDoList.Application.ToDo.Commands.Delete;
using ToDoList.Application.ToDo.Commands.ToAppoint;
using ToDoList.Application.ToDo.Commands.Update;
using ToDoList.Application.ToDo.Queries.GetInfo;
using ToDoList.Application.ToDo.Queries.GetList;

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
    public async Task Create(CreateToDoCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }

    [HttpPost("update")]
    public async Task Update(UpdateToDoCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }

    [HttpPost("delete")]
    public async Task Update(DeleteToDoCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }

    [HttpPost("getInfo")]
    public async Task<GetToDoInfo> GetInfo(GetInfoToDoQuery query)
    {
        return await _mediator.Send(query, HttpContext.RequestAborted);
    }

    [HttpPost("getList")]
    public async Task<GetListToDoResponse> GetList(GetListToDoQuery query)
    {
        return await _mediator.Send(query, HttpContext.RequestAborted);
    }

    [HttpPost("changeStatus")]
    public async Task ChangeStatus(ChangeStatusToDoCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }

    [HttpPost("toAppoint")]
    public async Task ToAppoint(ToAppointToDoCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }
}
