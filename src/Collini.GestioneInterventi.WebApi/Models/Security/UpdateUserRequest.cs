using Collini.GestioneInterventi.Domain.Security;

namespace Collini.GestioneInterventi.WebApi.Models.Security;

public class UpdateUserRequest
{
    public string? Password { get; set; }
    public string? UserName { get; set; }
    public bool Enabled { get; set; }
    public Role Role { get; set; }
    public string? EmailAddress { get; set; }
}