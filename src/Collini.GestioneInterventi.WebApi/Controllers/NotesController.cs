using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Application.Security;
using Collini.GestioneInterventi.Application.Security.DTOs;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.WebApi.Auth;
using Collini.GestioneInterventi.WebApi.Models.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using Collini.GestioneInterventi.Application.Notes.DTOs;
using Collini.GestioneInterventi.Application.Notes.Services;
using Collini.GestioneInterventi.Application.Activities.Services;
using Collini.GestioneInterventi.Domain.Docs;

namespace Collini.GestioneInterventi.WebApi.Controllers;

[RequireUser]
public class NotesController : ColliniApiController
{
    private readonly INotesService noteService;
    public NotesController(INotesService noteService)
    {
        this.noteService = noteService;
    }

    [HttpGet("job-notes/{jobId}")]
    public async Task<List<NoteReadModel>> GetJobNotes(long jobId)
    {
        List<NoteReadModel> jobNotes = (await noteService.GetJobNotes(jobId)).ToList();
        return jobNotes;
    }

    [HttpGet("activity-notes/{activityId}")]
    public async Task<List<NoteReadModel>> GetActivityNotes(long activityId)
    {
        List<NoteReadModel> activityNotes = (await noteService.GetActivityNotes(activityId)).ToList();
        return activityNotes;
    }

    [HttpGet("note-detail/{noteId}")]
    public async Task<NoteReadModel> GetNoteDetail(long noteId)
    {
        NoteReadModel noteDetail = await noteService.GetNoteDetail(noteId);
        return noteDetail;
    }

    [HttpPut("note/{id}")]
    public async Task<IActionResult> UpdateNote(long id, [FromBody] NoteDto noteDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await noteService.UpdateNote(id,noteDto);
        return Ok(noteDto);
    }

    [HttpPost("note")]
    public async Task<IActionResult> CreateNote([FromBody] NoteDto noteDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await noteService.CreateNote(noteDto);
        return Ok(noteDto);
    }

    [HttpGet("notes-attachments/{noteId}")]
    public async Task<List<NoteAttachmentReadModel>> GetNoteAttachments(long noteId)
    {
        List<NoteAttachmentReadModel> attachments = await noteService.GetNoteAttachments(noteId);
        return attachments;
    }

    [HttpGet("note-attachment-detail/{id}")]
    public async Task<NoteAttachmentReadModel> GetNoteAttachmentDetail(long attachmentId)
    {
        NoteAttachmentReadModel attachment = await noteService.GetNoteAttachmentDetail(attachmentId);
        
        return attachment;
    }

    [HttpPut("note-attachment/{id}")]
    public async Task<IActionResult> UpdateNoteAttachment(long id, [FromBody] NoteAttachmentDto attachmentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await noteService.UpdateNoteAttachment(id, attachmentDto);
        return Ok(attachmentDto);
    }

    [HttpPost("note-attachment")]
    public async Task<IActionResult> CreateNoteAttachment([FromBody] NoteAttachmentDto attachmentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await noteService.CreateNoteAttachment(attachmentDto);
        return Ok(attachmentDto);
    }
}