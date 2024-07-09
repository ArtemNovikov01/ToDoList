using MediatR;
using ToDoList.Application.Services;
using ToDoList.Exceptions.Common.Exceptions;
using Man = ToDoList.Models.Entities.User;

namespace ToDoList.Application.User.Commands.Create;
public record CreateToDoUserCommand : IRequest
{
    public string Name { get; init; } = null!;

    public sealed class CreateUserCommandHandler : IRequestHandler<CreateToDoUserCommand>
    {
        private readonly IToDoDbContext _toDoDbContext;

        public CreateUserCommandHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public async Task Handle(CreateToDoUserCommand command, CancellationToken cancellationToken)
        {
            ValidateRequestAndThrow(command);

            _toDoDbContext.Users.Add(new Man(command.Name));

            await _toDoDbContext.SaveChangesAsync(cancellationToken);
        }

        private void ValidateRequestAndThrow(CreateToDoUserCommand command)
        {
            if (string.IsNullOrEmpty(command.Name.Trim()))
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Поле 'Name' должно быть заполнено.");
            }
        }

    }
}
