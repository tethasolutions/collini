using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Security;
using Collini.GestioneInterventi.Application.Security.DTOs;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.WebApi.Auth;
using Collini.GestioneInterventi.WebApi.Models.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collini.GestioneInterventi.WebApi.Controllers;

[RequireUser]
public class CustomersController : ColliniApiController
{
    public CustomersController()
    {
    }

    [HttpGet("customers")]
    public async Task<DataSourceResult> GetCustomers([DataSourceRequest] DataSourceRequest request)
    {
        List<ContactReadModel> customers = new List<ContactReadModel>
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

        DataSourceResult result = new DataSourceResult
        {
            AggregateResults = null,
            Errors = null,
            Total = 3,
            Data = customers
        };

        return result;
    }

    [HttpGet("providers")]
    public async Task<DataSourceResult> GetProviders([DataSourceRequest] DataSourceRequest request)
    {
        List<ContactReadModel> customers = new List<ContactReadModel>
        {
            new ContactReadModel
            {
                Id = 1,
                Type = ContactType.Supplier,
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
                Type = ContactType.Supplier,
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
                Type = ContactType.Supplier,
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

        DataSourceResult result = new DataSourceResult
        {
            AggregateResults = null,
            Errors = null,
            Total = 3,
            Data = customers
        };

        return result;
    }

    [HttpGet("customer/{id}")]
    public async Task<ContactReadModel> GetCustomer(long id)
    {
        var customer = new ContactReadModel
        {
            Id = 3,
            Type = ContactType.Customer,
            CompanyName = "IDM",
            Name = "Alex",
            Surname = "Ronaldo",
            FiscalType = ContactFiscalType.PrivatePerson,
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
                    IsMainAddress = false
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
                    IsMainAddress = true
                }
            }
        };

        return customer;
    }

    [HttpPut("customer/{id}")]
    public async Task<IActionResult> UpdateCustomer(long id, [FromBody] ContactDto request)
    {
        return NoContent();
    }

    [HttpDelete("customer/{id}")]
    public async Task<IActionResult> DeleteCustomer(long id)
    {
        return NoContent();
    }

    [HttpPost("customer")]
    public async Task<IActionResult> CreateCustomer([FromBody] ContactDto request)
    {
        return NoContent();
    }
}