using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DbHelper
    {
        private readonly IDbConnection _context;

        public DbHelper(IDbConnection context)
        {
            _context = context;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="action">存储过程包含的操作</param>
        public bool ExecuteTransaction(Action<IDbTransaction> action)
        {
            if (_context.State != ConnectionState.Open)
            {
                _context.Open();
            }
            using (var dbContextTransaction = _context.BeginTransaction())
            {
                try
                {
                    action(dbContextTransaction);
                    dbContextTransaction.Commit();
                    _context.Close();
                }
                catch (Exception exception)
                {
                    dbContextTransaction.Rollback();
                    _context.Close();
                    return false;
                }
            }
            return true;
        }
    }
}
