using System;

using Microsoft.EntityFrameworkCore;

namespace Payroll2.Api.DataAccess.PayrollDbContext
{
    public interface IPayrollContextFactory
    {
        DbContext GetContext();
    }
}