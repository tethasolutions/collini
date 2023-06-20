using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Activities.DTOs
{
    public class CalendarViewModel
    {
        public List<ActivityViewModel> Activities { get; set; }
        public List<CalendarResourceViewModel> Resources { get; set; }
    }
}
