using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Note.DTOs
{
    public class NoteReadModel
    {
        public long Id { get; set; }
        public string? Value { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public ContactDto Operator { get; set; }
        public string Type { get; set; }
        public ICollection<NoteAttachmentReadModel> Attachments { get; set; }
    }
}
