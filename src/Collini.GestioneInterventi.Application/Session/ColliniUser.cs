using Collini.GestioneInterventi.Domain.Security;
using Collini.GestioneInterventi.Framework.Session;

namespace Collini.GestioneInterventi.Application.Session;

public class ColliniUser : IColliniUser
{
    public long UserId { get; }
    public Role Role { get; }
    public bool Enabled { get; }
    public string AccessToken { get; }
    public string Salt { get; }
    public string PasswordHash { get; }
    public string UserName { get; }

    public ColliniUser(long userId, Role role, bool enabled, string accessToken,
        string salt, string passwordHash, string userName)
    {
        UserId = userId;
        Role = role;
        Enabled = enabled;
        AccessToken = accessToken;
        Salt = salt;
        PasswordHash = passwordHash;
        UserName = userName;
    }
}