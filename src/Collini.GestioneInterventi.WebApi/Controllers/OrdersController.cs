using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Application.Activities.Services;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Orders.DTOs;
using Collini.GestioneInterventi.Application.Orders.Services;
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
    private readonly IOrderService orderService;
    public OrdersController(IOrderService orderService)
    {
        this.orderService = orderService;
    }

    [HttpGet("orders")]
    public async Task<DataSourceResult> GetOrders([DataSourceRequest] DataSourceRequest request)
    {
        var orders = orderService.GetOrders();
        return await orders.ToDataSourceResultAsync(request);
    }


    [HttpGet("order-detail/{id}")]
    public async Task<OrderDetailDto> GetOrderDetail(long id)
    {
        var order = await orderService.GetOrderDetail(id);
        return order;
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDetailDto orderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await orderService.CreateOrder(orderDto);
        return Ok(orderDto);
    }

    [HttpPut("update-order/{id}")]
    public async Task<IActionResult> UpdateOrder(long id, [FromBody] OrderDetailDto orderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await orderService.UpdateOrder(id, orderDto);
        return Ok();
    }

    [HttpGet("all-orders")]
    public async Task<List<OrderReadModel>> getAllOrders()
    {
        var jobs = await orderService.getAllOrders();
        return jobs.ToList();
    }
}