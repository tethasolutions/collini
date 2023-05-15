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
public class CustomersController : ColliniApiController
{
    public CustomersController()
    {
    }

    [HttpGet("customers")]
    public async Task<DataSourceResult> GetCustomers([DataSourceRequest] DataSourceRequest request)
    {
        List<CustomerDto> customers = new List<CustomerDto>
        {
            new CustomerDto
            {
                CustomerSupplierId = 1,
                Type = "C",
                CompanyName = "General Motors",
                Name = "Smith",
                Surname = "Tucson",
                Telephone = "+393889445428",
                Email = "Smith_Tucson@gmail.com",
                FiscalType = "P",
                ERPCode = "ERP123",
                Alert = false,
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        AddressId = 1,
                        CustomerId = 1,
                        City = "Verona",
                        Address = "via Roma 15",
                        Province = "VR",
                        ZipCode = "25031",
                        IsMainAddress = true
                    },
                    new AddressDto
                    {
                        AddressId = 2,
                        CustomerId = 1,
                        City = "Toscana",
                        Address = "corso Milano 12",
                        Province = "TS",
                        ZipCode = "23084",
                        IsMainAddress = false
                    },
                    new AddressDto
                    {
                        AddressId = 3,
                        CustomerId = 1,
                        City = "Bologna",
                        Address = "via Ronchi 19",
                        Province = "BL",
                        ZipCode = "29057",
                        IsMainAddress = false
                    }
                }
            },
            new CustomerDto
            {
                CustomerSupplierId = 2,
                Type = "F",
                CompanyName = "Cannon",
                Name = "John",
                Surname = "Travolta",
                Telephone = "+393888501683",
                Email = "John_Travolta@gmail.com",
                FiscalType = "A",
                ERPCode = "ERP456",
                Alert = true,
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        AddressId = 4,
                        CustomerId = 2,
                        City = "Milano",
                        Address = "corso Garibaldi 11",
                        Province = "MN",
                        ZipCode = "21007",
                        IsMainAddress = true
                    },
                    new AddressDto
                    {
                        AddressId = 5,
                        CustomerId = 2,
                        City = "Toscana",
                        Address = "corso Milano 12",
                        Province = "TS",
                        ZipCode = "23084",
                        IsMainAddress = false
                    },
                    new AddressDto
                    {
                        AddressId = 6,
                        CustomerId = 2,
                        City = "Bologna",
                        Address = "via Ronchi 19",
                        Province = "BL",
                        ZipCode = "29057",
                        IsMainAddress = false
                    }
                }
            },
            new CustomerDto
            {
                CustomerSupplierId = 3,
                Type = "C",
                CompanyName = "IDM",
                Name = "Alex",
                Surname = "Ronaldo",
                Telephone = "+393883504629",
                Email = "Alex_Ronaldo@gmail.com",
                FiscalType = "G",
                ERPCode = "ERP789",
                Alert = true,
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        AddressId = 7,
                        CustomerId = 3,
                        City = "Bergamo",
                        Address = "via Venezia 14",
                        Province = "BG",
                        ZipCode = "24001",
                        IsMainAddress = true
                    },
                    new AddressDto
                    {
                        AddressId = 8,
                        CustomerId = 3,
                        City = "Toscana",
                        Address = "corso Milano 12",
                        Province = "TS",
                        ZipCode = "23084",
                        IsMainAddress = false
                    },
                    new AddressDto
                    {
                        AddressId = 9,
                        CustomerId = 3,
                        City = "Bologna",
                        Address = "via Ronchi 19",
                        Province = "BL",
                        ZipCode = "29057",
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
    public async Task<CustomerDto> GetCustomer(long id)
    {
        var customer = new CustomerDto
        {
            CustomerSupplierId = 3,
            Type = "C",
            CompanyName = "IDM",
            Name = "Alex",
            Surname = "Ronaldo",
            Telephone = "+393883504629",
            Email = "Alex_Ronaldo@gmail.com",
            FiscalType = "A",
            ERPCode = "ERP789",
            Alert = true,
            Addresses = new List<AddressDto>
            {
                new AddressDto
                {
                    AddressId = 7,
                    CustomerId = 3,
                    City = "Bergamo",
                    Address = "via Venezia 14",
                    Province = "BG",
                    ZipCode = "24001",
                    IsMainAddress = false
                },
                new AddressDto
                {
                    AddressId = 8,
                    CustomerId = 3,
                    City = "Toscana",
                    Address = "corso Milano 12",
                    Province = "TS",
                    ZipCode = "23084",
                    IsMainAddress = false
                },
                new AddressDto
                {
                    AddressId = 9,
                    CustomerId = 3,
                    City = "Bologna",
                    Address = "via Ronchi 19",
                    Province = "BL",
                    ZipCode = "29057",
                    IsMainAddress = true
                }
            }
        };

        return customer;
    }

    [HttpPut("customer/{id}")]
    public async Task<IActionResult> UpdateCustomer(long id, CustomerDto request)
    {
        return NoContent();
    }

    [HttpDelete("customer/{id}")]
    public async Task<IActionResult> DeleteCustomer(long id)
    {
        return NoContent();
    }

    [HttpPost("customer")]
    public async Task<IActionResult> CreateCustomer(CustomerDto request)
    {
        return NoContent();
    }
}