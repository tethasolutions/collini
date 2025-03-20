using Collini.GestioneInterventi.Domain.Docs;

namespace Collini.GestioneInterventi.Domain.Registry;

public class SmtpSettings: BaseEntity
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string From { get; set; }
}