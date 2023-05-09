namespace Collini.GestioneInterventi.Domain;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}