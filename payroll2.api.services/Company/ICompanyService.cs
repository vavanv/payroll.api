using System;
using System.Threading.Tasks;

namespace Payroll2.Api.Services.Company
{
    public interface ICompanyService
    {
        Task<DataAccess.PayrollEntities.Company> GetCompany();
        Task<DataAccess.PayrollEntities.Company> GetCompanyById(int id);
        void UpdateCompany(DataAccess.PayrollEntities.Company entity);
    }
}