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
    public class JobReadModel
    {
        public long Id { get; set; }
        public DateTimeOffset JobDate { get; set; }
        public int Number { get; set; }
        public int Year { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public string? Description { get; set; }
        public JobStatus Status { get; set; }
        public ContactReadModel? Customer { get; set; }
    }
}
