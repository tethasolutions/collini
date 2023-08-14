namespace Collini.GestioneInterventi.Domain.Docs;

public class Quotation : FullAuditedEntity
{
    public decimal Amount { get; set; }
    public DateTimeOffset? ExpirationDate { get; set; }
    public QuotationStatus Status { get; set; }
    public DateTimeOffset? StatusChangedOn { get; set; }

    public long JobId { get; set; }
    public Job? Job { get; set; }

    public ICollection<Note> Notes { get; set; }
    public QuotationAttachment? Attachment { get; set; }

    public Quotation()
    {
        Notes = new List<Note>();
    }
}