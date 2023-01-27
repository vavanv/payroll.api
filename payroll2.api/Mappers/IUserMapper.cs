using System;

using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.Models;

namespace Payroll2.Api.Mappers
{
    public interface IUserMapper
    {
        UserModel MapFrom(User itemEntity);
    }
}