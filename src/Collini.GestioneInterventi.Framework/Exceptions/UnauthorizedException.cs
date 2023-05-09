namespace Collini.GestioneInterventi.Framework.Exceptions;

public class UnauthorizedException : ColliniException
{
    public UnauthorizedException()
    {

    }

    public UnauthorizedException(string message)
        : base(message)
    {

    }
}