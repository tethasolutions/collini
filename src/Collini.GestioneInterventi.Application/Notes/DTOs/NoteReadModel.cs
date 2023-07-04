using Collini.GestioneInterventi.Application.Customers.DTOs;

namespace Collini.GestioneInterventi.Application.Notes.DTOs
{
    public class NoteReadModel
    {
        public long Id { get; set; }
        public string? Value { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public long OperatorId { get; set; }
        public ContactDto Operator { get; set; }
        public ICollection<NoteAttachmentReadModel> Attachments { get; set; }
    }
}
