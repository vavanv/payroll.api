using System;
using System.Threading.Tasks;

using Payroll2.Api.DataAccess.Repository;
using Payroll2.Api.DataAccess.UnitOfWork;
using Payroll2.Api.Framework.Exception;

namespace Payroll2.Api.Services.Address
{
    internal sealed class AddressService : IAddressService
    {
        private readonly IRepository<DataAccess.PayrollEntities.Address> _address;
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(IRepository<DataAccess.PayrollEntities.Address> address, IUnitOfWork unitOfWork)
        {
            _address = address;
            _unitOfWork = unitOfWork;
        }

        public async Task<DataAccess.PayrollEntities.Address> GetAddressById(int id)
        {
            var address = await _address.FindOne(a => a.Id == id);
            if (address == null) throw new BusinessException($"Address with id [{id}] does not exist");

            return address;
        }

        public void AddAddress(DataAccess.PayrollEntities.Address entity)
        {
            _address.Add(entity);
            _unitOfWork.SaveChanges();
        }

        public void UpdateAddress(DataAccess.PayrollEntities.Address entity)
        {
            _address.Update(entity);
            _unitOfWork.SaveChanges();
        }
    }
}