using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Quotations.DTOs;
using Collini.GestioneInterventi.Application.Security;
using Collini.GestioneInterventi.Application.Security.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.WebApi.Auth;
using Collini.GestioneInterventi.WebApi.Models.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Collini.GestioneInterventi.WebApi.Controllers;

[RequireUser]
public class QuotationsController : ColliniApiController
{
    public QuotationsController()
    {
    }

    [HttpGet("quotations")]
    public async Task<DataSourceResult> GetQuotations([DataSourceRequest] DataSourceRequest request)
    {
        List<QuotationReadModel> quotations = new List<QuotationReadModel>
        {
            new QuotationReadModel
            {
                Id = 1,
                ExpirationDate = new DateTime(2023, 7, 17),
                Amount = 120,
                JobCode = "1/2023",
                CustomerName = "Cliente 1",
                JobDescription = "descrizione commessa test",
                Status = QuotationStatus.Pending
            },
            new QuotationReadModel
            {
                Id = 2,
                ExpirationDate = new DateTime(2023, 6, 17),
                Amount = 120,
                JobCode = "2/2023",
                CustomerName = "Cliente 1",
                JobDescription = "descrizione commessa test",
                Status = QuotationStatus.Refused
            },
            new QuotationReadModel
            {
                Id = 3,
                ExpirationDate = new DateTime(2023, 5, 17),
                Amount = 100,
                JobCode = "3/2023",
                CustomerName = "Cliente 1",
                JobDescription = "descrizione commessa test",
                Status = QuotationStatus.Accepted
            }
        };

        DataSourceResult result = new DataSourceResult
        {
            AggregateResults = null,
            Errors = null,
            Total = 3,
            Data = quotations
        };

        return result;
    }


    [HttpGet("quotation-detail/{id}")]
    public async Task<QuotationDetailDto> GetQuotationDetail(long id)
    {
        QuotationDetailDto quotation = new QuotationDetailDto
        {
            Id = 1,
            ExpirationDate = new DateTime(2023, 8, 16),
            Amount = 100,
            JobCode = "1/2023",
            CustomerName = "Cliente 1",
            JobDescription = "descrizione commessa test",
            Status = QuotationStatus.Pending,
        };

        return quotation;
    }

    [HttpPost("create-quotation")]
    public async Task<IActionResult> CreateQuotation([FromBody] QuotationDetailDto quotation)
    {
        return NoContent();
    }

    [HttpPut("update-quotation/{id}")]
    public async Task<IActionResult> UpdateQuotation(long id, [FromBody] QuotationDetailDto quotation)
    {
        return NoContent();
    }

    [HttpGet("all-quotations")]
    public async Task<List<QuotationReadModel>> getAllQuotations()
    {
        List<QuotationReadModel> quotations = new List<QuotationReadModel>
        {
            new QuotationReadModel
            {
                Id = 1,
                ExpirationDate = new DateTime(2023, 7, 17),
                Amount = 120,
                Status = QuotationStatus.Pending
            },
            new QuotationReadModel
            {
                Id = 2,
                ExpirationDate = new DateTime(2023, 6, 17),
                Amount = 120,
                Status = QuotationStatus.Refused
            },
            new QuotationReadModel
            {
                Id = 3,
                ExpirationDate = new DateTime(2023, 5, 17),
                Amount = 100,
                Status = QuotationStatus.Accepted
            }
        };

        return quotations;
    }
}