using ToDoList.Models.Entities;

namespace ToDoList.Application.ToDo.Extensions;
public static class UserExtensions
{
    public static IQueryable<ToDoItem> FilterByStatus(this IQueryable<ToDoItem> query, bool? status)
    {
        if (status is null)
        {
            return query;
        }

        return query.Where(t => t.IsCompleted == status);
    }

    public static IQueryable<ToDoItem> FilterByPriority(this IQueryable<ToDoItem> query, int? priorityId)
    {
        if (priorityId is null)
        {
            return query;
        }

        return query.Where(t => t.PriorityId == priorityId);
    }
}
