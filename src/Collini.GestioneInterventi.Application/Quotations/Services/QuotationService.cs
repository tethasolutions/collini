﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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

        Task<IEnumerable<QuotationDetailDto>> GetQuotations();
        Task<QuotationDetailDto> GetQuotationDetail(long id);
        Task<QuotationDetailDto> CreateQuotation(QuotationDetailDto quotation);
        Task<QuotationDetailDto> UpdateQuotation(long id, QuotationDetailDto quotation);
        Task<IEnumerable<QuotationReadModel>> getAllQuotations();
    }

    public class QuotationService : IQuotationService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Quotation> quotationRepository;
        private readonly IColliniDbContext dbContext;

        public QuotationService(
            IMapper mapper,
            IRepository<Quotation> quotationRepository,
            IColliniDbContext dbContext)
        {
            this.mapper = mapper;
            this.quotationRepository = quotationRepository;
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<QuotationDetailDto>> GetQuotations()
        {
            var quotations = await quotationRepository
                .Query()
                .Include(x=>x.Job)
                .Include(x=>x.Notes)
                .AsNoTracking()
                .ToArrayAsync();

            return quotations.MapTo<IEnumerable<QuotationDetailDto>>(mapper);
        }

        public async Task<QuotationDetailDto> GetQuotationDetail(long id)
        {
            if (id == 0)
                throw new ApplicationException("Impossibile recuperare una quotation con id 0");

            var quotation = await quotationRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Job)
                .Include(x=>x.Notes)
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
            await dbContext.SaveChanges();
            return quotation.MapTo<QuotationDetailDto>(mapper);
        }

        public async Task<QuotationDetailDto> UpdateQuotation(long id, QuotationDetailDto quotationDto)
        {


            if (id == 0)
                throw new ApplicationException("Impossibile aggiornare una quotation con id 0");

            var quotation= await quotationRepository
                .Query()
                .AsNoTracking()
                //.Include(x=>x.Job)
                //.Include(x=>x.Notes)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();;

            if (quotation == null)
                throw new ApplicationException($"Impossibile trovare una quotation con id {id}");

            quotationDto.MapTo(quotation, mapper);
            quotationRepository.Update(quotation);
            await dbContext.SaveChanges();

            return quotation.MapTo<QuotationDetailDto>(mapper);

            
        }

        public async Task<IEnumerable<QuotationReadModel>> getAllQuotations()
        {
            var quotations = await quotationRepository
                .Query()
                .Include(x=>x.Job)
                .Include(x=>x.Notes)
                .AsNoTracking()
                .ToArrayAsync();

            return quotations.MapTo<IEnumerable<QuotationReadModel>>(mapper);
        }
    }
}