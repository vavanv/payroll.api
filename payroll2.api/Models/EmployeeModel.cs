using System;

namespace Payroll2.Api.Models
{
    public class EmployeeModel
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool UserEnabled { get; set; }

        public int UserRightId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public string Sin { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime DateOfHire { get; set; }

        public int DepartmentId { get; set; }
        public string Department { get; set; } = string.Empty;

        public int PositionId { get; set; }
        public string Position { get; set; } = string.Empty;

        public int WageTypeId { get; set; }

        public int VacationPolicyId { get; set; }

        public decimal VacationRate { get; set; }

        public int? AddressId { get; set; }

        public string Street { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public int ProvinceId { get; set; }

        public string PostalCode { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
    }
}