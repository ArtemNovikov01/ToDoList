using ToDoList.DataBase.Setting;

namespace ToDoList.Backend.Settings;

/// <summary>
/// Настройки приложения
/// </summary>
public sealed record WebAppSettings
{
    /// <summary>
    /// Настройки БД
    /// </summary>
    public DatabaseSettings? Database { get; init; }
}