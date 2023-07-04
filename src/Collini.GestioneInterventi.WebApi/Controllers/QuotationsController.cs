using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Orders.Services;
using Collini.GestioneInterventi.Application.Quotations.DTOs;
using Collini.GestioneInterventi.Application.Quotations.Services;
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
    private readonly IQuotationService quotationService;
    public QuotationsController(IQuotationService quotationService)
    {
        this.quotationService = quotationService;
    }

    [HttpGet("quotations")]
    public async Task<DataSourceResult> GetQuotations([DataSourceRequest] DataSourceRequest request)
    {
        var quotations = (await quotationService.GetQuotations()).ToList();
        return await quotations.ToDataSourceResultAsync(request);
    }


    [HttpGet("quotation-detail/{id}")]
    public async Task<QuotationDetailDto> GetQuotationDetail(long quotationId)
    {
        var quotation = await quotationService.GetQuotationDetail(quotationId);
        return quotation;
    }

    [HttpPost("create-quotation")]
    public async Task<IActionResult> CreateQuotation([FromBody] QuotationDetailDto quotationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await quotationService.CreateQuotation(quotationDto);
        return Ok(quotationDto);
    }

    [HttpPut("update-quotation/{id}")]
    public async Task<IActionResult> UpdateQuotation(long id, [FromBody] QuotationDetailDto quotationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await quotationService.UpdateQuotation(id, quotationDto);
        return Ok(quotationDto);
    }

    [HttpGet("all-quotations")]
    public async Task<List<QuotationReadModel>> getAllQuotations()
    {
        List<QuotationReadModel> quotations = (await quotationService.getAllQuotations()).ToList();
        return quotations;
    }
}