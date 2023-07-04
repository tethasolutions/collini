using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Orders.DTOs;
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
public class OrdersController : ColliniApiController
{
    public OrdersController()
    {
    }

    [HttpGet("orders")]
    public async Task<DataSourceResult> GetOrders([DataSourceRequest] DataSourceRequest request)
    {
        List<OrderReadModel> orders = new List<OrderReadModel>
        {
            new OrderReadModel
            {
                Id = 1,
                Code = "0001",
                ExpirationDate = new DateTime(2023, 8, 16),
                Description = "descrizione ordine test",
                Status = OrderStatus.Pending,
                JobCode = "1/2023",
                CustomerName = "Cliente 1",
                JobDescription = "descrizione commessa test",
                Supplier = new ContactReadModel
                {
                    Type = ContactType.Supplier,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.Company,
                    ErpCode = "ERP123",
                    Alert = false,
                }
            },
            new OrderReadModel
            {
                Id = 2,
                Code = "0002",
                ExpirationDate = new DateTime(2023, 8, 16),
                Description = "descrizione ordine test",
                Status = OrderStatus.Completed,
                JobCode = "1/2023",
                CustomerName = "Cliente 1",
                JobDescription = "descrizione commessa test",
                Supplier = new ContactReadModel
                {
                    Type = ContactType.Supplier,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.Company,
                    ErpCode = "ERP123",
                    Alert = false,
                }
            },
            new OrderReadModel
            {
                Id = 3,
                Code = "0003",
                ExpirationDate = new DateTime(2023, 8, 16),
                Description = "descrizione ordine test",
                Status = OrderStatus.Canceled,
                JobCode = "1/2023",
                CustomerName = "Cliente 1",
                JobDescription = "descrizione commessa test",
                Supplier = new ContactReadModel
                {
                    Type = ContactType.Supplier,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.Company,
                    ErpCode = "ERP123",
                    Alert = false,
                }
            }
        };

        DataSourceResult result = new DataSourceResult
        {
            AggregateResults = null,
            Errors = null,
            Total = 3,
            Data = orders
        };

        return result;
    }


    [HttpGet("order-detail/{id}")]
    public async Task<OrderDetailDto> GetOrderDetail(long id)
    {
        OrderDetailDto order = new OrderDetailDto
        {
            Id = 1,
            Code = "0001",
            ExpirationDate = new DateTime(2023, 8, 16),
            Description = "descrizione ordine test",
            Status = OrderStatus.Pending,
            JobCode = "1/2023",
            CustomerName = "Cliente 1",
            JobDescription = "descrizione commessa test",
            Supplier = new ContactDto
            {
                Type = ContactType.Supplier,
                CompanyName = "General Motors",
                Name = "Smith",
                Surname = "Tucson",
                FiscalType = ContactFiscalType.Company,
                ErpCode = "ERP123",
                Alert = false,
            }
        };

        return order;
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDetailDto order)
    {
        return NoContent();
    }

    [HttpPut("update-order/{id}")]
    public async Task<IActionResult> UpdateOrder(long id, [FromBody] OrderDetailDto order)
    {
        return NoContent();
    }

    [HttpGet("all-orders")]
    public async Task<List<OrderReadModel>> getAllOrders()
    {
        List<OrderReadModel> orders = new List<OrderReadModel>
        {
            new OrderReadModel
            {
                Id = 1,
                Code = "0001",
                ExpirationDate = new DateTime(2023, 8, 16),
                Description = "descrizione ordine test",
                Status = OrderStatus.Pending,
                JobCode = "1/2023",
                CustomerName = "Cliente 1",
                JobDescription = "descrizione commessa test",
                Supplier = new ContactReadModel
                {
                    Type = ContactType.Supplier,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.Company,
                    ErpCode = "ERP123",
                    Alert = false,
                }
            },
            new OrderReadModel
            {
                Id = 2,
                Code = "0002",
                ExpirationDate = new DateTime(2023, 8, 16),
                Description = "descrizione ordine test",
                Status = OrderStatus.Completed,
                JobCode = "1/2023",
                CustomerName = "Cliente 1",
                JobDescription = "descrizione commessa test",
                Supplier = new ContactReadModel
                {
                    Type = ContactType.Supplier,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.Company,
                    ErpCode = "ERP123",
                    Alert = false,
                }
            },
            new OrderReadModel
            {
                Id = 3,
                Code = "0003",
                ExpirationDate = new DateTime(2023, 8, 16),
                Description = "descrizione ordine test",
                Status = OrderStatus.Canceled,
                JobCode = "1/2023",
                CustomerName = "Cliente 1",
                JobDescription = "descrizione commessa test",
                Supplier = new ContactReadModel
                {
                    Type = ContactType.Supplier,
                    CompanyName = "General Motors",
                    Name = "Smith",
                    Surname = "Tucson",
                    FiscalType = ContactFiscalType.Company,
                    ErpCode = "ERP123",
                    Alert = false,
                }
            }
        };

        return orders;
    }
}