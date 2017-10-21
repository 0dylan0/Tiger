using Core.Domain.Common;
using Core.Exceptions;
using Data.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace Data
{
    public class IMObjectContext : DbContext, IDbContext
    {
        static IMObjectContext()
        {
            Database.SetInitializer<IMObjectContext>(null);
        }

        public IMObjectContext()
            : this("KunlunConnection")
        {
        }

        public IMObjectContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
#if DEBUG
            Database.Log = ConsoleSqlLogger.Write;
#endif
            //目前这样写死，会造成没有超时
            Database.CommandTimeout = 60 * 60;
        }

        public virtual new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        [Obsolete("通过 SqlQuery<TElement> 来进行替代")]
        public virtual IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("Not support parameter type");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        commandText += " output";
                    }
                }
            }

            List<TEntity> result = Database.SqlQuery<TEntity>(commandText, parameters).ToList();

            return result;
        }

        //TODO:为什么延迟加载会报错？
        /// <summary>
        /// 执行SQL命令并返内容
        /// </summary>
        /// <typeparam name="TElement">返回的类型</typeparam>
        /// <param name="sql">需要执行的SQL语句，需要填写参数。如：EXEC sp_dic_validator @code, @table_name, @result OUTPUT</param>
        /// <param name="parameters">参数列表，若包含输出参数，返回值在语句执行之后可以通过对应Parameter的Value取出</param>
        /// <returns>指定类型的集合</returns>
        public virtual IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<TElement>(sql, parameters).ToList();
        }

        /// <summary>
        /// 执行SQL命令而不返内容
        /// </summary>
        /// <param name="sql">需要执行的SQL语句，需要填写参数。如：EXEC sp_generate_dt @start, @finish</param>
        /// <param name="doNotEnsureTransaction"></param>
        /// <param name="timeout">超时时间，以秒为单位。超时将引发 SqlException：Timeout 时间已到</param>
        /// <param name="parameters">参数列表，若包含输出参数，返回值在语句执行之后可以通过对应Parameter的Value取出</param>
        /// <returns>影响的行数</returns>
        public virtual int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            TransactionalBehavior transactionalBehavior = doNotEnsureTransaction ? TransactionalBehavior.DoNotEnsureTransaction : TransactionalBehavior.EnsureTransaction;
            int result = Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            return result;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="action">存储过程包含的操作</param>
        public virtual bool ExecuteTransaction(Action action)
        {
            bool result;
            using (var dbContextTransaction = this.Database.BeginTransaction())
            {
                try
                {

                    action();
                    dbContextTransaction.Commit();
                    result = true;
                }
                catch (Exception e)
                {
                    if (e is CouponException)
                    {
                        throw e;
                    }
                    dbContextTransaction.Rollback();
                    result = false;
                }

            }
            return result;
        }

        public override int SaveChanges()
        {
            return SaveChanges(true, false);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(type => !String.IsNullOrEmpty(type.Namespace)).Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            var dicMapTypes = typesToRegister.Where(p => p.BaseType.GetGenericArguments()[0].BaseType == typeof(BaseDicEntity));
            foreach (var dicMapType in dicMapTypes)
            {
                var type = dicMapType.BaseType.GetGenericArguments()[0];
                foreach (var property in type.GetProperties())
                {
                    var declaringType = property.DeclaringType;
                    modelBuilder.Properties<string>().Where(p => p.Name == "Code" && p.DeclaringType == declaringType).Configure(p => p.HasColumnName("code").IsKey());
                    modelBuilder.Properties<string>().Where(p => p.Name == "Name" && p.DeclaringType == declaringType).Configure(p => p.HasColumnName("name"));
                    modelBuilder.Properties<int>().Where(p => p.Name == "Seq" && p.DeclaringType == declaringType).Configure(p => p.HasColumnName("sort_id"));
                }
            }

            modelBuilder.Properties<byte[]>().Where(p => p.Name == "Version").Configure(p => p.IsConcurrencyToken().IsRequired());
            modelBuilder.Properties<string>().Where(p => p.Name == "InsertUser").Configure(p => p.HasColumnName("insert_user"));
            modelBuilder.Properties<string>().Where(p => p.Name == "UpdateUser").Configure(p => p.HasColumnName("update_user"));
            modelBuilder.Properties<DateTime>().Where(p => p.Name == "InsertDate").Configure(p => p.HasColumnName("insert_date"));
            modelBuilder.Properties<DateTime>().Where(p => p.Name == "UpdateDate").Configure(p => p.HasColumnName("update_date"));

            base.OnModelCreating(modelBuilder);
        }

        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public int SaveChanges(bool manageConcurrency)
        {
            return SaveChanges(manageConcurrency, false);
        }

        public int SaveChanges(bool manageConcurrency, bool storeWins)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;
            objectContext.DetectChanges();
            IEnumerable<ObjectStateEntry> objectEntries = objectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted);
            foreach (ObjectStateEntry entry in objectEntries)
            {
                var entity = entry.Entity as BaseVersionedEntity;

                if (entity == null)
                {
                    continue;
                }

                if (entity.Version == null)
                {
                    entity.Version = new byte[] { 1, 0, 0, 0 };
                    continue;
                }

                int b = BitConverter.ToInt32(entity.Version, 0);
                entity.Version = BitConverter.GetBytes(++b);
            }

            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!manageConcurrency)
                {
                    throw;
                }
                objectEntries = objectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Modified);
                foreach (ObjectStateEntry entry in objectEntries)
                {
                    objectContext.Refresh(storeWins ? RefreshMode.StoreWins : RefreshMode.ClientWins, entry.Entity);
                }
                return SaveChanges();
            }
        }
    }
}

