using Collini.GestioneInterventi.Domain.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collini.GestioneInterventi.Application.Jobs.DTOs;

namespace Collini.GestioneInterventi.Application.Activities.DTOs
{
    public class ActivityViewModel
    {
        public long Id { get; set; }
        public long OperatorId { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public ActivityStatus Status { get; set; }
        public long JobId { get; set; }
    }
}
