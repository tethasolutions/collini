using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Orders.Services;
using Collini.GestioneInterventi.Application.Quotations.DTOs;
using Collini.GestioneInterventi.Application.Quotations.Services;
using Collini.GestioneInterventi.Application.Security;
using Collini.GestioneInterventi.Application.Security.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.Framework.Configuration;
using Collini.GestioneInterventi.Framework.IO;
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
    private readonly IColliniConfiguration configuration;
    private readonly IMimeTypeProvider mimeTypeProvider;

    public QuotationsController(IQuotationService quotationService, IColliniConfiguration configuration, IMimeTypeProvider mimeTypeProvider)
    {
        this.quotationService = quotationService;
        this.configuration = configuration;
        this.mimeTypeProvider = mimeTypeProvider;
    }

    [HttpGet("quotations")]
    public async Task<DataSourceResult> GetQuotations([DataSourceRequest] DataSourceRequest request)
    {
        var quotations = (quotationService.GetQuotations());
        return await quotations.ToDataSourceResultAsync(request);
    }


    [HttpGet("quotation-detail/{id}")]
    public async Task<QuotationDetailDto> GetQuotationDetail(long id)
    {
        var quotation = await quotationService.GetQuotationDetail(id);
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
        return Ok();
    }

    [HttpGet("all-quotations")]
    public async Task<List<QuotationReadModel>> getAllQuotations()
    {
        List<QuotationReadModel> quotations = (await quotationService.getAllQuotations()).ToList();
        return quotations;
    }



    [HttpGet("attachment-detail/{id}")]
    public async Task<QuotationAttachmentReadModel> GetAttachmentDetail(long id)
    {
        var quotation = await quotationService.GetQuotationAttachmentDetail(id);
        return quotation;
    }

    [HttpPost("create-attachment")]
    public async Task<IActionResult> CreateQuotationAttachment([FromBody] QuotationAttachmentDto quotationAttachmentDtoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await quotationService.CreateQuotationAttachment(quotationAttachmentDtoDto);
        return Ok(quotationAttachmentDtoDto);
    }

    [HttpPut("update-attachment/{id}")]
    public async Task<IActionResult> UpdateQuotationAttachment(long id, [FromBody] QuotationAttachmentDto quotationAttachmentDtoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await quotationService.UpdateQuotationAttachment(id, quotationAttachmentDtoDto);
        return Ok();
    }

    [HttpGet("/{id}/all-attachments")]
    public async Task<List<QuotationAttachmentReadModel>> GetQuotationAttachments(long id)
    {
        List<QuotationAttachmentReadModel> quotationsAttachment = (await quotationService.GetQuotationAttachments(id)).ToList();
        return quotationsAttachment;
    }

    [AllowAnonymous]
    [HttpGet("quotation-attachment/download-file/{fileName}")]
    public async Task<FileResult> DownloadAttachment(string fileName)
    {
        fileName = Uri.UnescapeDataString(fileName);
        QuotationAttachmentReadModel quotationAttachment = (await quotationService.DownloadQuotationAttachment(fileName));
        
        var folder = configuration.AttachmentsPath;
        Directory.CreateDirectory(folder);
        var path = Path.Combine(folder, fileName);

        Stream stream = System.IO.File.OpenRead(path);
        return File(stream, mimeTypeProvider.Provide(fileName), quotationAttachment.DisplayName);
    }

    [HttpPost("quotation-attachment/upload-file")]
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

    [HttpPost("quotation-attachment/remove-file")]
    public async Task<IActionResult> DeleteFile()
    {
        return Ok();
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