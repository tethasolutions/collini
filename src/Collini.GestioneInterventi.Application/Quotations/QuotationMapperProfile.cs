using AutoMapper;
using Collini.GestioneInterventi.Application.Quotations.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Framework.Extensions;

namespace Collini.GestioneInterventi.Application.Quotations
{
    public class QuotationMapperProfile:Profile
    {
        public QuotationMapperProfile()
        {

            CreateMap<Quotation, QuotationDetailDto>()
                .MapMember(x => x.JobCode, y => y.Job.Number.ToString() + "/" + y.Job.Year.ToString())
                .MapMember(x => x.JobDescription, y => y.Job.Description + ((y.Job.ResultNote != null) ? " - " + y.Job.ResultNote : ""))
                .MapMember(x => x.JobDate, y => y.Job.CreatedOn)
                .MapMember(x => x.CustomerName, y => y.Job.Customer.CompanyName + " " + y.Job.Customer.Surname + " " + y.Job.Customer.Name +
                    ((y.Job.CustomerAddress != null) ? " - " + y.Job.CustomerAddress.StreetAddress + " " + y.Job.CustomerAddress.City : ""))
                .MapMember(x => x.CustomerContacts, y => ((y.Job.Customer.Telephone != null) ? "Tel: " + y.Job.Customer.Telephone : "") + ((y.Job.Customer.Email != null) ? " - Email: " + y.Job.Customer.Email : ""))
                .MapMember(x => x.HasNotes, y => y.Job.Notes.Count > 0 ? y.Job.Notes.FirstOrDefault().Attachments.Count > 0 : false)
                .MapMember(x => x.ActivityStart, y => y.Job.Activities.OrderByDescending(z => z.Start).FirstOrDefault().Start)
                .MapMember(x => x.ActivityEnd, y => y.Job.Activities.OrderByDescending(z => z.End).FirstOrDefault().End)
                .MapMember(x => x.ActivityOperator, y => y.Job.Activities.OrderByDescending(z => z.End).FirstOrDefault().Operator.Name + " " + y.Job.Activities.OrderByDescending(z => z.End).FirstOrDefault().Operator.Surname);

            CreateMap<QuotationDetailDto, Quotation>()
                .Ignore(x=>x.StatusChangedOn)
                .Ignore(x=>x.Job)
                .Ignore(x=>x.Notes)
                .Ignore(x=>x.Attachment)
                .IgnoreCommonMembers();

            CreateMap<Quotation, QuotationReadModel>()
                .MapMember(x => x.JobCode, y => y.Job.Number.ToString() + "/" + y.Job.Year.ToString())
                .MapMember(x => x.JobDate, y => y.Job.JobDate)
                .MapMember(x => x.JobDescription, y => y.Job.Description + ((y.Job.ResultNote != null) ? " - " + y.Job.ResultNote : ""))
                .MapMember(x => x.CustomerName, y => y.Job.Customer.CompanyName + " " + y.Job.Customer.Surname + " " + y.Job.Customer.Name +
                    ((y.Job.CustomerAddress != null) ? " - " + y.Job.CustomerAddress.StreetAddress + " " + y.Job.CustomerAddress.City : ""))
                .MapMember(x => x.CustomerContacts, y => ((y.Job.Customer.Telephone != null) ? "Tel: " + y.Job.Customer.Telephone : "") + ((y.Job.Customer.Email != null) ? " - Email: " + y.Job.Customer.Email : ""))
                .MapMember(x => x.ActivityStart, y => y.Job.Activities.OrderByDescending(z => z.Start).FirstOrDefault().Start)
                .MapMember(x => x.ActivityEnd, y => y.Job.Activities.OrderByDescending(z => z.End).FirstOrDefault().End)
                .MapMember(x => x.ActivityOperator, y => y.Job.Activities.OrderByDescending(z => z.End).FirstOrDefault().Operator.Name + " " + y.Job.Activities.OrderByDescending(z => z.End).FirstOrDefault().Operator.Surname);
            
            CreateMap<QuotationDetailDto, QuotationAttachment>()
                .Ignore(x=>x.Quotation)
                .Ignore(x=>x.QuotationId)
                .Ignore(x=>x.Id)
                .MapMember(x => x.FileName, y => y.AttachmentFileName)
                .MapMember(x => x.DisplayName, y => y.AttachmentDisplayName)
                .IgnoreCommonMembers();

            CreateMap<QuotationAttachment, QuotationAttachmentReadModel>();

        }
    }
}
