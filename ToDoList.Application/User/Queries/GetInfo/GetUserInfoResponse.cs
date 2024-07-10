namespace ToDoList.Application.User.Queries.GetInfo;
public record GetUserInfoResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public List<ToDoForUserDto> ToDos { get; init; } = new();
}
