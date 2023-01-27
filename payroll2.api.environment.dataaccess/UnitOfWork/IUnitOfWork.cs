using System;

using Microsoft.EntityFrameworkCore;

namespace Payroll2.Api.Environment.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }
        void SaveChanges();
    }
}