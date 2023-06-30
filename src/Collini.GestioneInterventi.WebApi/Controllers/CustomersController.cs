using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Customers.Services;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.WebApi.Auth;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace Collini.GestioneInterventi.WebApi.Controllers;

[RequireUser]
public class CustomersController : ColliniApiController
{
    private readonly IContactService contactService;

    public CustomersController(
        IContactService contactService)
    {
        this.contactService = contactService;
    }

    [HttpGet("customers")]
    public async Task<ActionResult<DataSourceResult>> GetCustomers([DataSourceRequest] DataSourceRequest request)
    {
        var contacts = await contactService.GetContacts(ContactType.Customer);
        return Ok(await contacts.ToDataSourceResultAsync(request));
    }

    [HttpGet("providers")]
    public async Task<ActionResult<DataSourceResult>> GetProviders([DataSourceRequest] DataSourceRequest request)
    {
        var contacts = await contactService.GetContacts(ContactType.Supplier);
        return Ok(await contacts.ToDataSourceResultAsync(request));
    }

    [HttpGet("customer/{id}")]
    public async Task<ActionResult<ContactDto>> GetCustomer(long id)
    {
        var contact = await contactService.GetContact(id);
        return Ok(contact);
    }

    [HttpPut("customer/{id}")]
    public async Task<IActionResult> UpdateCustomer(long id, ContactDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await contactService.UpdateContact(id, request);
        return Ok();
    }

    [HttpDelete("customer/{id}")]
    public async Task<IActionResult> DeleteCustomer(long id)
    {
        await contactService.DeleteContact(id);
        return Ok();
    }

    [HttpPost("customer")]
    public async Task<IActionResult> CreateCustomer(ContactDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var contact = await contactService.CreateContact(request);
        return Ok(contact);
    }
}