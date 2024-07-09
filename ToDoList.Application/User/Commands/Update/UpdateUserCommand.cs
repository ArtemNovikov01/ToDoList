using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionnairesService.Exceptions.Common.Exceptions;
using ToDoList.Application.Services;
using ToDoList.Exceptions.Common.Exceptions;

namespace ToDoList.Application.User.Commands.Update;
public record AddToDoCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; init; } = null!;

    public sealed class UpdateUserCommandHandler : IRequestHandler<AddToDoCommand>
    {
        private readonly IToDoDbContext _toDoDbContext;

        public UpdateUserCommandHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public async Task Handle(AddToDoCommand command, CancellationToken cancellationToken)
        {
            ValidateRequestAndThrow(command);

            var user = await _toDoDbContext.Users.FirstOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

            EntityNotFoundException.ThrowIfNull(user, $"Пользователя с Id={command.Id} не существует.");

            user!.UpdateName(command.Name);

            await _toDoDbContext.SaveChangesAsync(cancellationToken);
        }

        private void ValidateRequestAndThrow(AddToDoCommand command)
        {
            if (command.Id <= 0)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Не валидное поле 'Id'.");
            }

            if (string.IsNullOrEmpty(command.Name.Trim()))
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Поле 'Name' должно быть заполнено.");
            }
        }
    }
}
