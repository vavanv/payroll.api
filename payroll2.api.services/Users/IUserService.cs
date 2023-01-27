using System;
using System.Threading.Tasks;

using Payroll2.Api.DataAccess.PayrollEntities;

namespace Payroll2.Api.Services.Users
{
    public interface IUserService
    {
        Task<User> GetUserByUsernamePassword(string username, string password);
        Task<User> GetUserById(int id);
        Task FixPassword();
    }
}