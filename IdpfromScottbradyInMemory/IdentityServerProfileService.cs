using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdpfromScottbrady
{

    //Uncommenting all commented line will help adding extra claims to the user
    public class IdentityServerProfileService : IProfileService
    {
        //private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        //private readonly UserManager<ApplicationUser> _userManager;

        public IdentityServerProfileService(
            //IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, 
            //UserManager<ApplicationUser> userManager
            )
        {
            //_claimsFactory = claimsFactory;
            //_userManager = userManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            //var user = await _userManager.FindByIdAsync(sub);
            //if (user == null)
            //{
            //    throw new ArgumentException("");
            //}

            //var principal = await _claimsFactory.CreateAsync(user);
            //var claims = principal.Claims.ToList();

            //Add more claims like this
            //claims.Add(new System.Security.Claims.Claim("MyProfileID", user.Id));

            //context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            //var user = await _userManager.FindByIdAsync(sub);
            //context.IsActive = user != null;
        }
    }
}
