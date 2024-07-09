using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionnairesService.Exceptions.Common.Exceptions;
using ToDoList.Application.Services;
using ToDoList.Exceptions.Common.Exceptions;

namespace ToDoList.Application.ToDo.Commands.Update;
public record UpdateUserCommand : IRequest
{
    public int Id { get; set; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public DateTime DueDate { get; init; }
    public int PriorityId { get; init; }

    public sealed class UpdateToDoCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IToDoDbContext _toDoDbContext;

        public UpdateToDoCommandHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            ValidateRequestAndThrow(command);

            var toDo = await _toDoDbContext.ToDoItem.FirstOrDefaultAsync(t => t.Id == command.Id, cancellationToken);

            EntityNotFoundException.ThrowIfNull(toDo, $"Задачи с Id={command.Id} не существует.");

            toDo!.UpdateToDo(command.Title, command.Description, command.DueDate, command.PriorityId);

            await _toDoDbContext.SaveChangesAsync(cancellationToken);
        }

        private void ValidateRequestAndThrow(UpdateUserCommand command)
        {
            if (command.Id <= 0)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Не валидное поле 'Id'.");
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
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Не валидное поле 'PriorityId'.");
            }
        }
    }
}
