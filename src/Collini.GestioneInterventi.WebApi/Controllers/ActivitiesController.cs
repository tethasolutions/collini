using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Application.Activities.Services;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Application.Jobs.Services;
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
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Collini.GestioneInterventi.WebApi.Controllers;

[RequireUser]
public class ActivitiesController : ColliniApiController
{
    private readonly IActivityService activityService;
    public ActivitiesController(IActivityService activityService)
    {
        this.activityService = activityService;
    }

    [HttpGet("activities")]
    public async Task<DataSourceResult> GetActivities([DataSourceRequest] DataSourceRequest request)
    {
        var quotations = activityService.GetActivities();
        return await quotations.ToDataSourceResultAsync(request);
    }

    [HttpGet("activity/{id}")]
    public async Task<ActivityViewModel> GetActivity(long id)
    {
        var job = await activityService.GetActivity(id);
        return job;
    }

    [HttpPost("activity")]
    public async Task<IActionResult> CreateActivity([FromBody] ActivityDto activityDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await activityService.CreateActivity(activityDto);
        return Ok(activityDto);
    }

   
    [HttpPut("activity/{id}")]
    public async Task<IActionResult> UpdateActivity(long id, [FromBody] ActivityDto activityDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await activityService.UpdateActivity(id, activityDto);
        return Ok();
    }

    [HttpGet("calendar")]
    public async Task<CalendarViewModel> GetCalendar()
    {
        var calendar = await activityService.GetCalendar();
        return calendar;
    }

   
}