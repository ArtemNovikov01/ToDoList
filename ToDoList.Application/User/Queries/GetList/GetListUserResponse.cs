namespace ToDoList.Application.User.Queries.GetList;
public class GetListUserResponse
{
    public List<UserDto> Users { get; init; } =  new();
    public int TotalCount { get; init; }
}
