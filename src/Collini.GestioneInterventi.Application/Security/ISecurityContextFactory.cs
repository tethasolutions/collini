using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain.Security;
using Collini.GestioneInterventi.Framework.Security;

namespace Collini.GestioneInterventi.Application.Security;

public interface ISecurityContextFactory
{
    ISecurityContext CreateSecurityContext(User user);
}

public class SecurityContextFactory : ISecurityContextFactory
{
    private readonly IPasswordHasher passwordHasher;
    private readonly IAccessTokenGenerator accessTokenGenerator;
    private readonly IRepository<User> userRepository;

    public SecurityContextFactory(
        IPasswordHasher passwordHasher,
        IAccessTokenGenerator accessTokenGenerator,
        IRepository<User> userRepository
    )
    {
        this.passwordHasher = passwordHasher;
        this.accessTokenGenerator = accessTokenGenerator;
        this.userRepository = userRepository;
    }

    public ISecurityContext CreateSecurityContext(User user)
    {
        return new SecurityContext(
            passwordHasher,
            accessTokenGenerator,
            userRepository.Query(),
            user
        );
    }
}