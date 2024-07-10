using MediatR;
using QuestionnairesService.Exceptions.Common.Exceptions;
using ToDoList.Application.Services;
using ToDoList.Exceptions.Common.Exceptions;
using ToDoList.Models.Entities;

namespace ToDoList.Application.ToDo.Commands.Create;
public record CreateToDoCommand : IRequest
{
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public DateTime DueDate { get; init; }
    public int PriorityId { get; init; }

    public sealed class CreateToDoCommandHandler : IRequestHandler<CreateToDoCommand>
    {
        private readonly IToDoDbContext _toDoDbContext;

        public CreateToDoCommandHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public async Task Handle(CreateToDoCommand command, CancellationToken cancellationToken)
        {
            ValidateRequestAndThrow(command);

            var priority = _toDoDbContext.Priorities.FirstOrDefault(p => p.Id == command.PriorityId);

            EntityNotFoundException.ThrowIfNull(priority, $"Приоритета с Id={command.PriorityId} не существует.");

            _toDoDbContext.ToDoItem
                .Add(new ToDoItem(
                        command.Title, 
                        command.Description, 
                        command.DueDate,
                        command.PriorityId));

            await _toDoDbContext.SaveChangesAsync(cancellationToken);
        }

        private void ValidateRequestAndThrow(CreateToDoCommand command)
        {
            if (command.DueDate < DateTime.UtcNow)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Дата выполнения не может быть меньше текущей даты.");
            }
            if (string.IsNullOrEmpty(command.Title.Trim()))
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Поле 'Title' должно быть заполнено.");
            }
            if (string.IsNullOrEmpty(command.Description.Trim()))
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Поле 'Description' должно быть заполнено.");
            }
            if (command.PriorityId <= 0)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "'PriorityId' должен быть больше нуля.");
            }
        }

    }
}
