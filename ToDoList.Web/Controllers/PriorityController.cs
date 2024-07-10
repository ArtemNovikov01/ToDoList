using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Priority.Queries.GetList;

namespace ToDoList.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriorityController : ControllerBase
{
    private readonly IMediator _mediator;

    public PriorityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<GetPriorityResponse> GetList(GetPriorityQuery query)
    {
        return await _mediator.Send(query, HttpContext.RequestAborted);
    }
}
