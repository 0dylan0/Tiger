using EntityFramework.BulkInsert.Extensions;
using Core;
using Core.Data;
using Core.Domain.Common;
using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;

namespace Data
{
    public class Repository<T> : IRepository<T>
       where T : BaseEntity
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        public Repository(IDbContext context)
        {
            _context = context;
        }

        public virtual T GetById(params object[] id)
        {
            return Entities.Find(id);
        }

        public virtual void Insert(T entity, bool manageConcurrency = true, bool storeWins = false)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                IWorkContext workContext = EngineContext.Current.Resolve<IWorkContext>();
                var type = typeof(T);
                type.GetProperty("InsertDate")?.SetValue(entity, DateTime.Now);
                type.GetProperty("UpdateDate")?.SetValue(entity, DateTime.Now);
                type.GetProperty("InsertUser")?.SetValue(entity, workContext.CurrentUserCode);
                type.GetProperty("UpdateUser")?.SetValue(entity, workContext.CurrentUserCode);

                Entities.Add(entity);

                SaveChanges(manageConcurrency, storeWins);
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public virtual int InsertBatch(IEnumerable<T> entities, bool disabledAutoDetect = true, bool manageConcurrency = true, bool storeWins = false)
        {
            var context = _context as IMObjectContext;

            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                if (disabledAutoDetect)
                {
                    // 不进行 DetectChanges 以提升批量插入的性能
                    context.Configuration.AutoDetectChangesEnabled = false;
                }

                IWorkContext workContext = EngineContext.Current.Resolve<IWorkContext>();
                foreach (var entity in entities)
                {
                    var type = typeof(T);
                    type.GetProperty("InsertDate")?.SetValue(entity, DateTime.Now);
                    type.GetProperty("UpdateDate")?.SetValue(entity, DateTime.Now);
                    type.GetProperty("InsertUser")?.SetValue(entity, workContext.CurrentUserCode);
                    type.GetProperty("UpdateUser")?.SetValue(entity, workContext.CurrentUserCode);
                }

                (Entities as DbSet<T>).AddRange(entities);

                return SaveChanges(manageConcurrency, storeWins);
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
            finally
            {
                context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public virtual void Update(T entity, bool manageConcurrency = true, bool storeWins = false)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                IWorkContext workContext = EngineContext.Current.Resolve<IWorkContext>();
                var type = typeof(T);
                type.GetProperty("UpdateDate")?.SetValue(entity, DateTime.Now);
                type.GetProperty("UpdateUser")?.SetValue(entity, workContext.CurrentUserCode);

                SaveChanges(manageConcurrency, storeWins);
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public virtual int UpdateBatch(IEnumerable<T> entities, bool manageConcurrency = true, bool storeWins = false)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }

                IWorkContext workContext = EngineContext.Current.Resolve<IWorkContext>();
                foreach (var entity in entities)
                {
                    var type = typeof(T);
                    type.GetProperty("UpdateDate")?.SetValue(entity, DateTime.Now);
                    type.GetProperty("UpdateUser")?.SetValue(entity, workContext.CurrentUserCode);
                }

                return SaveChanges(manageConcurrency, storeWins);
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public virtual void Delete(T entity, bool manageConcurrency = true, bool storeWins = false)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                Entities.Remove(entity);

                SaveChanges(manageConcurrency, storeWins);
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public virtual void DeleteBatch(IEnumerable<T> entities, bool manageConcurrency = true, bool storeWins = false)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entity");
                }
                (Entities as DbSet<T>).RemoveRange(entities);

                SaveChanges(manageConcurrency, storeWins);
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public virtual void BulkInsert(IEnumerable<T> entities, bool manageConcurrency = true, bool storeWins = false)
        {
            IWorkContext workContext = EngineContext.Current.Resolve<IWorkContext>();
            foreach (var entity in entities)
            {
                var type = typeof(T);
                type.GetProperty("InsertDate")?.SetValue(entity, DateTime.Now);
                type.GetProperty("UpdateDate")?.SetValue(entity, DateTime.Now);
                type.GetProperty("InsertUser")?.SetValue(entity, workContext.CurrentUserCode);
                type.GetProperty("UpdateUser")?.SetValue(entity, workContext.CurrentUserCode);
            }
            (_context as IMObjectContext).BulkInsert(entities);
            SaveChanges(manageConcurrency, storeWins);
        }

        public virtual void BulkInsert(IEnumerable<T> entities, IDbTransaction transaction, bool manageConcurrency = true, bool storeWins = false)
        {
            IWorkContext workContext = EngineContext.Current.Resolve<IWorkContext>();
            foreach (var entity in entities)
            {
                var type = typeof(T);
                type.GetProperty("InsertDate")?.SetValue(entity, DateTime.Now);
                type.GetProperty("UpdateDate")?.SetValue(entity, DateTime.Now);
                type.GetProperty("InsertUser")?.SetValue(entity, workContext.CurrentUserCode);
                type.GetProperty("UpdateUser")?.SetValue(entity, workContext.CurrentUserCode);
            }
           (_context as IMObjectContext).BulkInsert(entities, transaction, SqlBulkCopyOptions.Default, entities.Count());
            SaveChanges(manageConcurrency, storeWins);
        }

        private int SaveChanges(bool manageConcurrency, bool storeWins)
        {
            return _context.SaveChanges(manageConcurrency, storeWins);
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }
    }
}