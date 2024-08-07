﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Services;
using ToDoList.Application.ToDo.Extensions;
using ToDoList.Exceptions.Common.Exceptions;

namespace ToDoList.Application.ToDo.Queries.GetList;
public record GetListToDoQuery : IRequest<GetListToDoResponse>
{
    public bool? Status { get; init; }
    public int? PriorityId { get; init; }
    public int Skip { get; init; }
    public int Take { get; init; }
    public sealed class GetListToDoQueryHandler : IRequestHandler<GetListToDoQuery, GetListToDoResponse>
    {
        private readonly IToDoDbContext _toDoDbContext;
        public GetListToDoQueryHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }
        public async Task<GetListToDoResponse> Handle(GetListToDoQuery request, CancellationToken cancellationToken)
        {
            ValidateRequestAndThrow(request);

            var quary = _toDoDbContext.ToDoItem
                .FilterByStatus(request.Status)
                .FilterByPriority(request.PriorityId);

            var totalCount = await quary
                .CountAsync(cancellationToken);

            return new GetListToDoResponse() {
                ToDoDtos = await quary
                    .OrderBy(t => t.Id)
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .Select(t => new ToDoDto()
                    {
                        Id = t.Id,
                        Title = t.Title
                    }).ToListAsync(cancellationToken),
                TotalCount = totalCount
            };
        }

        private void ValidateRequestAndThrow(GetListToDoQuery request)
        {
            if (request.PriorityId <= 0)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "PriorityId должен быть больше нулю.");
            }

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
