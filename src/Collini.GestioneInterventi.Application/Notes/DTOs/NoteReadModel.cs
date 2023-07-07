using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Application.Security.DTOs;

namespace Collini.GestioneInterventi.Application.Notes.DTOs
{
    public class NoteReadModel
    {
        public long Id { get; set; }
        public string? Value { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public long OperatorId { get; set; }
        public UserReadModel Operator { get; set; }
        public ICollection<NoteAttachmentReadModel> Attachments { get; set; }
    }
}
