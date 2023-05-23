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
        List<Job> jobs = new List<Job>
        {
            new Job
            {
                Number = 1,
                Year = 2023,
                ExpirationDate = new DateTime(2023, 8, 16),
                Description = "descrizione commessa test",
                Status = JobStatus.Working,
                StatusChangedOn = DateTime.Now,
                ProductType = new ProductType
                {
                    Name = "Casseforti"
                },
                CustomerId = 1,
                Customer = new Contact
                {
                    Type = ContactType.Customer,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.PrivatePerson,
                    ErpCode = "ERP123",
                    Alert = false,
                    Addresses = new List<ContactAddress>
                    {
                        new ContactAddress
                        {
                            City = "Verona",
                            StreetAddress = "via Roma 15",
                            Province = "VR",
                            ZipCode = "25031",
                            IsMainAddress = true
                        },
                        new ContactAddress
                        {
                            City = "Toscana",
                            StreetAddress = "corso Milano 12",
                            Province = "TS",
                            ZipCode = "23084",
                            IsMainAddress = false
                        },
                        new ContactAddress
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
            new Job
            {
                Number = 2,
                Year = 2023,
                ExpirationDate = new DateTime(2023, 5, 16),
                Description = "descrizione commessa test 2",
                Status = JobStatus.Working,
                StatusChangedOn = DateTime.Now,
                ProductType = new ProductType
                {
                    Name = "Serrature"
                },
                CustomerId = 1,
                Customer = new Contact
                {
                    Type = ContactType.Customer,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.PrivatePerson,
                    ErpCode = "ERP123",
                    Alert = true,
                    Addresses = new List<ContactAddress>
                    {
                        new ContactAddress
                        {
                            City = "Verona",
                            StreetAddress = "via Roma 15",
                            Province = "VR",
                            ZipCode = "25031",
                            IsMainAddress = true
                        },
                        new ContactAddress
                        {
                            City = "Toscana",
                            StreetAddress = "corso Milano 12",
                            Province = "TS",
                            ZipCode = "23084",
                            IsMainAddress = false
                        },
                        new ContactAddress
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
            new Job
            {
                Number = 3,
                Year = 2023,
                ExpirationDate = new DateTime(2023, 5, 17),
                Description = "descrizione commessa test 3",
                Status = JobStatus.Completed,
                StatusChangedOn = DateTime.Now,
                ProductType = new ProductType
                {
                    Name = "Chiavi"
                },
                CustomerId = 1,
                Customer = new Contact
                {
                    Type = ContactType.Customer,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.PrivatePerson,
                    ErpCode = "ERP123",
                    Alert = false,
                    Addresses = new List<ContactAddress>
                    {
                        new ContactAddress
                        {
                            City = "Verona",
                            StreetAddress = "via Roma 15",
                            Province = "VR",
                            ZipCode = "25031",
                            IsMainAddress = true
                        },
                        new ContactAddress
                        {
                            City = "Toscana",
                            StreetAddress = "corso Milano 12",
                            Province = "TS",
                            ZipCode = "23084",
                            IsMainAddress = false
                        },
                        new ContactAddress
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