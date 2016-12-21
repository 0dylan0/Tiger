using Core;
using Core.Domain.Common;
using Core.Infrastructure;
using Dapper;
using Dommel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DapperRepository
    {
        private readonly IDbConnection _context;

        public DapperRepository(IDbConnection context)
        {
            _context = context;
        }

        public int ExecuteSqlCommand(string sql, DynamicParameters parameters = null)
        {
            return _context.Execute(sql, parameters);
        }

        public void Insert<T>(T entity) where T : class, new()
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            IWorkContext workContext = EngineContext.Current.Resolve<IWorkContext>();
            var type = typeof(T);
            type.GetProperty("InsertDate")?.SetValue(entity, DateTime.Now);
            type.GetProperty("UpdateDate")?.SetValue(entity, DateTime.Now);
            type.GetProperty("InsertUser")?.SetValue(entity, workContext.CurrentUser?.ID);
            type.GetProperty("UpdateUser")?.SetValue(entity, workContext.CurrentUser?.ID);

            _context.Insert(entity);
            //var sqlGenerator = EngineContext.Current.Resolve<ISqlGenerator<T>>();
            //var sql = sqlGenerator.GetInsert();

            //if (sqlGenerator.IsIdentity)
            //{
            //    var newId = _context.Query<decimal>(sql, entity).Single();

            //    if (newId > 0)
            //    {
            //        var newParsedId = Convert.ChangeType(newId, sqlGenerator.IdentityProperty.PropertyInfo.PropertyType);
            //        sqlGenerator.IdentityProperty.PropertyInfo.SetValue(entity, newParsedId);
            //    }
            //}
            //else
            //{
            //    _context.Execute(sql, entity);
            //}
        }

        public void Update<T>(T entity) where T : new()
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            IWorkContext workContext = EngineContext.Current.Resolve<IWorkContext>();
            var type = typeof(T);
            type.GetProperty("UpdateDate")?.SetValue(entity, DateTime.Now);
            type.GetProperty("UpdateUser")?.SetValue(entity, workContext.CurrentUser?.ID);

            //var sqlGenerator = EngineContext.Current.Resolve<ISqlGenerator<T>>();
            //var query = sqlGenerator.GetUpdate();
            //_context.Execute(query, entity);
            _context.Update(entity);
        }

        public void Delete<T>(T instance) where T : new()
        {
            //var sqlGenerator = EngineContext.Current.Resolve<ISqlGenerator<T>>();
            //var query = sqlGenerator.GetDelete();
            _context.Delete(instance);
        }

        public virtual int InsertBatch<T>(IEnumerable<T> entities) where T : class, new()
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            IWorkContext workContext = EngineContext.Current.Resolve<IWorkContext>();
            foreach (var entity in entities)
            {
                var type = typeof(T);
                type.GetProperty("InsertDate")?.SetValue(entity, DateTime.Now);
                type.GetProperty("UpdateDate")?.SetValue(entity, DateTime.Now);
                type.GetProperty("InsertUser")?.SetValue(entity, workContext.CurrentUser?.ID);
                type.GetProperty("UpdateUser")?.SetValue(entity, workContext.CurrentUser?.ID);
                Insert(entity);
            }

            return 0;
        }

        //public virtual void Update(T entity,string sql, bool manageConcurrency = true, bool storeWins = false)
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("entity");
        //    }

        //    IWorkContext workContext = EngineContext.Current.Resolve<IWorkContext>();
        //    var type = typeof (T);
        //    type.GetProperty("UpdateDate")?.SetValue(entity, DateTime.Now);
        //    type.GetProperty("UpdateUser")?.SetValue(entity, workContext.CurrentUser?.Code);

        //    //_context.Execute(sql, entity);
        //    _context.Execute(sql, new {Id = -123123});
        //}

        public virtual int UpdateBatch<T>(IEnumerable<T> entities) where T : new()
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
                type.GetProperty("UpdateUser")?.SetValue(entity, workContext.CurrentUser?.ID);
                Update(entity);
            }
            return 0;
        }

        //public IEnumerable<T> GetWhere<T>(object filters) where T : new()
        //{
        //    //var sqlGenerator = EngineContext.Current.Resolve<ISqlGenerator<T>>();
        //    //var query = sqlGenerator.GetSelect(filters);
        //    return _context.Query<T>(query, filters);
        //}


        public IQueryable<T> Table<T>() where T : class, new()
        {
            //var sqlGenerator = EngineContext.Current.Resolve<ISqlGenerator<T>>();
            //var query = sqlGenerator.GetSelectAll();
            return _context.GetAll<T>().AsQueryable();
            //return _context.Query<T>(query).AsQueryable();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, DynamicParameters parameters = null)
        {
            return _context.Query<TElement>(sql, parameters);
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, object param = null)
        {
            return _context.Query<TElement>(sql, param);
        }

        public IEnumerable<T> ExecuteStoredProcedureList<T>(string commandText, DynamicParameters parameters = null) where T : new()
        {
            var result = _context.Query<T>(commandText, parameters, commandType: CommandType.StoredProcedure).ToList();
            return result;
        }

        public T GetById<T>(object filters) where T : BaseEntity, new()
        {
            //var sqlGenerator = EngineContext.Current.Resolve<ISqlGenerator<T>>();
            //var query = sqlGenerator.GetSelect(filters);
            return _context.Get<T>(filters);
            //return _context.Query<T>(query, filters).FirstOrDefault();
        }

    }
}
