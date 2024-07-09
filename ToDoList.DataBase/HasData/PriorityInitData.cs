using Microsoft.EntityFrameworkCore;
using ToDoList.Models.Entities;

namespace ToDoList.DataBase.HasData;
public class PriorityInitData
{
    public static void AddData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Priority>().HasData(

                   new Priority ( 1, 1),
                   new Priority ( 2, 2),
                   new Priority ( 3, 3),
                   new Priority ( 4, 4),
                   new Priority ( 5, 5)
           );
    }
}
