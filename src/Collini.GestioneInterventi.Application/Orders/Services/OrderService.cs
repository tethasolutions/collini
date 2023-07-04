using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Application.Orders.DTOs;
using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Framework.Exceptions;
using Collini.GestioneInterventi.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Collini.GestioneInterventi.Application.Orders.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDetailDto>> GetOrders();
        Task<OrderDetailDto> GetOrderDetail(long id);
        Task<OrderDetailDto> CreateOrder(OrderDetailDto order);
        Task<OrderDetailDto> UpdateOrder(long id, OrderDetailDto order);
        Task<IEnumerable<OrderReadModel>> getAllOrders();
    }

    public class OrderService : IOrderService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Order> orderRepository;
        private readonly IColliniDbContext dbContext;

        public OrderService(
            IMapper mapper,
            IRepository<Order> orderRepository,
            IColliniDbContext dbContext)
        {
            this.mapper = mapper;
            this.orderRepository = orderRepository;
            this.dbContext = dbContext;
        }


        public async Task<OrderDetailDto> UpdateOrder(long id,OrderDetailDto orderdtDto)
        {
            var activity = await orderRepository.Get(id);

            if (activity == null)
            {
                throw new NotFoundException(typeof(Order), id);
            }
            orderdtDto.MapTo(activity, mapper);

            await dbContext.SaveChanges();

            return activity.MapTo<OrderDetailDto>(mapper);
        }

        public async Task<IEnumerable<OrderDetailDto>> GetOrders()
        {
            var orders = await orderRepository
                .Query()
                .AsNoTracking()
                .ToArrayAsync();

            return orders.MapTo<IEnumerable<OrderDetailDto>>(mapper);
        }

        public async Task<OrderDetailDto> GetOrderDetail(long id)
        {

            if (id == 0)
                throw new ApplicationException("Impossibile recuperare un order con id 0");

            var job = await orderRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (job == null)
                throw new ApplicationException($"Impossibile trovare l'order con id {id}");

            return job.MapTo<OrderDetailDto>(mapper);
        }

        public async Task<OrderDetailDto> CreateOrder(OrderDetailDto orderDto)
        {
            var order = orderDto.MapTo<Order>(mapper);
            await orderRepository.Insert(order);
            await dbContext.SaveChanges();
            return order.MapTo<OrderDetailDto>(mapper);
        }
       
        public async Task<IEnumerable<OrderReadModel>> getAllOrders()
        {
            var orders = await orderRepository
                .Query()
                .AsNoTracking()
                .ToArrayAsync();

            return orders.MapTo<IEnumerable<OrderReadModel>>(mapper);
        }


    }
}
