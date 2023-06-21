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
                Type = "Commessa",
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
                Type = "Intervento",
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
                Type = "Commessa",
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
                Type = "Commessa",
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
                Type = "Intervento",
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
                Type = "Commessa",
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
}