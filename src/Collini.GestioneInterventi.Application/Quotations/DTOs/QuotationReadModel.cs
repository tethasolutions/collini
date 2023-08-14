using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Domain.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Quotations.DTOs
{
    public class QuotationReadModel
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public QuotationStatus Status { get; set; }
        public long JobId { get; set; }
        public string CustomerName { get; set; }
        public string JobCode { get; set; }
        public string? JobDescription { get; set; }
        public QuotationAttachmentReadModel? Attachment { get; set; }



    }
}
