using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using QuestionnairesService.Exceptions.Common.Exceptions;
using ToDoList.Application.Services;
using ToDoList.Exceptions.Common.Exceptions;

namespace ToDoList.Application.ToDo.Queries.GetInfo;
public record GetInfoToDoQuery : IRequest<GetToDoInfo>
{
    public int Id { get; init; }
    public sealed class GetInfoToDoQueryHandler : IRequestHandler<GetInfoToDoQuery, GetToDoInfo>
    {
        private readonly IToDoDbContext _toDoDbContext;
        public GetInfoToDoQueryHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }
        public async Task<GetToDoInfo> Handle (GetInfoToDoQuery query, CancellationToken cancellationToken)
        {
            ValidateRequestAndThrow(query);

            var toDo = await _toDoDbContext.ToDoItem
                .Include(t => t.Priority)
                .FirstOrDefaultAsync(t => t.Id == query.Id, cancellationToken);

            EntityNotFoundException.ThrowIfNull(toDo, $"Задачи с Id={query.Id} не существует.");

            return new GetToDoInfo
            { 
                Id = toDo!.Id,
                Title = toDo.Title,
                Description = toDo.Description,
                IsCompleted = toDo.IsCompleted,
                PriorityLevel = toDo.Priority.Level,
                DueDate = toDo.DueDate
            };
        }

        private void ValidateRequestAndThrow(GetInfoToDoQuery query)
        {
            if (query.Id <= 0)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Не валидное поле 'Id'.");
            }
        }
    }
}
