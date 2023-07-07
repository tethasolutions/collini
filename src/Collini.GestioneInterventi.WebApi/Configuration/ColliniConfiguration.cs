using Collini.GestioneInterventi.Framework.Configuration;

namespace Collini.GestioneInterventi.WebApi.Configuration;

public class ColliniConfiguration : IColliniConfiguration
{
    public bool AllowCors { get; set; }
    public string? CorsOrigins { get; set; }
    public string? AttachmentsPath { get; set; }
}