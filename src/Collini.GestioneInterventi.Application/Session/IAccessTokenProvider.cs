namespace Collini.GestioneInterventi.Application.Session;

public interface IAccessTokenProvider
{
    string? AccessToken { get; }
}