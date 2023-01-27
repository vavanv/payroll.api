using System;

using Microsoft.EntityFrameworkCore;

namespace Payroll2.Api.Environment.DataAccess.EnvironmentDbContext
{
    public interface IEnvironmentContextFactory
    {
        DbContext GetContext();
    }
}