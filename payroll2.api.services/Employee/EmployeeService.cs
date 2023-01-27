using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Payroll2.Api.DataAccess.Enums;
using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.DataAccess.Repository;
using Payroll2.Api.DataAccess.UnitOfWork;
using Payroll2.Api.Framework.Exception;
using Payroll2.Api.Services.Common;

namespace Payroll2.Api.Services.Employee
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepository<DataAccess.PayrollEntities.Address> _addressRepository;
        private readonly IRepository<DataAccess.PayrollEntities.Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;

        public EmployeeService(IRepository<DataAccess.PayrollEntities.Employee> employeeRepository,
            IRepository<DataAccess.PayrollEntities.Address> addressRepository,
            IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _addressRepository = addressRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<DataAccess.PayrollEntities.Employee> GetEmployeeById(int id)
        {
            var employee = _employeeRepository.FindOne(e => e.Id == id);
            if (employee == null) throw new BusinessException($"Employee with id [{id}] does not exist");

            return employee;
        }

        public void DeleteEmployeeById(int id, bool isDeleted = true)
        {
            var employee = _employeeRepository.FindOne(e => e.Id == id).GetAwaiter().GetResult();
            employee.IsDeleted = isDeleted;
            _employeeRepository.Update(employee);
            _unitOfWork.SaveChanges();
        }

        public async Task<DataAccess.PayrollEntities.Employee> GetEmployeeByUserId(int userId)
        {
            var employee = await _employeeRepository.FindOne(u => u.UserId == userId);
            if (employee == null) throw new BusinessException($"Employee with User Id [{userId}] does not exist");

            return employee;
        }

        public Task<ICollection<DataAccess.PayrollEntities.Employee>> GetEmployees()
        {
            var employee = _employeeRepository.FindAll(x => !x.IsDeleted);

            return employee;
        }

        public void UpdateEmployee(DataAccess.PayrollEntities.Employee entity)
        {
            _employeeRepository.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public void UpdateEmployee(DataAccess.PayrollEntities.Employee employee,
            DataAccess.PayrollEntities.Address address, User user)
        {
            using (var context = _unitOfWork.Context)
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (user.Id == 0)
                        {
                            user.RightId = (int) Right.Employee;
                            user.Username = employee.FirstName.Substring(0, 1) + employee.LastName.Substring(0, 1) +
                                            "001";
                            user.Password = HashPassword.GetHashPassword(user.Username);
                        }

                        _userRepository.Update(user);
                        _addressRepository.Update(address);
                        context.SaveChanges();

                        employee.UserId = user.Id;
                        employee.AddressId = address.Id;
                        _employeeRepository.Update(employee);

                        context.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new BusinessException(ex.Message);
                    }
                }
            }
        }
    }
}