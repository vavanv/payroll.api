using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Payroll2.Api.DataAccess.PayrollEntities;

namespace Payroll2.Api.Services.Employee
{
    public interface IEmployeeService
    {
        Task<DataAccess.PayrollEntities.Employee> GetEmployeeById(int id);
        void DeleteEmployeeById(int id, bool isDeleted = true);
        Task<DataAccess.PayrollEntities.Employee> GetEmployeeByUserId(int userId);
        Task<ICollection<DataAccess.PayrollEntities.Employee>> GetEmployees();
        void UpdateEmployee(DataAccess.PayrollEntities.Employee entity);

        void UpdateEmployee(DataAccess.PayrollEntities.Employee entity,
            DataAccess.PayrollEntities.Address address,
            User user);
    }
}