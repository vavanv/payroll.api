using System;

namespace Payroll2.Api.Models
{
    public class CompanyModel
    {
        public int? Id { get; set; }
        public string LegalName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int ProvinceId { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        public int BusinessTypeId { get; set; }
        public DateTime EstablishedDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}