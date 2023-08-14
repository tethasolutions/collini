using AutoMapper;
using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Application.Notes.DTOs;
using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Framework.Exceptions;
using Collini.GestioneInterventi.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Collini.GestioneInterventi.Application.Notes.Services
{

    public interface INotesService
    {
        Task<IEnumerable<NoteReadModel>> GetJobNotes(long jobId);
        Task<IEnumerable<NoteReadModel>> GetActivityNotes(long activityId);
        Task<NoteReadModel> GetNoteDetail(long noteId);
        Task<NoteDto> UpdateNote(long id, NoteDto noteDto);
        Task<NoteDto> CreateNote(NoteDto noteDto);
        Task<IEnumerable<NoteAttachmentReadModel>> GetNoteAttachments(long noteId);
        Task<NoteAttachmentReadModel> GetNoteAttachmentDetail(long id);
        Task<NoteAttachmentDto> UpdateNoteAttachment(long id, NoteAttachmentDto attachmentDto); 
        Task<NoteAttachmentDto> CreateNoteAttachment(NoteAttachmentDto attachmentDto);
        Task<IEnumerable<NoteReadModel>> GetQuotationNotes(long quotationId);

        Task<IEnumerable<NoteReadModel>> GetOrderNotes(long orderId);
    }

    public class NoteService:INotesService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Note> noteRepository;
        private readonly IRepository<NoteAttachment> noteAttachmentRepository;
        private readonly IColliniDbContext dbContext;

        public NoteService(
            IMapper mapper,
            IRepository<Note> noteRepository,
            IColliniDbContext dbContext, IRepository<NoteAttachment> noteAttachmentRepository)
        {
            this.mapper = mapper;
            this.noteRepository = noteRepository;
            this.dbContext = dbContext;
            this.noteAttachmentRepository = noteAttachmentRepository;
        }

        public async Task<IEnumerable<NoteReadModel>> GetJobNotes(long jobId)
        {
            var notes = await noteRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Attachments)
                .Where(x => x.JobId == jobId)
                .ToArrayAsync();
            return notes.MapTo<IEnumerable<NoteReadModel>>(mapper);
        }

        public async Task<IEnumerable<NoteReadModel>> GetActivityNotes(long activityId)
        {
            var notes = await noteRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Attachments)
                .Where(x => x.ActivityId == activityId)
                .ToArrayAsync();
            return notes.MapTo<IEnumerable<NoteReadModel>>(mapper);
        }

        public async Task<NoteReadModel> GetNoteDetail(long noteId)
        {
            var note = await noteRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Attachments)
                .Where(x => x.Id == noteId)
                .SingleOrDefaultAsync();

            return note.MapTo<NoteReadModel>(mapper);
        }

        public async Task<NoteDto> UpdateNote(long id, NoteDto noteDto)
        {

            if (id == 0)
                throw new ApplicationException("Impossibile aggiornare una nota con id 0");

            var note= await noteRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x=>x.Attachments)
                .SingleOrDefaultAsync();;

            if (note == null)
                throw new ApplicationException($"Impossibile trovare una nota con id {id}");
            
           
            noteDto.MapTo(note, mapper);
            
           
            noteRepository.Update(note);
            await dbContext.SaveChanges();

            return note.MapTo<NoteDto>(mapper);

        }

        public async Task<NoteDto> CreateNote(NoteDto noteDto)
        {
            var note = noteDto.MapTo<Note>(mapper); 
            
            await noteRepository.Insert(note);

            
            foreach (var file in note.Attachments)
            {
                var noteAttachment = file.MapTo<NoteAttachment>(mapper);
                noteAttachment.NoteId = note.Id;
                await noteAttachmentRepository.Insert(noteAttachment);
            }
            await dbContext.SaveChanges();

            return note.MapTo<NoteDto>(mapper);
        }

        public async Task<IEnumerable<NoteAttachmentReadModel>> GetNoteAttachments(long noteId)
        {
            var noteAttachments = await noteAttachmentRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.NoteId == noteId)
                .OrderBy(x=>x.CreatedOn)
                .ToArrayAsync();

            return noteAttachments.MapTo<IEnumerable<NoteAttachmentReadModel>>(mapper);
        }

        public async Task<NoteAttachmentReadModel> GetNoteAttachmentDetail(long attachmentId)
        {
            var noteAttachment = await noteAttachmentRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == attachmentId)
                .SingleOrDefaultAsync();

            return noteAttachment.MapTo<NoteAttachmentReadModel>(mapper);
        }

        public async Task<NoteAttachmentDto> UpdateNoteAttachment(long id, NoteAttachmentDto attachmentDto)
        {
            var attachment = await noteAttachmentRepository.Get(id);

            if (attachment == null)
            {
                throw new NotFoundException(typeof(Note), id);
            }
            attachmentDto.MapTo(attachment, mapper);

            noteAttachmentRepository.Update(attachment);

            await dbContext.SaveChanges();

            return attachment.MapTo<NoteAttachmentDto>(mapper);
        }

        public async Task<NoteAttachmentDto> CreateNoteAttachment( NoteAttachmentDto attachmentDto)
        {
            var attachment = attachmentDto.MapTo<NoteAttachment>(mapper);

            await noteAttachmentRepository.Insert(attachment);

            await dbContext.SaveChanges();

            return attachment.MapTo<NoteAttachmentDto>(mapper);
        }

        public async Task<IEnumerable<NoteReadModel>> GetQuotationNotes(long quotationId)
        {
            
            var notes = await noteRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.QuotationId == quotationId)
                .ToArrayAsync();
            return notes.MapTo<IEnumerable<NoteReadModel>>(mapper);
        }

        public async Task<IEnumerable<NoteReadModel>> GetOrderNotes(long orderId)
        {
            var notes = await noteRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.OrderId == orderId)
                .ToArrayAsync();
            return notes.MapTo<IEnumerable<NoteReadModel>>(mapper);
        }
    }
}
