using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Jobs.DTOs
{
    public class JobCountersDto
    {
        public JobCounterDto Acceptance { get; set; }
        public JobCounterDto Actives { get; set; }
        public JobCounterDto Preventives { get; set; }
        public JobCounterDto SupplierOrders { get; set; }
        public JobCounterDto Interventions { get; set; }
        public JobCounterDto Completed { get; set; }
        public JobCounterDto Billing { get; set; }
    }
}
