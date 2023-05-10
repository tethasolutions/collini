using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Security;
using Collini.GestioneInterventi.Application.Security.DTOs;
using Collini.GestioneInterventi.WebApi.Auth;
using Collini.GestioneInterventi.WebApi.Models.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collini.GestioneInterventi.WebApi.Controllers;

[RequireUser]
public class AddressesController : ColliniApiController
{
    public AddressesController()
    {
    }

    [HttpGet("address/{id}")]
    public async Task<AddressDto> GetAddress(long id)
    {
        var address = new AddressDto
        {
            AddressId = 8,
            CustomerId = 3,
            City = "Toscana",
            Address = "corso Milano 12",
            Province = "TS",
            ZipCode = "23084",
            MainAddress = false
        };

        return address;
    }

    [HttpPut("address/{id}")]
    public async Task<IActionResult> UpdateAddress(long id, AddressDto request)
    {
        return NoContent();
    }

    [HttpDelete("address/{id}")]
    public async Task<IActionResult> DeleteAddress(long id)
    {
        return NoContent();
    }

    [HttpPost("address")]
    public async Task<IActionResult> CreateAddress(AddressDto request)
    {
        return NoContent();
    }
}