using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Services;

namespace ToDoList.Application.ToDo.Queries.GetList;
public record GetListToDoQuery : IRequest<GetListToDoResponse>
{
    public sealed class GetListToDoQueryHandler : IRequestHandler<GetListToDoQuery, GetListToDoResponse>
    {
        private readonly IToDoDbContext _toDoDbContext;
        public GetListToDoQueryHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }
        public async Task<GetListToDoResponse> Handle(GetListToDoQuery query, CancellationToken cancellationToken)
        {
            return new GetListToDoResponse(){
                ToDoDtos = await _toDoDbContext.ToDoItem.Select(t => new ToDoDto()
                {
                    Id = t.Id,
                    Title = t.Title
                }).ToListAsync(cancellationToken)
            };
        }
    }
}
