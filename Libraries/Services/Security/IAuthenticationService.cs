using Core.Domain;
using Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Security
{
    public interface IAuthenticationService
    {
        void SignIn(Core.Domain.Common.Users user, bool createPersistentCookie);

        void SignOut();

        Core.Domain.Common.Users GetAuthenticatedUser();

        IdentityServerClient GetClient();
    }
}
