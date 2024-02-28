using Collini.GestioneInterventi.Application.Activities.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Jobs.DTOs
{
    public class JobActivitiesDto
    {
        public long? Id { get; set; }

        public ICollection<ActivityDto> Activities { get; set; }

        public JobActivitiesDto()
        {
            Activities = new List<ActivityDto>();
        }
    }
}
