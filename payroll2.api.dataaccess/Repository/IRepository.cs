﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Payroll2.Api.DataAccess.Core.Common;

namespace Payroll2.Api.DataAccess.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        DbContext GetContext();

        void Add(TEntity instance);

        void Remove(TEntity instance);

        void Update(TEntity instance);

        void Attach(TEntity instance);

        Task<TEntity> FindOne(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> First();

        Task<ICollection<TEntity>> All();

        Task<ICollection<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate);

        Task<ICollection<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, string>> inOrderBy);

        Task<int> Count();

        Task<int> Count(Expression<Func<TEntity, bool>> predicate);

        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
    }
}