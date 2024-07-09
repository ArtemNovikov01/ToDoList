namespace ToDoList.Application.Priority.Queries.GetList;
public record GetPriorityResponse
{
    public List<PriorityDto> Priorities { get; init; } = new List<PriorityDto>();
}
