using Collini.GestioneInterventi.Domain.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Activities.DTOs
{
    public class ActivityDto
    {
        public long? Id { get; set; }
        public long OperatorId { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public ActivityStatus Status { get; set; }
    }
}
