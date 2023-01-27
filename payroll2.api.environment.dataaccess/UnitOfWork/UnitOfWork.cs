using System;

using Microsoft.EntityFrameworkCore;

using Payroll2.Api.Environment.DataAccess.EnvironmentDbContext;

namespace Payroll2.Api.Environment.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed;

        public UnitOfWork(IEnvironmentContextFactory contextFactory)
        {
            Context = contextFactory.GetContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            Context?.SaveChangesAsync();
        }

        public DbContext Context { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    Context.Dispose();

            _disposed = true;
        }
    }
}