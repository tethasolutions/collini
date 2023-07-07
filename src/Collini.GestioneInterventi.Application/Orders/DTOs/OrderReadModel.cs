﻿using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Domain.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Orders.DTOs
{
    public class OrderReadModel
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public long SupplierId { get; set; }
        public ContactReadModel? Supplier { get; set; }
        public long JobId { get; set; }
        public string? JobCode { get; set; }
        public string? JobDescription { get; set; }
        public string? CustomerName { get; set; }
        public OrderStatus Status { get; set; }
    }
}
