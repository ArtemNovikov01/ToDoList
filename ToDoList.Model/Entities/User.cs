namespace ToDoList.Models.Entities;

/// <summary>
///     Модель пользователя.
/// </summary>
public class User
{
    public int Id { get; private set; }

    /// <summary>
    ///     Имя.
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>
    ///     Список задач.
    /// </summary>
    public List<ToDoItem> ToDoItems { get; private set; } = new List<ToDoItem>();

    public User()
    {

    }

    public User(string name)
    {
        Name = name;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}
