using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security
{
    public class IdentityServerClient
    {
        private readonly ClaimsPrincipal _claimsPrincipal;

        public IdentityServerClient(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }

        /// <summary>
        /// ClientId
        /// </summary>
        public string ClientId => _claimsPrincipal.FindFirst("client_id")?.Value;

        /// <summary>
        /// 券投放渠道
        /// </summary>
        public string CouponChannel => _claimsPrincipal.FindFirst("client_CouponChannel")?.Value;

        public static IdentityServerClient Create(IPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return new IdentityServerClient(user as ClaimsPrincipal);
        }
    }
}

