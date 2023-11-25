using Collini.GestioneInterventi.Domain.Registry;

namespace Collini.GestioneInterventi.Domain.Docs;

public class Job : FullAuditedEntity
{
    public int Number { get; set; }
    public int Year { get; set; }
    public DateTimeOffset JobDate { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
    public string? Description { get; set; }
    public string? ResultNote { get; set; }
    public JobStatus Status { get; set; }
    public DateTimeOffset? StatusChangedOn { get; set; }
    public bool? IsPaid { get; set; }

    public long CustomerId { get; set; }
    public Contact? Customer { get; set; }

    public long? CustomerAddressId { get; set; }
    public ContactAddress? CustomerAddress { get; set; }

    public long SourceId { get; set; }
    public JobSource? Source { get; set; }

    public long ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; }

    public ICollection<Note> Notes { get; set; }
    public ICollection<Quotation> Quotations { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Activity> Activities { get; set; }

    public Job()
    {
        Notes = new List<Note>();
        Quotations = new List<Quotation>();
        Orders = new List<Order>();
        Activities = new List<Activity>();
    }
}