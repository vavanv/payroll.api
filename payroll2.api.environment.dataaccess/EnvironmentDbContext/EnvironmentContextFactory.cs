using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Payroll2.Api.Configuration;

namespace Payroll2.Api.Environment.DataAccess.EnvironmentDbContext
{
    public class EnvironmentContextFactory : IEnvironmentContextFactory
    {
        private readonly DbContext _context;

        public EnvironmentContextFactory(IOptions<EnvironmentStringKey> connectionStringKey)
        {
            _context = new EnvironmentDbContext(connectionStringKey.Value.KeyValue);
        }

        public DbContext GetContext()
        {
            return _context;
        }
    }
}