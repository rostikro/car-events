using System.Security.Claims;
using CarEvents.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarEvents.Controllers;

public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult GoogleLogin(string returnUrl = "/")
    {
        var redirectUrl = Url.Action("GoogleCallback", "User", new { ReturnUrl = returnUrl });

        var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);

        return new ChallengeResult("Google", properties);
    }

    public async Task<IActionResult> GoogleCallback(string? returnUrl)
    {
        returnUrl = returnUrl ?? Url.Content("~/");

        var info = await _signInManager.GetExternalLoginInfoAsync();

        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
            info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        if (signInResult.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }

        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (email != null)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                };

                await _userManager.CreateAsync(user);
            }

            await _userManager.AddLoginAsync(user, info);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);
        }
        
        return View("Error");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}