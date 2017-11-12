using Core.Domain;
using Services.Users;
using System;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Core.Security;

namespace Services.Security
{
    public class ClaimsAuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly UserService _userService;

        private Core.Domain.Common.Users _cachedUser;

        private readonly IAuthenticationManager _authenticationManager;

        #endregion

        #region Ctor

        public ClaimsAuthenticationService(UserService userService, IAuthenticationManager authenticationManager)
        {
            _userService = userService;
            _authenticationManager = authenticationManager;
        }

        #endregion

        #region Methods

        public void SignIn(Core.Domain.Common.Users user, bool createPersistentCookie)
        {
            var applicationIdentity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            applicationIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier,user.Name ));
            applicationIdentity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));

            // Cookie
            var authProerties = new AuthenticationProperties { IsPersistent = createPersistentCookie };
            if (authProerties.IsPersistent)
            {
                var currentUtc = new SystemClock().UtcNow;
                authProerties.IssuedUtc = currentUtc;
                authProerties.ExpiresUtc = currentUtc.Add(TimeSpan.FromDays(30));
            }
            _authenticationManager.SignIn(authProerties, applicationIdentity);
            _cachedUser = user;
        }

        public void SignOut()
        {
            _cachedUser = null;
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public Core.Domain.Common.Users GetAuthenticatedUser()
        {
            if (_cachedUser != null)
            {
                return _cachedUser;
            }

            if (!(_authenticationManager?.User.Identity is ClaimsIdentity))
            {
                return null;
            }

            var formsIdentity = (ClaimsIdentity)_authenticationManager.User.Identity;
            Core.Domain.Common.Users user = GetAuthenticatedUserFromClaims(formsIdentity);
            if (user != null)
            {
                _cachedUser = user;
            }

            return _cachedUser;
        }

        public virtual Core.Domain.Common.Users GetAuthenticatedUserFromClaims(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            string code = identity.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrWhiteSpace(code))
            {
                return null;
            }
            var user = _userService.GetByCode(code);
            return user;
        }

        /// <summary>
        /// 获取当前调用 API 的客户端信息
        /// </summary>
        /// <returns>当前调用 API 的客户端，可能为 null</returns>
        public IdentityServerClient GetClient()
        {
            var user = _authenticationManager?.User;

            if (user == null)
            {
                return null;
            }

            return new IdentityServerClient(user);
        }

        #endregion
    }
}
