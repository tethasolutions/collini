using Collini.GestioneInterventi.Domain.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Activities.DTOs
{
    public class CopyActivityDto
    {
        public long Id { get; set; }
        public long NewOperatorId { get; set; }

    }
}
