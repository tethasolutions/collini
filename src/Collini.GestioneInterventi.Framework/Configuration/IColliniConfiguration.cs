namespace Collini.GestioneInterventi.Framework.Configuration;

public interface IColliniConfiguration
{
    bool AllowCors { get; }
    string? CorsOrigins { get; }
}