﻿using AutoMapper;
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
        Task<List<NoteAttachmentReadModel>> GetNoteAttachments(long noteId);
        Task<NoteAttachmentReadModel> GetNoteAttachmentDetail(long id);
        Task<NoteAttachmentDto> UpdateNoteAttachment(long id, NoteAttachmentDto attachmentDto); 
        Task<NoteAttachmentDto> CreateNoteAttachment(NoteAttachmentDto attachmentDto);
    }

    public class NoteService
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
            var notes = noteRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.JobId == jobId)
                .ToArrayAsync();
            return notes.MapTo<IEnumerable<NoteReadModel>>(mapper);
        }

        public async Task<IEnumerable<NoteReadModel>> GetActivityNotes(long activityId)
        {
            var notes = noteRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.ActivityId == activityId)
                .ToArrayAsync();
            return notes.MapTo<IEnumerable<NoteReadModel>>(mapper);
        }

        public async Task<NoteReadModel> GetNoteDetail(long noteId)
        {
            var note = noteRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == noteId)
                .SingleOrDefaultAsync();

            return note.MapTo<NoteReadModel>(mapper);
        }

        public async Task<NoteDto> UpdateNote(long id, NoteDto noteDto)
        {
            var note = await noteRepository.Get(id);

            if (note == null)
            {
                throw new NotFoundException(typeof(Note), id);
            }
            noteDto.MapTo(note, mapper);

            await dbContext.SaveChanges();

            return note.MapTo<NoteDto>(mapper);
        }

        public async Task<NoteDto> CreateNote(NoteDto noteDto)
        {
            var note = noteDto.MapTo<Note>(mapper);

            await noteRepository.Insert(note);

            await dbContext.SaveChanges();

            return note.MapTo<NoteDto>(mapper);
        }

        public async Task<IEnumerable<NoteAttachmentReadModel>> GetNoteAttachments(long noteId)
        {
            var noteAttachments = noteRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == noteId)
                .Select(y => y.Attachments)
                .ToArrayAsync();

            return noteAttachments.MapTo<IEnumerable<NoteAttachmentReadModel>>(mapper);
        }

        public async Task<NoteAttachmentReadModel> GetNoteAttachmentDetail(long attachmentId)
        {
            var noteAttachment = noteAttachmentRepository
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
    }
}