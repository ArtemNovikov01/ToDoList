namespace ToDoList.Application.ToDo.Queries.GetList;
public class GetListToDoResponse
{
    public List<ToDoDto> ToDoDtos { get; init; } =  new();
    public int TotalCount { get; init; }
}
