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

    [HttpGet("quotation-notes/{quotationId}")]
    public async Task<List<NoteReadModel>> GetQuotationNotes(long quotationId)
    {
        List<NoteReadModel> quotationNotes = new List<NoteReadModel>
        {
            new NoteReadModel
            {
                Id = 1,
                Value = "Nota 1",
                CreatedOn = new DateTime(2023, 6, 10),
                Operator = new ContactDto
                {
                    Id = 1,
                    Type = ContactType.Customer,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.PrivatePerson,
                    ErpCode = "ERP123",
                    Alert = false
                },
                Attachments = new List<NoteAttachmentReadModel>
                {
                    new NoteAttachmentReadModel
                    {
                        Id = 1,
                        DisplayName = "allegato 1",
                        FileName = "file1.png"
                    }
                }
            },
            new NoteReadModel
            {
                Id = 2,
                Value = "Nota 2",
                CreatedOn = new DateTime(2023, 6, 11),
                Operator = new ContactDto
                {
                    Id = 2,
                    Type = ContactType.Customer,
                    CompanyName = "Cannon",
                    Name = "John",
                    Surname = "Travolta",
                    FiscalType = ContactFiscalType.Company,
                    ErpCode = "ERP456",
                    Alert = true
                },
                Attachments = new List<NoteAttachmentReadModel>()
            },
            new NoteReadModel
            {
                Id = 3,
                Value = "Nota 3",
                CreatedOn = new DateTime(2023, 6, 12),
                Operator = new ContactDto
                {
                    Id = 3,
                    Type = ContactType.Customer,
                    CompanyName = "IDM",
                    Name = "Alex",
                    Surname = "Ronaldo",
                    FiscalType = ContactFiscalType.Building,
                    ErpCode = "ERP789",
                    Alert = true
                },
                Attachments = new List<NoteAttachmentReadModel>
                {
                    new NoteAttachmentReadModel
                    {
                        Id = 2,
                        DisplayName = "allegato 2",
                        FileName = "file2.png"
                    }
                }
            }
        };

        return quotationNotes;
    }

    [HttpGet("order-notes/{orderId}")]
    public async Task<List<NoteReadModel>> GetOrderNotes(long orderId)
    {
        List<NoteReadModel> orderNotes = new List<NoteReadModel>
        {
            new NoteReadModel
            {
                Id = 1,
                Value = "Nota 1",
                CreatedOn = new DateTime(2023, 6, 10),
                Operator = new ContactDto
                {
                    Id = 1,
                    Type = ContactType.Customer,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.PrivatePerson,
                    ErpCode = "ERP123",
                    Alert = false
                },
                Attachments = new List<NoteAttachmentReadModel>
                {
                    new NoteAttachmentReadModel
                    {
                        Id = 1,
                        DisplayName = "allegato 1",
                        FileName = "file1.png"
                    }
                }
            },
            new NoteReadModel
            {
                Id = 2,
                Value = "Nota 2",
                CreatedOn = new DateTime(2023, 6, 11),
                Operator = new ContactDto
                {
                    Id = 2,
                    Type = ContactType.Customer,
                    CompanyName = "Cannon",
                    Name = "John",
                    Surname = "Travolta",
                    FiscalType = ContactFiscalType.Company,
                    ErpCode = "ERP456",
                    Alert = true
                },
                Attachments = new List<NoteAttachmentReadModel>()
            },
            new NoteReadModel
            {
                Id = 3,
                Value = "Nota 3",
                CreatedOn = new DateTime(2023, 6, 12),
                Operator = new ContactDto
                {
                    Id = 3,
                    Type = ContactType.Customer,
                    CompanyName = "IDM",
                    Name = "Alex",
                    Surname = "Ronaldo",
                    FiscalType = ContactFiscalType.Building,
                    ErpCode = "ERP789",
                    Alert = true
                },
                Attachments = new List<NoteAttachmentReadModel>
                {
                    new NoteAttachmentReadModel
                    {
                        Id = 2,
                        DisplayName = "allegato 2",
                        FileName = "file2.png"
                    }
                }
            }
        };

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