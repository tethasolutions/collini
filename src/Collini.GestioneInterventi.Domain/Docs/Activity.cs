using Collini.GestioneInterventi.Domain.Security;

namespace Collini.GestioneInterventi.Domain.Docs;

public class Activity : FullAuditedEntity
{
    public string? Description { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public ActivityStatus Status { get; set; }
    public DateTimeOffset? StatusChangedOn { get; set; }
    
    public long OperatorId { get; set; }
    public User? Operator { get; set; }

    public long JobId { get; set; }
    public Job? Job { get; set; }

    public ICollection<Note> Notes { get; set; }

    public Activity()
    {
        Notes = new List<Note>();
    }
}