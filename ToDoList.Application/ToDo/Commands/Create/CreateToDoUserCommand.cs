using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionnairesService.Exceptions.Common.Exceptions;
using ToDoList.Application.Services;
using ToDoList.Exceptions.Common.Exceptions;
using ToDoList.Models.Entities;

namespace ToDoList.Application.ToDo.Commands.Create;
public record CreateToDoUserCommand : IRequest
{
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public int PriorityId { get; init; }
    public int UserId { get; init; }

    public sealed class CreateToDoUserCommandHandler : IRequestHandler<CreateToDoUserCommand>
    {
        private readonly IToDoDbContext _toDoDbContext;

        public CreateToDoUserCommandHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public async Task Handle(CreateToDoUserCommand command, CancellationToken cancellationToken)
        {
            ValidateRequestAndThrow(command);

            _toDoDbContext.ToDoItem
                .Add(new ToDoItem(
                        command.Title, 
                        command.Description, 
                        command.PriorityId, 
                        command.UserId));

            await _toDoDbContext.SaveChangesAsync(cancellationToken);
        }

        private void ValidateRequestAndThrow(CreateToDoUserCommand command)
        {
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
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Невалидный 'PriorityId'.");
            }
            if (command.UserId <= 0 )
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Невалидный 'UserId'.");
            }
        }

    }
}
