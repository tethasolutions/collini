﻿using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Domain.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Quotations.DTOs
{
    public class QuotationDetailDto
    {
        public long? Id { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public QuotationStatus Status { get; set; }
        public long JobId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerContacts { get; set; }
        public string? JobCode { get; set; }
        public string? JobDescription { get; set; }
        public DateTimeOffset? JobDate { get; set; }
        public string? AttachmentFileName { get; set; }
        public string? AttachmentDisplayName { get; set; }
        public bool? HasNotes { get; set; }
        public DateTimeOffset? ActivityStart { get; set; }
        public DateTimeOffset? ActivityEnd { get; set; }
        public string? ActivityOperator { get; set; }
    }
}
