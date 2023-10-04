using System;
using System.Threading.Tasks;
using System.Transactions;

using Payroll2.Api.DataAccess.Enums;
using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.DataAccess.Repository;
using Payroll2.Api.DataAccess.UnitOfWork;
using Payroll2.Api.Framework.Exception;
using Payroll2.Api.Services.Common;

namespace Payroll2.Api.Services.Users
{
    internal sealed class UserService : IUserService
    {
        private readonly IRepository<DataAccess.PayrollEntities.Employee> _employee;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _users;

        public UserService(IRepository<User> users, IRepository<DataAccess.PayrollEntities.Employee> employee,
            IUnitOfWork unitOfWork)
        {
            _users = users;
            _employee = employee;
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserByUsernamePassword(string username, string password)
        {
            var hash = HashPassword.GetHashPassword(password);

            var user = await _users.FindOne(u => u.Username == username && u.Password == hash);
            if (user == null || !user.Username.Equals(username, StringComparison.Ordinal))
                throw new BusinessException("Username/password is not correct");

            if (!user.Enabled) throw new BusinessException("User is disabled. Please contact your administrator.");

            return user;
        }

        public async Task FixPassword()
        {
            try
            {
                using var scope = new TransactionScope();
                await using (var context = _unitOfWork.Context)
                {
                    foreach (var u in await _users.All())
                    {
                        var openedPassword = u.Password;
                        u.Password = HashPassword.GetHashPassword(openedPassword);
                        _users.Update(u);
                    }

                    context.SaveChanges();
                }

                scope.Complete();
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _users.FindOne(u => u.Id == id);
            if (user == null) throw new BusinessException($"User with id [{id}] does not exist");

            if (user.RightId == (int) Right.Employee)
            {
                var employee = await _employee.FindOne(e => e.UserId == user.Id);
                if (employee == null) throw new BusinessException($"employee with UserId [{id}] does not exist");
            }

            return user;
        }
    }
}
