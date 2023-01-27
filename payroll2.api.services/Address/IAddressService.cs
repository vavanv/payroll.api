using System;
using System.Threading.Tasks;

namespace Payroll2.Api.Services.Address
{
    public interface IAddressService
    {
        Task<DataAccess.PayrollEntities.Address> GetAddressById(int id);
        void AddAddress(DataAccess.PayrollEntities.Address entity);
        void UpdateAddress(DataAccess.PayrollEntities.Address entity);
    }
}