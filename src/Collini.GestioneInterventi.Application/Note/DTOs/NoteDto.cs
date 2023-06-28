using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Note.DTOs
{
    public class NoteDto
    {
        public long Id { get; set; }
        public string? Value { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public long OperatorId { get; set; }
        public ContactDto Operator { get; set; }
        public long? JobId { get; set; }
        public long? OrderId { get; set; }
        public long? QuotationId { get; set; }
        public long? ActivityId { get; set; }
        public ICollection<NoteAttachmentReadModel> Attachments { get; set; }
    }
}
