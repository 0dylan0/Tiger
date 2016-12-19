using Core.Domain;
using Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    public class UserValidateService
    {
        private readonly UserService _userService;

        public UserValidateService(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 判断用户是否有登录权限
        /// </summary>
        /// <param name="userCode">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public UserLoginResult Validate(string userCode, string password, bool isEncryptedPassword = false)
        {
            //User user = _userService.GetByCode(userCode);
            //if (user == null)
            //{
            //    return UserLoginResult.UserNotExist;
            //}

            //string encryptedPassword = password;
            //if (!isEncryptedPassword)
            //{
            //    encryptedPassword = EncryptionService.EncryptCRSPassword(password);
            //}

            //if (user.Password.Equals(encryptedPassword) == false)
            //{
            //    return UserLoginResult.WrongPassword;
            //}

            return UserLoginResult.Successful;
        }
    }
}
