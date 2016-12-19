using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Security
{
    public interface IAuthenticationService
    {
        void SignIn(User user, bool createPersistentCookie);

        void SignOut();

        User GetAuthenticatedUser();
    }
}
