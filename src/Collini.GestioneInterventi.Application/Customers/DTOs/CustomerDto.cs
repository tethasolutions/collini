using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collini.GestioneInterventi.Application.Customers.DTOs
{
    public class CustomerDto
    {
        public int CustomerSupplierId { get; set; }
        public string Type { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set;}
        public string Surname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set;}
        public string FiscalType { get; set; }
        public string ERPCode { get; set; }
        public bool Alert { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}
