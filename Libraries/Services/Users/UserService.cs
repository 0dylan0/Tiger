﻿using Core.Domain;
using Core.Domain.Common;
using Core.Page;
using Dapper;
using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    public class UserService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public UserService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }

        public void UpdatePassword(string code, string password)
        {
            _context.Execute("Update users set password=@password where code=code", new { code = code, password = password });
        }

        public void Insert(Core.Domain.Common.Users user)
        {
            var sql = $@"insert into Users(
                    [Name],
                    [Password])
			        VALUES (
                    @Name,
                    @Password)";


            _context.Execute(sql, new
            {
                Name = user.Name,
                Password = user.Password
            });
        }

        public void Update(Core.Domain.Common.Users user)
        {
            var sql = $@"update Users set
                    [Name] =@Name,
                    [Password]=@Password
                    where ID=@ID";

            _context.Execute(sql, new
            {
                Name = user.Name,
                Password = user.Password,
                ID = user.ID
            });

        }

        public Core.Domain.Common.Users GetUserById(int id)
        {
            var sql = @"select * from Users  where id = @id";

            return _context.QuerySingle<Core.Domain.Common.Users>(sql, new
            {
                id = id
            });
        }

        public IEnumerable<Core.Domain.Common.Users> GetAll()
        {
            var sql = @"select * from Users";
            return _context.Query<Core.Domain.Common.Users>(sql,
                new { }
                );
        }

        public IPagedList<Core.Domain.Common.Users> GetList(string textQuery, int pageIndex = 0, int pageSize = 2147483647, string sortExpression = "")
        {
            string sql = @"select * from Users ";

            var Parameter = new DynamicParameters();
            if (!string.IsNullOrEmpty(textQuery))
            {
                sql += " where Name like @textQuery";
                textQuery = textQuery.Contains("%") ? textQuery : $"%{textQuery}%";
                Parameter.Add("textQuery", textQuery);
            }

            return new SqlPagedList<Core.Domain.Common.Users>(sql, Parameter, pageIndex, pageSize, sortExpression);
        }

        public Core.Domain.Common.Users GetByCode(string Name)
        {
            string sql = @" select ID,Name,Password from Users where Name = @Name ";
            //return _context.QuerySingle<Core.Domain.Common.Users>(sql, new { Name = Name });
            return _context.QuerySingleOrDefault<Core.Domain.Common.Users>(sql, new { Name = Name });
        }

        public void Delete(int id)
        {
            var sql = $@"delete from Users where id=@id";

            _context.Execute(sql, new { id = id });
        }


    }
}
