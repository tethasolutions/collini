using Collini.GestioneInterventi.Domain.Security;

namespace Collini.GestioneInterventi.WebApi.Auth;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class RequireRoleAttribute : Attribute
{
    public Role Role { get; }

    public RequireRoleAttribute(Role role)
    {
        Role = role;
    }
}