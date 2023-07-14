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
using System.IO;
using Collini.GestioneInterventi.Framework.Configuration;

namespace Collini.GestioneInterventi.WebApi.Controllers;

[RequireUser]
public class NotesController : ColliniApiController
{
    private readonly INotesService noteService;
    private readonly IColliniConfiguration configuration;

    public NotesController(INotesService noteService, IColliniConfiguration configuration)
    {
        this.noteService = noteService;
        this.configuration = configuration;
    }

    [HttpGet("job-notes/{jobId}")]
    public async Task<List<NoteReadModel>> GetJobNotes(long jobId)
    {
        List<NoteReadModel> jobNotes = (await noteService.GetJobNotes(jobId)).ToList();
        return jobNotes;
    }

    [HttpGet("quotation-notes/{quotationId}")]
    public async Task<List<NoteReadModel>> GetQuotationNotes(long quotationId)
    {
        List<NoteReadModel> quotationNotes = (await noteService.GetQuotationNotes(quotationId)).ToList();

        return quotationNotes;
    }

    [HttpGet("order-notes/{orderId}")]
    public async Task<List<NoteReadModel>> GetOrderNotes(long orderId)
    {
        List<NoteReadModel> orderNotes = (await noteService.GetOrderNotes(orderId)).ToList();

        return orderNotes;
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
        List<NoteAttachmentReadModel> attachments = (await noteService.GetNoteAttachments(noteId)).ToList();
        return attachments;
    }

    [HttpGet("note-attachment-detail/{id}")]
    public async Task<NoteAttachmentReadModel> GetNoteAttachmentDetail(long id)
    {
        NoteAttachmentReadModel attachment = await noteService.GetNoteAttachmentDetail(id);
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

    [HttpPost("note-attachment/upload-file")]
    public async Task<IActionResult> UploadFile()
    {
        var file = Request.Form.Files.FirstOrDefault();
        if (file == null)
        {
            return BadRequest();
        }
        var fileName = await SaveFile(file);
        return Ok(new
        {
            fileName,
            originalFileName = Path.GetFileName(file.FileName)
        });
    }

    private async Task<string> SaveFile(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName);
        var fileName = Guid.NewGuid() + extension;
        var folder = configuration.AttachmentsPath;
        Directory.CreateDirectory(folder);
        var path = Path.Combine(folder, fileName);
        await using (var stream = file.OpenReadStream())
        {
            await using (var fileStream = System.IO.File.OpenWrite(path))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
        return fileName;
    }

}