using Core.Domain.Common;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Core.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(params object[] id);

        void Insert(T entity, bool manageConcurrency = true, bool storeWins = false);

        int InsertBatch(IEnumerable<T> entities, bool disabledAutoDetect = true, bool manageConcurrency = true, bool storeWins = false);

        void Update(T entity, bool manageConcurrency = true, bool storeWins = false);

        int UpdateBatch(IEnumerable<T> entities, bool manageConcurrency = true, bool storeWins = false);

        void Delete(T entity, bool manageConcurrency = true, bool storeWins = false);

        void DeleteBatch(IEnumerable<T> entities, bool manageConcurrency = true, bool storeWins = false);

        void BulkInsert(IEnumerable<T> entities, bool manageConcurrency = true, bool storeWins = false);

        void BulkInsert(IEnumerable<T> entities, IDbTransaction transaction, bool manageConcurrency = true, bool storeWins = false);

        IQueryable<T> Table { get; }

        IQueryable<T> TableNoTracking { get; }
    }
}
