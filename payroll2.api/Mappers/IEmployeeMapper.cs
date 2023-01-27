using System;
using System.Collections.Generic;

using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.Models;

namespace Payroll2.Api.Mappers
{
    public interface IEmployeeMapper
    {
        EmployeeModel MapFrom(Employee employeeEntity);
        EmployeeModel MapFrom(Employee employeeEntity, Address addressEntity, User userEntity);
        Employee MapFrom(EmployeeModel model, Employee employeeEntity);
        Address MapFrom(EmployeeModel model, Address addressEntity);
        User MapFrom(EmployeeModel model, User userEntity);
        IEnumerable<EmployeeModel> MapFrom(IEnumerable<Employee> employees);
    }
}