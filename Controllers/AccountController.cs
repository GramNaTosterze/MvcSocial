using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MvcSocial.Models;

namespace MvcSocial.Controllers;

public class AccountController : Controller
{
    private readonly UserRepository _userRepository = UserRepository.Instance;

    [Route("/Login/{login?}")]
    public IActionResult Login(string? login)
    {
        if (login == null || _userRepository.Users.Find(u => u.Login == login) == null)
            throw new AuthenticationException("Invalid User");
        List<Claim> claims = [
            new Claim(ClaimTypes.Name, login),
            new Claim(ClaimTypes.Role, login == "admin" ? "Admin" : "User"),
        ];
        var identity = new ClaimsIdentity(claims: claims,
            authenticationType: "cookie");

        var user = new ClaimsPrincipal(identity: identity);

        var authProperties = new AuthenticationProperties { };

        //Sign-in the user
        HttpContext.SignInAsync("cookie", user, authProperties);
        return RedirectToAction("Index", "Friends");
    }
    

    [Route("/Logout")]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync("cookie");
        return RedirectToAction("Index", "Home");
    }
}