using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Data
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

        int SaveChanges(bool manageConcurrency);

        int SaveChanges(bool manageConcurrency, bool storeWins);

        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new();

        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        bool ExecuteTransaction(Action action);
    }
}
