﻿using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Customers.Services;
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
    private readonly IAddressService addressService;

    public AddressesController(
        IAddressService addressService)
    {
        this.addressService = addressService;
    }
    [HttpGet("address/customer/{id}")]
    public async Task<ActionResult<AddressDto>> GetCustomerAddresses(long id)
    {
        var addresses = await addressService.GetCustomerAddresses(id);

        return Ok(addresses);
    }

    [HttpGet("address/{id}")]
    public async Task<ActionResult<AddressDto>> GetAddress(long id)
    {
        var address = await addressService.GetAddress(id);

        return Ok(address);
    }

    [HttpPut("address/{id}")]
    public async Task<IActionResult> UpdateAddress(long id, [FromBody] AddressDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var address = await addressService.UpdateAddress(id, request);

        return Ok(address);
    }

    [HttpDelete("address/{id}")]
    public async Task<IActionResult> DeleteAddress(long id)
    {
        await addressService.DeleteAddress(id);

        return Ok();
    }

    [HttpPost("address")]
    public async Task<IActionResult> CreateAddress([FromBody] AddressDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var address = await addressService.CreateAddress(request);

        return Ok(address);
    }

    [HttpPut("set-address-as-main/{id}")]
    public async Task<IActionResult> SetAddressAsMain(long id)
    {
        var address = await addressService.SetMainAddress(id);

        return Ok(address);
    }
}