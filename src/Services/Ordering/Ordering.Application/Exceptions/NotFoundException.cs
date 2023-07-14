namespace Ordering.Application.Exceptions;

public sealed class NotFoundException : ApplicationException
{
    public NotFoundException(string name, object key)
        : base($"Entity '{name}' ({key}) was not found. ")
    {
    }
}