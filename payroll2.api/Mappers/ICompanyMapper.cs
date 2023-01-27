using System;

using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.Models;

namespace Payroll2.Api.Mappers
{
    public interface ICompanyMapper
    {
        CompanyModel MapFrom(Company itemEntity);
        Company MapFrom(CompanyModel model, Company itemEntity);
    }
}