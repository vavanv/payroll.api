using System;

using Microsoft.EntityFrameworkCore;

using Payroll2.Api.DataAccess.PayrollDbContext;

namespace Payroll2.Api.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed;

        public UnitOfWork(IPayrollContextFactory contextFactory)
        {
            Context = contextFactory.GetContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChangesAsync()
        {
            Context?.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            Context?.SaveChanges();
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