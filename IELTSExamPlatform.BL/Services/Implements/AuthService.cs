using IELTSExamPlatform.BL.DTOs.Auth;
using IELTSExamPlatform.BL.ResponceObject;
using IELTSExamPlatform.BL.ResponceObject.Enums;
using IELTSExamPlatform.BL.Services.Abstractions;
using IELTSExamPlatform.CORE.Entities; 
using Microsoft.AspNetCore.Identity;

namespace IELTSExamPlatform.BL.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Response> LoginAsync(LoginDto user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.UsernameOrEmail);

            if (existingUser == null)
                existingUser = await _userManager.FindByNameAsync(user.UsernameOrEmail);

            if (existingUser == null)
                return new Response(ResponseStatusCode.Error, "Username/Email or password is incorrect.");

            if (await _userManager.IsLockedOutAsync(existingUser))
                return new Response(ResponseStatusCode.Error, "Account is locked due to too many failed login attempts. Please contact support to unlock your account.");

            var result = await _signInManager.CheckPasswordSignInAsync(existingUser, user.Password, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                return new Response(ResponseStatusCode.Success, "Login successful.");
            }
            else if (result.IsLockedOut)
            {
                return new Response(ResponseStatusCode.Error, "Account is locked due to too many failed login attempts. Please contact support to unlock your account.");
            }
            else
            {
                return new Response(ResponseStatusCode.Error, "Username/Email or password is incorrect.");
            }
        }


        public async Task<Response> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return new Response(ResponseStatusCode.Success, "Logout successful.");
        }
    }
}
