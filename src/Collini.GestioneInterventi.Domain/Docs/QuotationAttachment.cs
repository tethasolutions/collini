namespace Collini.GestioneInterventi.Domain.Docs;

public class QuotationAttachment : FullAuditedEntity
{
    public string? DisplayName { get; set; }
    public string? FileName { get; set; }

    public long QuotationId { get; set; }
    public Quotation? Quotation { get; set; }
}