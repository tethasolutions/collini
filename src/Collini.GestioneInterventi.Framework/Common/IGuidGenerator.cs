namespace Collini.GestioneInterventi.Framework.Common;

public interface IGuidGenerator
{
    string Generate();
}

public class GuidGenerator : IGuidGenerator
{
    public string Generate()
    {
        return Guid.NewGuid().ToString();
    }
}