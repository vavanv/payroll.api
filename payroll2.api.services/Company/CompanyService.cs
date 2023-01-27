using System;
using System.Threading.Tasks;

using Payroll2.Api.DataAccess.Repository;
using Payroll2.Api.DataAccess.UnitOfWork;
using Payroll2.Api.Framework.Exception;

namespace Payroll2.Api.Services.Company
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepository<DataAccess.PayrollEntities.Company> _company;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IRepository<DataAccess.PayrollEntities.Company> company, IUnitOfWork unitOfWork)
        {
            _company = company;
            _unitOfWork = unitOfWork;
        }

        public async Task<DataAccess.PayrollEntities.Company> GetCompany()
        {
            var company = await _company.First();
            return company;
        }

        public async Task<DataAccess.PayrollEntities.Company> GetCompanyById(int id)
        {
            var company = await _company.FindOne(u => u.Id == id);
            if (company == null) throw new BusinessException($"Company with id [{id}] does not exist");

            return company;
        }

        public void UpdateCompany(DataAccess.PayrollEntities.Company entity)
        {
            _company.Update(entity);
            _unitOfWork.SaveChanges();
        }
    }
}