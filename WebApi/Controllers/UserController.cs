using Core.Domain.Common;
using Core.Enum;
using WebApi.Extensions;
using Services.Common;
using Services.Localization;
using Services.Security;
using Web.Framework.WebAPI.Controllers;
using Web.Framework.WebAPI.Models;
using Microsoft.Web.Http;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebApi.Models;
using Services.Users;

namespace WebApi.Controllers
{
    /// <summary>
    /// 用户协议接口集,所有关于用户的增删改查等操作全都在这里了，以后跟用户相关的接口也会加在这里。
    /// </summary>
    [RoutePrefix("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class UserController : BaseApiController
    {
        static List<UserEditPasswordModel> userEditPasswordMockList = initUserEditPasswordMockDataList();

        private static List<UserEditPasswordModel> initUserEditPasswordMockDataList()
        {
            return new List<UserEditPasswordModel>()
            {
                new UserEditPasswordModel {Code="1",UserName="Product A",Password="1000000"},
                new UserEditPasswordModel {Code="2",UserName="Product B",Password="200000"},
                new UserEditPasswordModel {Code="3",UserName="Product C",Password="500000"},
                new UserEditPasswordModel {Code="4",UserName="Product D",Password="80000"},
                new UserEditPasswordModel {Code="5",UserName="Product E",Password="300000"}
            };
        }

        //private readonly UserService _userService;
        //public UserController(UserService userService)
        //{
        //    _userService = userService;
        //}

        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="request">注册信息</param>
        [HttpGet]
        [Route("user")]
        //[SwaggerResponseRemoveDefaults]
        public IHttpActionResult Users()
        {

            return Ok(new Core.Domain.Common.Users { Code = "haha" });
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            return Ok(new Core.Domain.Common.Users { Code = "delete" });
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns>所有用户信息</returns>
        [HttpGet]
        [Route("user/Get")]
        public IHttpActionResult Get()
        {
            //List<Core.Domain.Common.Users> list = _userService.GetAll().ToList();
            //var model = list.MapTo<List<Core.Domain.Common.Users>, List<UserEditPasswordModel>>();

            return Ok();
            //return Json<List<UserEditPasswordModel>>(model);



        }
        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="id"></param>
        /// <returns>dfssdf</returns>
        public IHttpActionResult Get(int id)
        {
            return Ok(new Core.Domain.Common.Users { Code = "haha" });
        }

        // GET api/products
        public IEnumerable<UserEditPasswordModel> GetAllProducts()
        {
            return userEditPasswordMockList;
        }

        public UserEditPasswordModel GetProdcut(string id)
        {
            return userEditPasswordMockList.Where(p => p.Code == id).FirstOrDefault();
        }
    }
}
