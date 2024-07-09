using ToDoList.Exceptions.Common.Exceptions;

namespace QuestionnairesService.Exceptions.Common.Exceptions;

public class EntityNotFoundException : BaseException
{
    private EntityNotFoundException(string message) : base(ErrorCodes.Common.NotFound, message)
    {}

    public static void ThrowIfNull<T>( T? entity, string messageTemplate,
        params object[] args)
    {
        if (entity is null)
        {
            Throw(messageTemplate, args);
        }
    }

    public static void ThrowIfAny<T>(IEnumerable<T> entities, string messageTemplate, params object[] args)
    {
        if (entities.Any())
        {
            Throw(messageTemplate, args);
        }
    }

    public static void Throw(string messageTemplate, params object[] args)
    {
        throw new EntityNotFoundException(string.Format(messageTemplate, args));
    }
}