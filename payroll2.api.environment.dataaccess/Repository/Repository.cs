using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Payroll2.Api.DataAccess.Core.Common;
using Payroll2.Api.Environment.DataAccess.UnitOfWork;

namespace Payroll2.Api.Environment.DataAccess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _context;

        public Repository(IUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public DbContext GetContext()
        {
            return _context;
        }

        public virtual void Add(TEntity instance)
        {
            _context.Set<TEntity>().Add(instance);
        }

        public virtual void Remove(TEntity instance)
        {
            _context.Set<TEntity>().Remove(instance);
        }

        public void Update(TEntity instance)
        {
            _context.Set<TEntity>().Update(instance);
        }

        public virtual async Task<TEntity> One(int id)
        {
            return await _context.Set<TEntity>().SingleAsync(e => e.Id == id);
        }

        public virtual async Task<TEntity> FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).FirstAsync();
        }

        public TEntity FindOneSync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        public virtual async Task<ICollection<TEntity>> All()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToArrayAsync();
        }

        public virtual async Task<ICollection<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public virtual async Task<ICollection<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, string>> orderBy)
        {
            return await _context.Set<TEntity>().Where(predicate).AsNoTracking().OrderBy(orderBy).ToListAsync();
        }

        public virtual async Task<int> Count()
        {
            return await _context.Set<TEntity>().AsNoTracking().CountAsync();
        }

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AsNoTracking().CountAsync(predicate);
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AsNoTracking().AnyAsync(predicate);
        }
    }
}