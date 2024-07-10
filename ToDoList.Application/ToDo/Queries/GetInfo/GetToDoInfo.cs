namespace ToDoList.Application.ToDo.Queries.GetInfo;
public record GetToDoInfo
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public bool IsCompleted { get; init; }
    public int PriorityLevel { get; init; }
    public DateTime DueDate { get; init; }
}
