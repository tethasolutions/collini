using Collini.GestioneInterventi.Domain.Docs;

namespace Collini.GestioneInterventi.Domain.Security;

public class User : BaseEntity
{
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
    public string? Salt { get; set; }
    public string? AccessToken { get; set; }
    public bool Enabled { get; set; }
    public Role Role { get; set; }
    public string? EmailAddress { get; set; }
    
    public ICollection<Activity> Activities { get; set; }

    public User()
    {
        Activities = new List<Activity>();
    }
}