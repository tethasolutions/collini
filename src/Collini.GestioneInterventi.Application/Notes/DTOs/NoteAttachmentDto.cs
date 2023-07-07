namespace Collini.GestioneInterventi.Application.Notes.DTOs
{
    public class NoteAttachmentDto
    {
        public long? Id { get; set; }
        public string? DisplayName { get; set; }
        public string? FileName { get; set; }
        public long NoteId { get; set; }
    }
}
