using Dapper;
using Core;
using System;
using System.Data;
using System.Linq;

namespace Services.Security
{
    public class PermissionService
    {
        private readonly IWorkContext _workContext;
        private readonly IDbConnection _context;

        public PermissionService(IWorkContext workContext, IDbConnection context)
        {
            _workContext = workContext;
            _context = context;
        }

        public virtual bool Authorize(string permission)
        {
            return Authorize(_workContext.CurrentUser.Code, permission);
        }

        /// <summary>
        /// 通过存储过程 sp_check_right_menu 判断用户权限
        /// </summary>
        /// <param name="userCode">User Code</param>
        /// <param name="rightCode">Right Code</param>
        /// <returns>true：有权限； false：没有权限</returns>
        public bool Authorize(string userCode, string rightCode)
        {
            userCode = userCode.Trim();
            rightCode = rightCode.Trim();
            var spCheckRightResult = _context.Query<int>("EXEC sp_check_right @user_code, @right_code", new { user_code = userCode, right_code = rightCode });

            if (spCheckRightResult.ElementAt(0) == 0)
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(spCheckRightResult.FirstOrDefault());
            }
        }
    }
}

