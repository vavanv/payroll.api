using System;

using Microsoft.EntityFrameworkCore;

namespace Payroll2.Api.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }
        void SaveChangesAsync();
        void SaveChanges();
    }
}