using System;

using Microsoft.EntityFrameworkCore;

using Payroll2.Api.Environment.Services.TenantProvider;

namespace Payroll2.Api.DataAccess.PayrollDbContext
{
    public class PayrollContextFactory : IPayrollContextFactory
    {
        private readonly DbContext _context;

        public PayrollContextFactory(IDatabaseTenantProvider provider)
        {
            _context = new PayrollDbContext(provider.GetTenant().Connection);
        }

        public DbContext GetContext()
        {
            return _context;
        }
    }
}