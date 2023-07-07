using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collini.GestioneInterventi.Application.Notes.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Framework.Extensions;

namespace Collini.GestioneInterventi.Application.Notes
{
    public class NoteMappingProfile : Profile
    {
        public NoteMappingProfile()
        {
            CreateMap<Note, NoteReadModel>()
                .Ignore(x=>x.OperatorId)
                .Ignore(x=>x.Operator);
            
            CreateMap<Note, NoteDto>()
                .Ignore(x=>x.OperatorId);

            CreateMap<NoteDto, Note>()
                .Ignore(x=>x.Job)
                .Ignore(x=>x.Order)
                .Ignore(x=>x.Quotation)
                .Ignore(x=>x.Activity)
                .IgnoreCommonMembersWithoutCreatedOn();

            CreateMap<NoteAttachment, NoteAttachmentReadModel>();
            CreateMap<NoteAttachmentReadModel, NoteAttachment>()
                .Ignore(x=>x.Note)
                .Ignore(x => x.NoteId)
                .IgnoreCommonMembers();


            CreateMap<NoteAttachment, NoteAttachmentDto>();
            CreateMap<NoteAttachmentDto, NoteAttachment>()
                .Ignore(x=>x.Note)
                .IgnoreCommonMembers();

        }
    }
}
