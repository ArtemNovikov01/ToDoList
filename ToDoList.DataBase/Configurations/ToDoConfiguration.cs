using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Models.Entities;

namespace ToDoList.DataBase.Configurations;
public class ToDoConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .IsRequired();

        builder.Property(b => b.Description)
            .IsRequired();

        builder.Property(b => b.DueDate)
            .IsRequired();

        builder.Property(b => b.Priority)
            .IsRequired();

        builder.Property(b => b.IsCompleted)
            .IsRequired();

        builder.Property(b => b.User)
            .IsRequired(false);

        builder.HasOne(b => b.Priority)
            .WithMany(l => l.ToDoItems);

        builder.HasOne(b => b.User)
            .WithMany(l => l.ToDoItems);
    }
}
