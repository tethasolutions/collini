using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain;
using Collini.GestioneInterventi.Domain.Security;
using Collini.GestioneInterventi.Framework.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collini.GestioneInterventi.Application.Session;

public class ColliniSession : IColliniSession
{
    private readonly IAccessTokenProvider accessTokenProvider;
    private readonly IServiceProvider serviceProvider;

    private string? accessToken;
    private string AccessToken => accessToken ??= accessTokenProvider.AccessToken;

    private ColliniUser? currentUser;
    public IColliniUser? CurrentUser => currentUser ??= SetCurrentUser().GetAwaiter().GetResult();

    public ColliniSession(
        IAccessTokenProvider accessTokenProvider,
        IServiceProvider serviceProvider
    )
    {
        this.accessTokenProvider = accessTokenProvider;
        this.serviceProvider = serviceProvider;
    }

    public bool IsAuthenticated()
    {
        return CurrentUser is { Enabled: true };
    }

    public bool IsAuthorized(Role role)
    {
        return IsAuthenticated() && CurrentUser.Role == role;
    }

    private async Task<ColliniUser?> SetCurrentUser()
    {
        currentUser = await BuildUser();

        if (currentUser == null)
        {
            return currentUser;
        }

        await FillInfo();

        return currentUser;
    }

    private async Task<ColliniUser?> BuildUser()
    {
        var user = await Query<User>()
            .SingleOrDefaultAsync(e => e.AccessToken == AccessToken);

        if (user == null)
        {
            return null;
        }

        var rivendellUser = new ColliniUser(user.Id, user.Role, user.Enabled, user.AccessToken, user.Salt,
            user.PasswordHash, user.UserName);

        return rivendellUser;
    }

    private Task FillInfo()
    {
        // riempire qui eventuali altre proprietà di ColliniUser recuperate da altre tabelle

        return Task.CompletedTask;
    }

    private IQueryable<T> Query<T>() where T : BaseEntity
    {
        return serviceProvider
            .GetRequiredService<IRepository<T>>()
            .Query()
            .AsNoTracking();
    }
}