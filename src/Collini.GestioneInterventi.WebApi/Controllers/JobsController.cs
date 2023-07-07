﻿using Azure.Core;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Customers.Services;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Application.Jobs.Services;
using Collini.GestioneInterventi.Application.Security;
using Collini.GestioneInterventi.Application.Security.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.Framework.Extensions;
using Collini.GestioneInterventi.WebApi.Auth;
using Collini.GestioneInterventi.WebApi.Models.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Collini.GestioneInterventi.WebApi.Controllers;

[RequireUser]
public class JobsController : ColliniApiController
{
    private readonly IJobService jobsService;

    public JobsController(IJobService jobsService)
    {
        this.jobsService = jobsService;
    }

    [HttpGet("jobs-acceptance")]
    public async Task<DataSourceResult> GetJobsAcceptance([DataSourceRequest] DataSourceRequest request)
    {
        var jobs = await jobsService.GetJobsAcceptance();
        return await jobs.ToDataSourceResultAsync(request);
    }

    [HttpGet("jobs-active")]
    public async Task<DataSourceResult> GetJobsActive([DataSourceRequest] DataSourceRequest request)
    {
        var jobs = await jobsService.GetJobsActive();
        return await jobs.ToDataSourceResultAsync(request);
    }

    [HttpGet("jobs-billed")]
    public async Task<DataSourceResult> GetJobsBilled([DataSourceRequest] DataSourceRequest request)
    {
        var jobs = await jobsService.GetJobsBilled();
        return await jobs.ToDataSourceResultAsync(request);
    }


    [HttpGet("job-counters")]
    public async Task<JobCountersDto> GetJobCounters()
    {
        JobCountersDto jobCounters = await jobsService.GetJobCounters();
        return jobCounters;
    }

    [HttpGet("operators")]
    public async Task<List<JobOperatorDto>> GetOperators()
    {
        var jobOperators = await jobsService.GetOperators();
        return jobOperators.ToList();
    }

    [HttpGet("job-customers")]
    public async Task<List<ContactReadModel>> GetJobCustomers()
    {
       var jobCustomers = await jobsService.GetJobCustomers();
       return jobCustomers.ToList();
    }

    [HttpGet("job-suppliers")]
    public async Task<List<ContactReadModel>> GetJobSuppliers()
    {
        var jobCustomers = await jobsService.GetJobSuppliers();
        return jobCustomers.ToList();
    }

    [HttpGet("job-sources")]
    public async Task<List<JobSourceDto>> GetJobSources()
    {
        var jobSources = await jobsService.GetJobSources();
        return jobSources.ToList();
    }

    [HttpGet("job-product-types")]
    public async Task<List<ProductTypeDto>> GetJobProductTypes()
    {
        var jobProductTypes = await jobsService.GetJobProductTypes();
        return jobProductTypes.ToList();
    }

    [HttpGet("job-detail/{id}")]
    public async Task<JobDetailReadModel> GetJobDetail(long id)
    {
        var job = await jobsService.GetJobDetail(id);
        return job;
    }

    [HttpPost("create-job")]
    public async Task<IActionResult> CreateJob([FromBody] JobDetailDto jobDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var job = await jobsService.CreateJob(jobDto);
        return Ok(job);
    }

    [HttpPut("update-job/{id}")]
    public async Task<IActionResult> UpdateJob(long id, [FromBody] JobDetailDto jobDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await jobsService.UpdateJob(id, jobDto);
        return Ok(jobDto);
    }

    [HttpGet("all-jobs")]
    public async Task<List<JobReadModel>> getAllJobs()
    {
        var jobs = await jobsService.GetAllJobs();
        return jobs.ToList();
    }
}