using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Jobs.DTOs
{
    public class JobCounterDto
    {
        public int Active { get; set; }
        public int Expired { get; set; }
    }
}
