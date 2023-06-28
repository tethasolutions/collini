using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Application.Note.DTOs;
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

namespace Collini.GestioneInterventi.WebApi.Controllers;

[RequireUser]
public class NotesController : ColliniApiController
{
    public NotesController()
    {
    }

    [HttpGet("job-notes/{jobId}")]
    public async Task<List<NoteReadModel>> GetJobNotes(long id)
    {
        List<NoteReadModel> jobNotes = new List<NoteReadModel>
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

        return jobNotes;
    }

    [HttpGet("activity-notes/{activityId}")]
    public async Task<List<NoteReadModel>> GetActivityNotes(long activityId)
    {
        List<NoteReadModel> activityNotes = new List<NoteReadModel>
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

        return activityNotes;
    }

    [HttpGet("note-detail/{noteId}")]
    public async Task<NoteReadModel> GetNoteDetail(long noteId)
    {
        NoteReadModel noteDetail = new NoteReadModel
        {
            Id = 1,
            Value = "Nota 1",
            CreatedOn = new DateTime(2023, 6, 10),
            OperatorId = 1,
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
                },
                new NoteAttachmentReadModel
                {
                    Id = 2,
                    DisplayName = "allegato 2",
                    FileName = "file2.png"
                },
                new NoteAttachmentReadModel
                {
                    Id = 3,
                    DisplayName = "allegato 3",
                    FileName = "file3.png"
                }
            }
        };

        return noteDetail;
    }

    [HttpPut("note/{id}")]
    public async Task<IActionResult> UpdateNote(long id, [FromBody] NoteDto request)
    {
        return NoContent();
    }

    [HttpPost("note")]
    public async Task<IActionResult> CreateNote([FromBody] NoteDto request)
    {
        return Ok(2);
    }

    [HttpGet("notes-attachments/{noteId}")]
    public async Task<List<NoteAttachmentReadModel>> GetNoteAttachments(long noteId)
    {
        List<NoteAttachmentReadModel> attachments = new List<NoteAttachmentReadModel>
        {
            new NoteAttachmentReadModel
            {
                Id = 1,
                DisplayName = "allegato 1",
                FileName = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQdba8xL_9XnvWHLChJys72Kpk-QdCAo0lqPzJ8avvAwiubsfAYvSNSyd04tNUOOTge-9U&usqp=CAU"
            },
            new NoteAttachmentReadModel
            {
                Id = 2,
                DisplayName = "allegato 2",
                FileName = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR205noDjp2Tqv9_Srpri1cI-Ikl0MtdoH8q-6eML-z2m6dIFgK9eczVrEx1CCFdHkSKJ8&usqp=CAU"
            },
            new NoteAttachmentReadModel
            {
                Id = 3,
                DisplayName = "allegato 3",
                FileName = "https://goods-photos.static1-sima-land.com/items/2400734/0/400.jpg?v=1602131867"
            }
        };

        return attachments;
    }

    [HttpGet("note-attachment-detail/{id}")]
    public async Task<NoteAttachmentReadModel> GetNoteAttachmentDetail(long id)
    {
        NoteAttachmentReadModel attachment = new NoteAttachmentReadModel
        {
            Id = 1,
            DisplayName = "allegato 1",
            FileName = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQdba8xL_9XnvWHLChJys72Kpk-QdCAo0lqPzJ8avvAwiubsfAYvSNSyd04tNUOOTge-9U&usqp=CAU"
        };

        return attachment;
    }


    [HttpPut("note-attachment/{id}")]
    public async Task<IActionResult> UpdateNoteAttachment(long id, [FromBody] NoteAttachmentDto request)
    {
        return NoContent();
    }

    [HttpPost("note-attachment")]
    public async Task<IActionResult> CreateNoteAttachment([FromBody] NoteAttachmentDto request)
    {
        return Ok(2);
    }
}