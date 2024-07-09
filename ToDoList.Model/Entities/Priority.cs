namespace ToDoList.Models.Entities;
public class Priority
{
    public int Id { get; private set; }

    /// <summary>
    ///     Уровень важности.
    /// </summary>
    public int Level { get; private set; }

    /// <summary>
    ///     Список задач.
    /// </summary>
    public List<ToDoItem> ToDoItems { get; private set; } = new List<ToDoItem>();

    public Priority()
    {

    }

    public Priority(int id, int level)
    {
        Id = id;
        Level = level;
    }

    public void AddToDo(ToDoItem toDo)
    {
        ToDoItems.Add(toDo);
    }
}
