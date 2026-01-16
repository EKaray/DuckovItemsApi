namespace Duckov.Api.Exceptions;

public sealed class AlreadyExistsException : Exception
{
    public string EntityName { get; }
    public object Key { get; }

    public AlreadyExistsException(string entityName, object key)
        : base($"{entityName} with identifier '{key}' already exists.")
    {
        EntityName = entityName;
        Key = key;
    }
}