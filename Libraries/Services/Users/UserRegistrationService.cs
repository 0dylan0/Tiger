using Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    public class UserRegistrationService
    {
        private readonly UserService _userService;
        private readonly EncryptionService _encryptionService;

        public UserRegistrationService(UserService userService, EncryptionService encryptionService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
        }

        public void UpdateUser(string userCode, string newPassword)
        {
            if (!String.IsNullOrWhiteSpace(newPassword))
            {
                newPassword = EncryptionService.EncryptCRSPassword(newPassword);
            }
            _userService.UpdatePassword(userCode, newPassword);
        }
    }
}
