using Collini.GestioneInterventi.Domain.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Activities.DTOs
{
    public class ActivityViewModel
    {
        public long Id { get; set; }
        public long OperatorId { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public ActivityStatus Status { get; set; }
        public string Customer { get; set; }

        public long JobId { get; set; }
        public string Job { get; set; }
    }
}
