using Microsoft.EntityFrameworkCore;
using Man = ToDoList.Models.Entities.User;

namespace ToDoList.Application.User.Extensions;
public static class UserExtensions
{
    public static IQueryable<Man> FilterByName(this IQueryable<Man> query, string? name)
    {
        var trimmedNameTerm = name?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(trimmedNameTerm))
        {
            return query;
        }

        return query.Where(u => EF.Functions.Like(u.Name, $"%{trimmedNameTerm}%"));
    }
}
