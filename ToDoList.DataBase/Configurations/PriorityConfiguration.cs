using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Models.Entities;

namespace ToDoList.DataBase.Configurations;
public class PriorityConfiguration : IEntityTypeConfiguration<Priority>
{
    public void Configure(EntityTypeBuilder<Priority> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Level)
            .IsRequired();

        builder.HasMany(b => b.ToDoItems)
            .WithOne(l => l.Priority);
    }
}
