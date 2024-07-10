using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionnairesService.Exceptions.Common.Exceptions;
using ToDoList.Application.Services;
using ToDoList.Exceptions.Common.Exceptions;

namespace ToDoList.Application.ToDo.Commands.ToAppoint;
public record ToAppointToDoCommand : IRequest
{
    public int Id { get; init; }
    public int UserId { get; init; }

    public sealed class ToAppointToDoCommandHandler : IRequestHandler<ToAppointToDoCommand>
    {
        private readonly IToDoDbContext _toDoDbContext;
        public ToAppointToDoCommandHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public async Task Handle(ToAppointToDoCommand command, CancellationToken cancellationToken)
        {

            ValidateRequestAndThrow(command);

            var toDo = await _toDoDbContext.ToDoItem.FirstOrDefaultAsync(t => t.Id == command.Id, cancellationToken);

            EntityNotFoundException.ThrowIfNull(toDo, $"Задачи с Id={command.Id} не существует.");

            var user = await _toDoDbContext.Users.FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

            EntityNotFoundException.ThrowIfNull(toDo, $"Пользователя с Id={command.Id} не существует.");

            toDo!.ToAppoint(user!);

            await _toDoDbContext.SaveChangesAsync(cancellationToken);
        }

        private void ValidateRequestAndThrow(ToAppointToDoCommand command)
        {
            if (command.Id <= 0)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Поле 'Id' должно быть больше 0.");
            }

            if (command.UserId <= 0)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Поле 'UserId' должно быть больше 0.");
            }
        }
    }
}
