using Collini.GestioneInterventi.Application.Activities.DTOs;
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
public class ActivitiesController : ColliniApiController
{
    public ActivitiesController()
    {
    }

    [HttpPost("activity")]
    public async Task<IActionResult> CreateActivity([FromBody] ActivityDto request)
    {
        return Ok(2);
    }
}