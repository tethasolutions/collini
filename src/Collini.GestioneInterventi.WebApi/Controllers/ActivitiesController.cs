using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Application.Customers.DTOs;
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
    public ActivitiesController()
    {
    }

    [HttpGet("calendar")]
    public async Task<CalendarViewModel> GetCalendar()
    {
        var calendar = new CalendarViewModel
        {
            Activities = new List<ActivityViewModel>
            {
                new ActivityViewModel
                {
                    Id = 1,
                    OperatorId = 1,
                    Start = new DateTime(2023, 6, 20, 10, 25, 0),
                    End = new DateTime(2023, 6, 20, 14, 50, 0),
                    Status = ActivityStatus.Planned,
                    Customer = "Cliente 1",
                    Job = "Job 1"
                },
                new ActivityViewModel
                {
                    Id = 2,
                    OperatorId = 2,
                    Start = new DateTime(2023, 6, 21, 10, 25, 0),
                    End = new DateTime(2023, 6, 21, 14, 50, 0),
                    Status = ActivityStatus.Planned,
                    Customer = "Cliente 2",
                    Job = "Job 2"
                },
                new ActivityViewModel
                {
                    Id = 3,
                    OperatorId = 3,
                    Start = new DateTime(2023, 6, 22, 10, 25, 0),
                    End = new DateTime(2023, 6, 22, 14, 50, 0),
                    Status = ActivityStatus.Planned,
                    Customer = "Cliente 3",
                    Job = "Job 3"
                },
                new ActivityViewModel
                {
                    Id = 4,
                    OperatorId = 4,
                    Start = new DateTime(2023, 6, 23, 10, 25, 0),
                    End = new DateTime(2023, 6, 23, 14, 50, 0),
                    Status = ActivityStatus.Planned,
                    Customer = "Cliente 4",
                    Job = "Job 4"
                },
                new ActivityViewModel
                {
                    Id = 5,
                    OperatorId = 5,
                    Start = new DateTime(2023, 6, 24, 10, 25, 0),
                    End = new DateTime(2023, 6, 24, 14, 50, 0),
                    Status = ActivityStatus.Planned,
                    Customer = "Cliente 5",
                    Job = "Job 5"
                }
            },
            Resources = new List<CalendarResourceViewModel>
            {
                new CalendarResourceViewModel
                {
                    Id = 1,
                    Description = "Mario Rossi",
                    Color = "red"
                },
                new CalendarResourceViewModel
                {
                    Id = 2,
                    Description = "Fernando Maroni",
                    Color = "blue"
                },
                new CalendarResourceViewModel
                {
                    Id = 3,
                    Description = "Adriano Valtemara",
                    Color = "green"
                },
                new CalendarResourceViewModel
                {
                    Id = 4,
                    Description = "Massimiliano Donati",
                    Color = "yellow"
                },
                new CalendarResourceViewModel
                {
                    Id = 5,
                    Description = "Luca Galbusera",
                    Color = "pink"
                }
            }
        };

        return calendar;
    }

    [HttpPost("activity")]
    public async Task<IActionResult> CreateActivity([FromBody] ActivityDto request)
    {
        return Ok(2);
    }
}