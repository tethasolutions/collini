using Collini.GestioneInterventi.Domain.Security;

namespace Collini.GestioneInterventi.Framework.Session
{
    public interface IColliniUser
    {
        long UserId { get; }
        Role Role { get; }
        bool Enabled { get; }
        string AccessToken { get; }
        string Salt { get; }
        string PasswordHash { get; }
        string UserName { get; }
    }
}
