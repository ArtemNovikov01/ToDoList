using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionnairesService.Exceptions.Common.Exceptions;
using ToDoList.Application.Services;
using ToDoList.Exceptions.Common.Exceptions;

namespace ToDoList.Application.User.Commands.Delete;
public record DeleteUserCommand : IRequest
{
    public int Id { get; set; }

    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IToDoDbContext _toDoDbContext;

        public DeleteUserCommandHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            ValidateRequestAndThrow(command);

            var user = await _toDoDbContext.Users.FirstOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

            EntityNotFoundException.ThrowIfNull(user, $"Пользователя с Id={command.Id} не существует.");

            _toDoDbContext.Users.Remove(user!);

            await _toDoDbContext.SaveChangesAsync(cancellationToken);
        }

        private void ValidateRequestAndThrow(DeleteUserCommand command)
        {
            if (command.Id <= 0)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Не валидное поле 'Id'.");
            }
        }
    }
}
