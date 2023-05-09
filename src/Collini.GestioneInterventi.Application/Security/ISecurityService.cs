using System.Linq.Expressions;
using AutoMapper;
using Collini.GestioneInterventi.Application.Security.DTOs;
using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain.Security;
using Collini.GestioneInterventi.Framework.Exceptions;
using Collini.GestioneInterventi.Framework.Extensions;
using Collini.GestioneInterventi.Framework.Session;
using Microsoft.EntityFrameworkCore;

namespace Collini.GestioneInterventi.Application.Security;

public interface ISecurityService
{
    IQueryable<UserReadModel> Query();
    Task<UserDto> Login(string userName, string password);
    Task<UserDto> Register(UserDto user, string password);
    Task<UserDto> ChangeCurrentUserPassword(string currentPassword, string newPassword);
    Task<UserDto> GetUser(long id);
    Task<UserDto> GetCurrentUser();
    Task DeleteUser(long userId);
    Task<UserDto> ExecuteOnUser(long userId, Func<ISecurityContext, Task> action);
    Task<UserDto> GetAdmin();
}

public class SecurityService : ISecurityService
{
    private readonly IRepository<User> userRepository;
    private readonly IColliniDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IColliniSession session;
    private readonly ISecurityContextFactory securityContextFactory;
        
    public SecurityService(
        IRepository<User> userRepository,
        IColliniDbContext dbContext,
        IMapper mapper,
        IColliniSession session,
        ISecurityContextFactory securityContextFactory
    )
    {
        this.userRepository = userRepository;
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.session = session;
        this.securityContextFactory = securityContextFactory;
    }

    public IQueryable<UserReadModel> Query()
    {
        return userRepository.Query()
            .AsNoTracking()
            .Project<UserReadModel>(mapper);
    }

    public async Task<UserDto> Login(string userName, string password)
    {
        const string errorMessage = "Nome utente o password non validi.";
        var passwordVerified = false;
        var user = await ExecuteOnUser(e => e.UserName == userName,
            context => Task.FromResult(passwordVerified = context.VerifyPassword(password)),
            errorMessage
        );

        if (!passwordVerified)
        {
            throw new ColliniException(errorMessage);
        }

        return user.MapTo<UserDto>(mapper);
    }

    public async Task<UserDto> Register(UserDto userDto, string password)
    {
        var user = userDto.MapTo<User>(mapper);

        await ExecuteOnUser(user, async context =>
        {
            await context.EnsureUserNameIsUnique(user.UserName);
            await context.HashPasswordWithUniqueSalt(password);
            await context.GenerateUniqueAccessToken();
        });

        return user.MapTo<UserDto>(mapper);
    }

    public async Task<UserDto> ChangeCurrentUserPassword(string currentPassword, string newPassword)
    {
        var dto = await ExecuteOnUser(
            session.CurrentUser.UserId,
            context => context.ChangePassword(newPassword)
        );

        return dto;
    }

    public async Task<UserDto> GetUser(long userId)
    {
        var user = await GetUser(e => e.Id == userId);

        return user.MapTo<UserDto>(mapper);
    }

    public Task<UserDto> GetCurrentUser()
    {
        return GetUser(session.CurrentUser.UserId);
    }

    public async Task DeleteUser(long userId)
    {
        var user = await GetUser(e => e.Id == userId);

        userRepository.Delete(user);

        await dbContext.SaveChanges();
    }

    public async Task<UserDto> ExecuteOnUser(long userId, Func<ISecurityContext, Task> action)
    {
        var user = await ExecuteOnUser(e => e.Id == userId, action);

        return user.MapTo<UserDto>(mapper);
    }

    public async Task<UserDto> GetAdmin()
    {
        var user = await GetUser(e => e.Role == Role.Administrator);
         
        return user.MapTo<UserDto>(mapper);
    }
        
    private async Task<User> GetUser(Expression<Func<User, bool>> expression, string? errorMessage = null)
    {
        var user = await userRepository.Query()
            .FirstOrDefaultAsync(expression);

        if (user == null)
        {
            throw new NotFoundException(errorMessage ?? "Utente non trovato.");
        }

        return user;
    }

    private async Task<User> ExecuteOnUser(Expression<Func<User, bool>> getUserExpression,
        Func<ISecurityContext, Task> action, string? errorMessage = null)
    {
        var user = await GetUser(getUserExpression, errorMessage);

        await ExecuteOnUser(user, action);

        return user;
    }

    private async Task ExecuteOnUser(User user, Func<ISecurityContext, Task> action)
    {
        using (var context = securityContextFactory.CreateSecurityContext(user))
        {
            await action(context);
        }

        await userRepository.InsertOrUpdate(user);
        await dbContext.SaveChanges();
    }
}