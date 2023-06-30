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

    [HttpGet("jobs-acceptance")]
    public async Task<DataSourceResult> GetJobsAcceptance([DataSourceRequest] DataSourceRequest request)
    {
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

    [HttpGet("jobs-active")]
    public async Task<DataSourceResult> GetJobsActive([DataSourceRequest] DataSourceRequest request)
    {
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

    [HttpGet("jobs-billed")]
    public async Task<DataSourceResult> GetJobsBilled([DataSourceRequest] DataSourceRequest request)
    {
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

    [HttpGet("job-counters")]
    public async Task<JobCountersDto> GetJobCounters()
    {
        JobCountersDto jobCounters = new JobCountersDto
        {
            Acceptance = new JobCounterDto
            {
                Active = 4,
                Expired = 7
            },
            Actives = new JobCounterDto
            {
                Active = 2,
                Expired = 8
            },
            Preventives = new JobCounterDto
            {
                Active = 3,
                Expired = 6
            },
            SupplierOrders = new JobCounterDto
            {
                Active = 5,
                Expired = 1
            },
            Interventions = new JobCounterDto
            {
                Active = 1,
                Expired = 4
            },
            Billed = new JobCounterDto
            {
                Active = 7,
                Expired = 3
            }
        };

        return jobCounters;
    }

    [HttpGet("operators")]
    public async Task<List<JobOperatorDto>> GetOperators()
    {
        List<JobOperatorDto> operators = new List<JobOperatorDto>
        {
            new JobOperatorDto
            {
                Id = 1,
                Name = "Smith",
                Surname = "Tucson"
            },
            new JobOperatorDto
            {
                Id = 2,
                Name = "John",
                Surname = "Travolta"
            },
            new JobOperatorDto
            {
                Id = 3,
                Name = "Alex",
                Surname = "Ronaldo"
            }
        };

        return operators;
    }

    [HttpGet("job-customers")]
    public async Task<List<ContactReadModel>> GetJobCustomers()
    {
        List<ContactReadModel> jobCustomers = new List<ContactReadModel>
        {
            new ContactReadModel
            {
                Id = 1,
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
                        Id = 1,
                        ContactId = 1,
                        City = "Verona",
                        StreetAddress = "via Roma 15",
                        Province = "VR",
                        ZipCode = "25031",
                        Telephone = "+393889445428",
                        Email = "Smith_Tucson@gmail.com",
                        IsMainAddress = true
                    },
                    new AddressDto
                    {
                        Id = 2,
                        ContactId = 1,
                        City = "Toscana",
                        StreetAddress = "corso Milano 12",
                        Province = "TS",
                        ZipCode = "23084",
                        Telephone = "+393889445428",
                        Email = "Smith_Tucson@gmail.com",
                        IsMainAddress = false
                    },
                    new AddressDto
                    {
                        Id = 3,
                        ContactId = 1,
                        City = "Bologna",
                        StreetAddress = "via Ronchi 19",
                        Province = "BL",
                        ZipCode = "29057",
                        Telephone = "+393889445428",
                        Email = "Smith_Tucson@gmail.com",
                        IsMainAddress = false
                    }
                }
            },
            new ContactReadModel
            {
                Id = 2,
                Type = ContactType.Customer,
                CompanyName = "Cannon",
                Name = "John",
                Surname = "Travolta",
                FiscalType = ContactFiscalType.Company,
                ErpCode = "ERP456",
                Alert = true,
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 4,
                        ContactId = 2,
                        City = "Milano",
                        StreetAddress = "corso Garibaldi 11",
                        Province = "MN",
                        ZipCode = "21007",
                        Telephone = "+393888501683",
                        Email = "John_Travolta@gmail.com",
                        IsMainAddress = true
                    },
                    new AddressDto
                    {
                        Id = 5,
                        ContactId = 2,
                        City = "Toscana",
                        StreetAddress = "corso Milano 12",
                        Province = "TS",
                        ZipCode = "23084",
                        Telephone = "+393888501683",
                        Email = "John_Travolta@gmail.com",
                        IsMainAddress = false
                    },
                    new AddressDto
                    {
                        Id = 6,
                        ContactId = 2,
                        City = "Bologna",
                        StreetAddress = "via Ronchi 19",
                        Province = "BL",
                        ZipCode = "29057",
                        Telephone = "+393888501683",
                        Email = "John_Travolta@gmail.com",
                        IsMainAddress = false
                    }
                }
            },
            new ContactReadModel
            {
                Id = 3,
                Type = ContactType.Customer,
                CompanyName = "IDM",
                Name = "Alex",
                Surname = "Ronaldo",
                FiscalType = ContactFiscalType.Building,
                ErpCode = "ERP789",
                Alert = true,
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 7,
                        ContactId = 3,
                        City = "Bergamo",
                        StreetAddress = "via Venezia 14",
                        Province = "BG",
                        ZipCode = "24001",
                        Telephone = "+393883504629",
                        Email = "Alex_Ronaldo@gmail.com",
                        IsMainAddress = true
                    },
                    new AddressDto
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
                    },
                    new AddressDto
                    {
                        Id = 9,
                        ContactId = 3,
                        City = "Bologna",
                        StreetAddress = "via Ronchi 19",
                        Province = "BL",
                        ZipCode = "29057",
                        Telephone = "+393883504629",
                        Email = "Alex_Ronaldo@gmail.com",
                        IsMainAddress = false
                    }
                }
            }
        };

        return jobCustomers;
    }

    [HttpGet("job-sources")]
    public async Task<List<JobSourceDto>> GetJobSources()
    {
        List<JobSourceDto> jobSources = new List<JobSourceDto>
        {
            new JobSourceDto
            {
                Id = 1,
                Name = "Source 1"
            },
            new JobSourceDto
            {
                Id = 2,
                Name = "Source 2"
            },
            new JobSourceDto
            {
                Id = 3,
                Name = "Source 3"
            }
        };

        return jobSources;
    }

    [HttpGet("job-product-types")]
    public async Task<List<ProductTypeDto>> GetJobProductTypes()
    {
        List<ProductTypeDto> jobProductTypes = new List<ProductTypeDto>
        {
            new ProductTypeDto
            {
                Id = 1,
                Name = "Product Type 1"
            },
            new ProductTypeDto
            {
                Id = 2,
                Name = "Product Type 2"
            },
            new ProductTypeDto
            {
                Id = 3,
                Name = "Product Type 3"
            }
        };

        return jobProductTypes;
    }

    [HttpGet("job-detail/{id}")]
    public async Task<JobDetailReadModel> GetJobDetail(long id)
    {
        JobDetailReadModel job = new JobDetailReadModel
        {
            Id = 1,
            CreatedOn = new DateTime(2023, 7, 12),
            ExpirationDate = new DateTime(2023, 8, 16),
            Description = "descrizione commessa test",
            OperatorId = 2,
            CustomerId = 1,
            CustomerAddressId = 2,
            SourceId = 1,
            ProductTypeId = 2,
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
                            Id = 1,
                            City = "Verona",
                            StreetAddress = "via Roma 15",
                            Province = "VR",
                            ZipCode = "25031",
                            IsMainAddress = true
                        },
                        new AddressDto
                        {
                            Id = 2,
                            City = "Toscana",
                            StreetAddress = "corso Milano 12",
                            Province = "TS",
                            ZipCode = "23084",
                            IsMainAddress = false
                        },
                        new AddressDto
                        {
                            Id = 3,
                            City = "Bologna",
                            StreetAddress = "via Ronchi 19",
                            Province = "BL",
                            ZipCode = "29057",
                            IsMainAddress = false
                        }
                    }
            }
        };

        return job;
    }

    [HttpPost("create-job")]
    public async Task<IActionResult> CreateJob([FromBody] JobDetailDto job)
    {
        return NoContent();
    }

    [HttpPut("update-job/{id}")]
    public async Task<IActionResult> UpdateJob(long id, [FromBody] JobDetailDto job)
    {
        return NoContent();
    }

    [HttpGet("all-jobs")]
    public async Task<List<JobReadModel>> getAllJobs()
    {
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

        return jobs;
    }
}