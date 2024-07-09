namespace ToDoList.Application.ToDo.Queries.GetInfo;
public record GetToDoInfo
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsCompleted { get; set; }
    public int PriorityLevel { get; set; }
}
