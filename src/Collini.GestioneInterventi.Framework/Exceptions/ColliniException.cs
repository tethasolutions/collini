namespace Collini.GestioneInterventi.Framework.Exceptions;

public class ColliniException : Exception
{
    public ColliniException()
    {

    }

    public ColliniException(string message)
        : base(message)
    {

    }

    public ColliniException(string message, Exception innerException)
        : base(message, innerException)
    {

    }

    public string GetMessageRecursive()
    {
        if (InnerException is ColliniException innerException)
        {
            return $"{Message} {innerException.GetMessageRecursive()}";
        }

        return Message;
    }
}