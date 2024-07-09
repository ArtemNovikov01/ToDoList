using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Services;

namespace ToDoList.Application.Priority.Queries.GetList;
public record GetPriorityQuery : IRequest<GetPriorityResponse>
{
    public sealed class GetPriorityQueryHandler : IRequestHandler<GetPriorityQuery, GetPriorityResponse>
    {
        private readonly IToDoDbContext _toDoDbContext;

        public GetPriorityQueryHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public async Task<GetPriorityResponse> Handle(GetPriorityQuery request, CancellationToken cancellationToken)
        {
            return new GetPriorityResponse()
            {
                Priorities =
                await _toDoDbContext.Priorities.Select(p => new PriorityDto()
                {
                    Id = p.Id,
                    Level = p.Level
                }).ToListAsync(cancellationToken)
            };
        }
    }
}
