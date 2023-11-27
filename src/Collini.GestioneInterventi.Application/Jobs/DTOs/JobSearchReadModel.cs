using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Domain.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Jobs.DTOs
{
    public class JobSearchReadModel
    {
        public long? Id { get; set; }
        //public string? Description { get; set; }
        public string? ResultNote { get; set; }
        public string? Code { get; set; }
        public long OperatorId { get; set; }
        public DateTimeOffset JobDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public long CustomerId { get; set; }
        public ContactReadModel? Customer { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerFullAddress { get; set; }
        public long? CustomerAddressId { get; set; }
        public AddressDto? CustomerAddress { get; set; }
        public long SourceId { get; set; }
        public JobSourceDto? Source { get; set; }
        public long ProductTypeId { get; set; }
        public ProductTypeDto? ProductType { get; set; }
        public JobStatus Status { get; set; }
        public QuotationStatus? LastQuotation { get; set; }
        public DateTimeOffset? LastQuotationDate { get; set; }
        public OrderStatus? LastOrder { get; set; }
        public DateTimeOffset? LastOrderDate { get; set; }
        public ActivityStatus? LastActivity { get; set; }
        public DateTimeOffset? LastActivityDate { get; set; }
        public string? LastActivityOperator { get; set; }
        public string? LastOperatorColor { get; set; }
    }
}
