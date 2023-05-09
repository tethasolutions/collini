namespace Collini.GestioneInterventi.Framework.Exceptions;

public class NotFoundException : ColliniException
{
    public NotFoundException(Type entityType, long id)
        : base($"Entity of type [{entityType.Name}] with id [{id}] not found.")
    {

    }

    public NotFoundException(string message)
        : base(message)
    {

    }
}