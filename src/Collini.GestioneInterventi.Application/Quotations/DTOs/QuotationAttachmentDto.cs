namespace Collini.GestioneInterventi.Application.Quotations.DTOs
{
    public class QuotationAttachmentDto
    {
        public long? Id { get; set; }
        public string? DisplayName { get; set; }
        public string? FileName { get; set; }
        public long QuotationId { get; set; }
    }
}
