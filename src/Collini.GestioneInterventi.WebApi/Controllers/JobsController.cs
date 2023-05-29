using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
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
public class JobsController : ColliniApiController
{
    public JobsController()
    {
    }

    [HttpGet("jobs")]
    public async Task<DataSourceResult> GetJobs([DataSourceRequest] DataSourceRequest request)
    {
        // JobReadModel
        List<JobReadModel> jobs = new List<JobReadModel>
        {
            new JobReadModel
            {
                Id = 1,
                CreatedOn = new DateTime(2023, 7, 12),
                Number = 1,
                Year = 2023,
                ExpirationDate = new DateTime(2023, 8, 16),
                Description = "descrizione commessa test",
                Status = JobStatus.Working,
                Customer = new ContactReadModel
                {
                    Type = ContactType.Customer,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.PrivatePerson,
                    ErpCode = "ERP123",
                    Alert = false,
                    Addresses = new List<AddressDto>
                    {
                        new AddressDto
                        {
                            City = "Verona",
                            StreetAddress = "via Roma 15",
                            Province = "VR",
                            ZipCode = "25031",
                            IsMainAddress = true
                        },
                        new AddressDto
                        {
                            City = "Toscana",
                            StreetAddress = "corso Milano 12",
                            Province = "TS",
                            ZipCode = "23084",
                            IsMainAddress = false
                        },
                        new AddressDto
                        {
                            City = "Bologna",
                            StreetAddress = "via Ronchi 19",
                            Province = "BL",
                            ZipCode = "29057",
                            IsMainAddress = false
                        }
                    }
                }
            },
            new JobReadModel
            {
                Id = 2,
                CreatedOn = new DateTime(2023, 4, 18),
                Number = 2,
                Year = 2023,
                ExpirationDate = new DateTime(2023, 5, 16),
                Description = "descrizione commessa test 2",
                Status = JobStatus.Working,
                Customer = new ContactReadModel
                {
                    Type = ContactType.Customer,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.PrivatePerson,
                    ErpCode = "ERP123",
                    Alert = true,
                    Addresses = new List<AddressDto>
                    {
                        new AddressDto
                        {
                            City = "Verona",
                            StreetAddress = "via Roma 15",
                            Province = "VR",
                            ZipCode = "25031",
                            IsMainAddress = true
                        },
                        new AddressDto
                        {
                            City = "Toscana",
                            StreetAddress = "corso Milano 12",
                            Province = "TS",
                            ZipCode = "23084",
                            IsMainAddress = false
                        },
                        new AddressDto
                        {
                            City = "Bologna",
                            StreetAddress = "via Ronchi 19",
                            Province = "BL",
                            ZipCode = "29057",
                            IsMainAddress = false
                        }
                    }
                }
            },
            new JobReadModel
            {
                Id = 3,
                CreatedOn = new DateTime(2023, 4, 11),
                Number = 3,
                Year = 2023,
                ExpirationDate = new DateTime(2023, 5, 17),
                Description = "descrizione commessa test 3",
                Status = JobStatus.Completed,
                Customer = new ContactReadModel
                {
                    Type = ContactType.Customer,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.Company,
                    ErpCode = "ERP123",
                    Alert = false,
                    Addresses = new List<AddressDto>
                    {
                        new AddressDto
                        {
                            City = "Verona",
                            StreetAddress = "via Roma 15",
                            Province = "VR",
                            ZipCode = "25031",
                            IsMainAddress = true
                        },
                        new AddressDto
                        {
                            City = "Toscana",
                            StreetAddress = "corso Milano 12",
                            Province = "TS",
                            ZipCode = "23084",
                            IsMainAddress = false
                        },
                        new AddressDto
                        {
                            City = "Bologna",
                            StreetAddress = "via Ronchi 19",
                            Province = "BL",
                            ZipCode = "29057",
                            IsMainAddress = false
                        }
                    }
                }
            }
        };

        DataSourceResult result = new DataSourceResult
        {
            AggregateResults = null,
            Errors = null,
            Total = 3,
            Data = jobs
        };

        return result;
    }
}