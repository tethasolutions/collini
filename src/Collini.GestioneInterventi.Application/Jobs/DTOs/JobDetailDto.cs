using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Jobs.DTOs
{
    public class JobDetailDto
    {
        public long? Id { get; set; }
        public string? Description { get; set; }
        public string? ResultNote { get; set; }
        public long OperatorId { get; set; }
        public DateTimeOffset JobDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public long CustomerId { get; set; }
        public ContactDto? Customer { get; set; }
        public long? CustomerAddressId { get; set; }
        public long SourceId { get; set; }
        public long ProductTypeId { get; set; }
        public JobStatus Status { get; set; }
        public bool? IsPaid { get; set; }
    }
}
