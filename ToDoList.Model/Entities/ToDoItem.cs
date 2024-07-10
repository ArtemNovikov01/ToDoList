using Microsoft.VisualBasic;

namespace ToDoList.Models.Entities;
public class ToDoItem
{
    public int Id { get; private set; }

    /// <summary>
    ///     Заголовок.
    /// </summary>
    public string Title { get; private set; } = null!;

    /// <summary>
    ///     Описание.
    /// </summary>
    public string Description { get; private set; } = null!;

    /// <summary>
    ///     Завершенность.
    /// </summary>
    public bool IsCompleted { get; private set; } = false;

    /// <summary>
    ///     Срок.
    /// </summary>
    public DateTime DueDate { get; private set; } = DateTime.UtcNow;

    /// <summary>
    ///     Приоритет.
    /// </summary>
    public Priority Priority { get; private set; } = null!;
    public int PriorityId { get; private set; }

    /// <summary>
    ///     Пользователь.
    /// </summary>
    public User? User { get; private set; }

    public ToDoItem()
    {

    }

    public ToDoItem(string title, 
        string description, 
        DateTime dueDate,
        int priorityId)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        PriorityId = priorityId;
    }

    public void ToAppoint(User user)
    {
        User = user;
    }

    public void UpdateToDo(string title, string description, DateTime dueDate, int priorityId)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        PriorityId = priorityId;
    }

    public void ChangeStatus()
    {
        IsCompleted = !IsCompleted ? true : false;
    }
}
