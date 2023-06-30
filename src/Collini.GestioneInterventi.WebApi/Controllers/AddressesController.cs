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
            Id = 8,
            ContactId = 3,
            City = "Toscana",
            StreetAddress = "corso Milano 12",
            Province = "TS",
            ZipCode = "23084",
            Telephone = "+393883504629",
            Email = "Alex_Ronaldo@gmail.com",
            IsMainAddress = false
        };

        return address;
    }

    [HttpPut("address/{id}")]
    public async Task<IActionResult> UpdateAddress(long id, [FromBody] AddressDto request)
    {
        return NoContent();
    }

    [HttpDelete("address/{id}")]
    public async Task<IActionResult> DeleteAddress(long id)
    {
        return NoContent();
    }

    [HttpPost("address")]
    public async Task<IActionResult> CreateAddress([FromBody] AddressDto request)
    {
        return Ok(5);
    }

    [HttpPut("set-address-as-main/{id}")]
    public async Task<IActionResult> SetAddressAsMain(long id)
    {
        return NoContent();
    }
}