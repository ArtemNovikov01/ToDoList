using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionnairesService.Exceptions.Common.Exceptions;
using ToDoList.Application.Services;
using ToDoList.Application.ToDo.Commands.Update;
using ToDoList.Exceptions.Common.Exceptions;

namespace ToDoList.Application.ToDo.Commands.Delete;
public record DeleteToDoCommand : IRequest
{
    public int Id { get; set; }

    public sealed class DeleteToDoCommandHandler : IRequestHandler<DeleteToDoCommand>
    {
        private readonly IToDoDbContext _toDoDbContext;

        public DeleteToDoCommandHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public async Task Handle(DeleteToDoCommand command, CancellationToken cancellationToken)
        {
            ValidateRequestAndThrow(command);

            var toDo = await _toDoDbContext.ToDoItem.FirstOrDefaultAsync(t => t.Id == command.Id, cancellationToken);

            EntityNotFoundException.ThrowIfNull(toDo, $"Задачи с Id={command.Id} не существует.");

            _toDoDbContext.ToDoItem.Remove(toDo!);

            await _toDoDbContext.SaveChangesAsync(cancellationToken);
        }

        private void ValidateRequestAndThrow(DeleteToDoCommand command)
        {
            if (command.Id <= 0)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Не валидное поле 'Id'.");
            }
        }
    }
}
