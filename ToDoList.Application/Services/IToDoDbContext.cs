using Microsoft.EntityFrameworkCore;
using ToDoList.Models.Entities;
using Prioritet =  ToDoList.Models.Entities.Priority;
using Man = ToDoList.Models.Entities.User;

namespace ToDoList.Application.Services;
public interface IToDoDbContext
{
    public DbSet<Man> Users { get; }
    public DbSet<ToDoItem> ToDoItem { get; }
    public DbSet<Prioritet> Priorities { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
