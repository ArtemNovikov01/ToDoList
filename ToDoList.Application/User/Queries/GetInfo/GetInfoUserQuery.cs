using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionnairesService.Exceptions.Common.Exceptions;
using ToDoList.Application.Services;
using ToDoList.Exceptions.Common.Exceptions;

namespace ToDoList.Application.User.Queries.GetInfo;
public record GetInfoUserQuery : IRequest<GetUserInfoResponse>
{
    public int Id { get; init; }
    public sealed class GetInfoUserQueryHandler : IRequestHandler<GetInfoUserQuery, GetUserInfoResponse>
    {
        private readonly IToDoDbContext _toDoDbContext;
        public GetInfoUserQueryHandler(IToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }
        public async Task<GetUserInfoResponse> Handle (GetInfoUserQuery request, CancellationToken cancellationToken)
        {
            ValidateRequestAndThrow(request);

            var user = await _toDoDbContext.Users
                .Select(u => new GetUserInfoResponse()
                {
                    Id = u.Id,
                    Name = u.Name,
                    ToDos = u.ToDoItems
                        .Select(t => new ToDoForUserDto()
                        {
                            Id = t.Id,
                            Title = t.Title
                        }).ToList()
                })
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            EntityNotFoundException.ThrowIfNull(user, $"Пользователя с Id={request.Id} не существует.");

            return user!;
        }

        private void ValidateRequestAndThrow(GetInfoUserQuery query)
        {
            if (query.Id <= 0)
            {
                throw new BadRequestException(ErrorCodes.Common.BadRequest, "Не валидное поле 'Id'.");
            }
        }
    }
}
