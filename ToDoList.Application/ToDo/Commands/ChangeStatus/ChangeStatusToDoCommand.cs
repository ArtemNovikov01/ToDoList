using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionnairesService.Exceptions.Common.Exceptions;
using ToDoList.Application.Services;
using ToDoList.Exceptions.Common.Exceptions;

namespace ToDoList.Application.ToDo.Commands.ChangeStatus;
public record ChangeStatusToDoCommand : IRequest
{
    public int Id { get; init; }
    public sealed class ChangeStatusToDoCommandHandler : IRequestHandler<ChangeStatusToDoCommand>
    {
        private readonly IToDoDbContext _toDoDbContext;
        public ChangeStatusToDoCommandHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public async Task Handle(ChangeStatusToDoCommand command, CancellationToken cancellationToken)
        {
            ValidateRequestAndThrow(command);

            var toDo = await _toDoDbContext.ToDoItem.FirstOrDefaultAsync(t => t.Id == command.Id, cancellationToken);

            EntityNotFoundException.ThrowIfNull(toDo, $"Задачи с Id={command.Id} не существует.");

            toDo!.ChangeStatus();

            await _toDoDbContext.SaveChangesAsync(cancellationToken);
        }

        private void ValidateRequestAndThrow(ChangeStatusToDoCommand command)
        {
            if (command.Id <= 0)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Поле 'Id' должно быть больше 0.");
            }
        }
    }
}
