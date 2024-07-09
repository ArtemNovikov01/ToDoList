using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Services;
using ToDoList.DataBase.HasData;
using ToDoList.Models.Entities;

namespace ToDoList.DataBase;
public class ToDoDbContext : DbContext, IToDoDbContext
{
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        PriorityInitData.AddData(modelBuilder);
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Priority> Prioritys => Set<Priority>();

    public DbSet<ToDoItem> ToDoItem => Set<ToDoItem>();
}
