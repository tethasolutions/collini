using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Note.DTOs
{
    public class NoteAttachmentDto
    {
        public long Id { get; set; }
        public string? DisplayName { get; set; }
        public string? FileName { get; set; }
    }
}
