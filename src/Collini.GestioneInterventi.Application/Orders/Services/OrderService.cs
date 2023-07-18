using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Application.Jobs.DTOs;
using Collini.GestioneInterventi.Application.Jobs.Services;
using Collini.GestioneInterventi.Application.Orders.DTOs;
using Collini.GestioneInterventi.Application.Quotations.DTOs;
using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Framework.Exceptions;
using Collini.GestioneInterventi.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Collini.GestioneInterventi.Application.Orders.Services
{
    public interface IOrderService
    {
        IQueryable<OrderDetailDto> GetOrders();
        Task<OrderDetailDto> GetOrderDetail(long id);
        Task<OrderDetailDto> CreateOrder(OrderDetailDto order);
        Task UpdateOrder(long id, OrderDetailDto order);
        Task<IEnumerable<OrderReadModel>> getAllOrders();
    }

    public class OrderService : IOrderService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Order> orderRepository;
        private readonly IJobService jobService;
        private readonly IColliniDbContext dbContext;

        public OrderService(
            IMapper mapper,
            IRepository<Order> orderRepository,
            IColliniDbContext dbContext, 
            IJobService jobService)
        {
            this.mapper = mapper;
            this.orderRepository = orderRepository;
            this.dbContext = dbContext;
            this.jobService = jobService;
        }


        public async Task UpdateOrder(long id,OrderDetailDto orderdtDto)
        {
            if (id == 0)
                throw new ApplicationException("Impossibile aggiornare una nota con id 0");

            var order= await orderRepository
                .Query()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();;

            if (order == null)
                throw new ApplicationException($"Impossibile trovare una nota con id {id}");

            orderdtDto.MapTo(order, mapper);
            orderRepository.Update(order);
            await dbContext.SaveChanges();
            
        }

        public IQueryable<OrderDetailDto> GetOrders()
        {
            var orders = orderRepository
                .Query()
                .AsNoTracking()
                .Where (x => x.Status == OrderStatus.Pending || x.Status == OrderStatus.Sent)
                .Project<OrderDetailDto>(mapper);

            return orders;
        }

        public async Task<OrderDetailDto> GetOrderDetail(long id)
        {

            if (id == 0)
                throw new ApplicationException("Impossibile recuperare un order con id 0");

            var order = await orderRepository
                .Query()
                .AsNoTracking()
                .Include(x=>x.Supplier)
                .Include(x => x.Job)
                .ThenInclude(y => y.Customer)
                .Include(x => x.Job)
                .ThenInclude(y => y.CustomerAddress)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (order == null)
                throw new ApplicationException($"Impossibile trovare l'order con id {id}");

            return order.MapTo<OrderDetailDto>(mapper);
        }

        public async Task<OrderDetailDto> CreateOrder(OrderDetailDto orderDto)
        {
            var order = orderDto.MapTo<Order>(mapper);
            await orderRepository.Insert(order);
            
            var job = await jobService.GetJob(orderDto.JobId);
            if (job == null)
                throw new ApplicationException("Job non trovato");
            if (job.Status == JobStatus.Pending)
                job.Status = JobStatus.Working;
            await jobService.UpdateJob(job.Id, job.MapTo<JobDetailDto>(mapper));
            
            await dbContext.SaveChanges();

            order.Job = await jobService.GetJob(orderDto.JobId);

            return order.MapTo<OrderDetailDto>(mapper);
        }
       
        public async Task<IEnumerable<OrderReadModel>> getAllOrders()
        {
            var orders = await orderRepository
                .Query()
                .Include(x=>x.Supplier)
                .Include(x=>x.Job)
                .AsNoTracking()
                .ToArrayAsync();

            return orders.MapTo<IEnumerable<OrderReadModel>>(mapper);
        }


    }
}
