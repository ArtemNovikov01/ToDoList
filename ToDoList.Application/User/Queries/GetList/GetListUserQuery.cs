using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Services;
using ToDoList.Application.User.Extensions;
using ToDoList.Exceptions.Common.Exceptions;

namespace ToDoList.Application.User.Queries.GetList;
public record GetListUserQuery : IRequest<GetListUserResponse>
{
    public string? Name { get; init; }
    public int Skip { get; init; }
    public int Take { get; init; }
    public sealed class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, GetListUserResponse>
    {
        private readonly IToDoDbContext _toDoDbContext;
        public GetListUserQueryHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }
        public async Task<GetListUserResponse> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            ValidateRequestAndThrow(request);

            var quary = _toDoDbContext.Users
                .FilterByName(request.Name);

            var totalCount = await quary
                .CountAsync(cancellationToken);

            return new GetListUserResponse() {
                Users = await quary
                    .OrderBy(t => t.Id)
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .Select(t => new UserDto()
                    {
                        Id = t.Id,
                        Name = t.Name
                    }).ToListAsync(cancellationToken),
                TotalCount = totalCount
            };
        }

        private void ValidateRequestAndThrow(GetListUserQuery request)
        {
            if (request.Skip < 0)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Skip должен быть больше или равен нулю.");
            }

            if (request.Take < 0)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Take должен быть больше или равен нулю.");
            }
        }
    }
}
