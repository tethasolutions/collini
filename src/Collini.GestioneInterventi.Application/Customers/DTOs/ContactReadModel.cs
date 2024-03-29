﻿using Collini.GestioneInterventi.Domain.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Customers.DTOs
{
    public class ContactReadModel
    {
        public long Id { get; set; }
        public ContactType Type { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set;}
        public string Surname { get; set; }
        public ContactFiscalType FiscalType { get; set; }
        public string ErpCode { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public bool Alert { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}
