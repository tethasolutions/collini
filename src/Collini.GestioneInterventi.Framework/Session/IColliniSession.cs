using Collini.GestioneInterventi.Domain.Security;

namespace Collini.GestioneInterventi.Framework.Session
{
    public interface IColliniSession
    {
        IColliniUser? CurrentUser { get; }

        bool IsAuthenticated();
        bool IsAuthorized(Role role);
    }
}