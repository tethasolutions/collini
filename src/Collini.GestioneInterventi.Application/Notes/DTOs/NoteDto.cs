using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Application.Security.DTOs;

namespace Collini.GestioneInterventi.Application.Notes.DTOs
{
    public class NoteDto
    {
        public long? Id { get; set; }
        public string? Value { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? OperatorId { get; set; }
        public long? JobId { get; set; }
        public long? OrderId { get; set; }
        public long? QuotationId { get; set; }
        public long? ActivityId { get; set; }
        public IEnumerable<NoteAttachmentDto>? Attachments { get; set; }

    }
}
