using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Application.Jobs.Services;
using Collini.GestioneInterventi.Application.Notes.DTOs;
using Collini.GestioneInterventi.Application.Orders.DTOs;
using Collini.GestioneInterventi.Application.Quotations.DTOs;
using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Framework.Exceptions;
using Collini.GestioneInterventi.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Collini.GestioneInterventi.Application.Quotations.Services
{
    public interface IQuotationService
    {

        IQueryable<QuotationDetailDto> GetQuotations();
        Task<QuotationDetailDto> GetQuotationDetail(long id);
        Task<QuotationDetailDto> CreateQuotation(QuotationDetailDto quotation);
        Task UpdateQuotation(long id, QuotationDetailDto quotation);
        Task<IEnumerable<QuotationReadModel>> getAllQuotations();


        Task<IEnumerable<QuotationAttachmentReadModel>> GetQuotationAttachments(long quotationId);
        Task<QuotationAttachmentReadModel> GetQuotationAttachmentDetail(long attachmentId);
        Task<QuotationAttachmentDto> UpdateQuotationAttachment(long id, QuotationAttachmentDto attachmentDto);
        Task<QuotationAttachmentDto> CreateQuotationAttachment(QuotationAttachmentDto attachmentDto);
        Task<QuotationAttachmentReadModel> DownloadQuotationAttachment(string filename);
    }

    public class QuotationService : IQuotationService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Quotation> quotationRepository;
        private readonly IRepository<QuotationAttachment> quotationAttachmentRepository;
        private readonly IJobService jobService;
        private readonly IColliniDbContext dbContext;

        public QuotationService(
            IMapper mapper,
            IRepository<Quotation> quotationRepository,
            IColliniDbContext dbContext, 
            IJobService jobService, IRepository<QuotationAttachment> quotationAttachmentRepository)
        {
            this.mapper = mapper;
            this.quotationRepository = quotationRepository;
            this.dbContext = dbContext;
            this.jobService = jobService;
            this.quotationAttachmentRepository = quotationAttachmentRepository;
        }

        public IQueryable<QuotationDetailDto> GetQuotations()
        {
            var quotations = quotationRepository
                .Query()
                .Where(x => x.Status == QuotationStatus.Pending || x.Status == QuotationStatus.Sent)
                .AsNoTracking()
                .Project<QuotationDetailDto>(mapper);

            //return quotations.MapTo<IEnumerable<QuotationDetailDto>>(mapper);
            return quotations;
        }

        public async Task<QuotationDetailDto> GetQuotationDetail(long id)
        {
            if (id == 0)
                throw new ApplicationException("Impossibile recuperare una quotation con id 0");

            var quotation = await quotationRepository
                .Query()
                .AsNoTracking()
                .Include(x => x.Job)
                .ThenInclude(y => y.Customer)
                .Include(x => x.Job)
                .ThenInclude(y => y.CustomerAddress)
                .Include(x=>x.Notes)
                .Include(x=>x.Attachment)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (quotation == null)
                throw new ApplicationException($"Impossibile trovare la quotation con id {id}");

            return quotation.MapTo<QuotationDetailDto>(mapper);
        }

        public async Task<QuotationDetailDto> CreateQuotation(QuotationDetailDto quotationDto)
        {
            var quotation = quotationDto.MapTo<Quotation>(mapper);
            await quotationRepository.Insert(quotation);

            if (quotationDto.AttachmentFileName != null && quotationDto.AttachmentDisplayName != null)
            {
                var quotationAttachment = quotationDto.MapTo<QuotationAttachment>(mapper);
                quotationAttachment.QuotationId = quotation.Id;
                await quotationAttachmentRepository.Insert(quotationAttachment);
            }

            var job = await jobService.GetJob(quotationDto.JobId);
            if (job == null)
                throw new ApplicationException("Job non trovato");
            if (job.Status == JobStatus.Pending)
                job.Status = JobStatus.Working;
            await jobService.UpdateJob(job.Id, job.MapTo<JobDetailDto>(mapper));

            await dbContext.SaveChanges();

            quotation.Job = await jobService.GetJob(quotationDto.JobId);

            return quotation.MapTo<QuotationDetailDto>(mapper);
        }

        public async Task UpdateQuotation(long id, QuotationDetailDto quotationDto)
        {


            if (id == 0)
                throw new ApplicationException("Impossibile aggiornare una quotation con id 0");

            var quotation= await quotationRepository
                .Query()
                .Where(x => x.Id == id)
                .Include(x=>x.Attachment)
                .SingleOrDefaultAsync();

            if (quotation == null)
                throw new ApplicationException($"Impossibile trovare una quotation con id {id}");
            
            quotationDto.MapTo(quotation, mapper);
           
            if (!string.IsNullOrEmpty(quotationDto.AttachmentFileName) && !string.IsNullOrEmpty(quotationDto.AttachmentDisplayName))
            {
                var quotationAttachment = quotation.Attachment;
                if (quotationAttachment == null)
                {
                    quotationAttachment = quotationDto.MapTo<QuotationAttachment>(mapper);
                    quotationAttachment.QuotationId = quotation.Id;
                    await quotationAttachmentRepository.Insert(quotationAttachment);
                    
                }
                else
                {
                    quotationAttachment.DisplayName = quotationDto.AttachmentDisplayName;
                    quotationAttachment.FileName = quotationDto.AttachmentFileName;
                }
            }
            
            if (quotation.Attachment != null && string.IsNullOrEmpty(quotationDto.AttachmentFileName) && string.IsNullOrEmpty(quotationDto.AttachmentDisplayName))
            {
                quotation.Attachment = null;
            }

            try
            {
                await dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<QuotationReadModel>> getAllQuotations()
        {
            var quotations = await quotationRepository
                .Query()
                .Include(x=>x.Job)
                .Include(x=>x.Notes)
                .Include(x=>x.Attachment)
                .AsNoTracking()
                .ToArrayAsync();

            return quotations.MapTo<IEnumerable<QuotationReadModel>>(mapper);
        }

       // --------------------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<QuotationAttachmentReadModel>> GetQuotationAttachments(long quotationId)
        {
            var quotationAttachments = await quotationAttachmentRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.QuotationId == quotationId)
                .OrderBy(x => x.CreatedOn)
                .ToArrayAsync();

            return quotationAttachments.MapTo<IEnumerable<QuotationAttachmentReadModel>>(mapper);
        }

        public async Task<QuotationAttachmentReadModel> GetQuotationAttachmentDetail(long attachmentId)
        {
            var quotationAttachment = await quotationAttachmentRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == attachmentId)
                .SingleOrDefaultAsync();

            return quotationAttachment.MapTo<QuotationAttachmentReadModel>(mapper);
        }

        public async Task<QuotationAttachmentReadModel> DownloadQuotationAttachment(string filename)
        {
            var quotationAttachment = await quotationAttachmentRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.FileName == filename)
                .SingleOrDefaultAsync();

            return quotationAttachment.MapTo<QuotationAttachmentReadModel>(mapper);
        }

        public async Task<QuotationAttachmentDto> UpdateQuotationAttachment(long id, QuotationAttachmentDto attachmentDto)
        {
            var attachment = await quotationAttachmentRepository.Get(id);

            if (attachment == null)
            {
                throw new NotFoundException(typeof(Note), id);
            }
            attachmentDto.MapTo(attachment, mapper);

            quotationAttachmentRepository.Update(attachment);

            await dbContext.SaveChanges();

            return attachment.MapTo<QuotationAttachmentDto>(mapper);
        }

        public async Task<QuotationAttachmentDto> CreateQuotationAttachment(QuotationAttachmentDto attachmentDto)
        {
            var attachment = attachmentDto.MapTo<QuotationAttachment>(mapper);

            await quotationAttachmentRepository.Insert(attachment);

            await dbContext.SaveChanges();

            return attachment.MapTo<QuotationAttachmentDto>(mapper);
        }



    }
}
