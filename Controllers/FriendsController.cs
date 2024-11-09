using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcSocial.Models;
using NuGet.Protocol;

namespace MvcSocial.Controllers;

[Authorize(Roles = "Admin,User")]
public class FriendsController : Controller
{
    private readonly IUserRepository _userRepository = UserRepository.Instance;


    private User CurrentUser()
    {
        var loggedUser = HttpContext.User.Identity?.Name;
        if (loggedUser == null)
            throw new ApplicationException("Nobody is currently logged in");
        
        return _userRepository.Users.Find(u => u.Login == loggedUser) 
               ?? throw new ApplicationException("Nobody is currently logged in");
    }
        public IActionResult Index()
        {
            return View(CurrentUser().Friends);
        }

        public IActionResult List()
        {
            return Json(CurrentUser().Friends);
        }
        
        
        [ValidateAntiForgeryToken]
        public IActionResult Add(string? login)
        {
            var user = _userRepository.Users.Find(u => u.Login == login);
            if (user == null)
                return Json(false);
            
            CurrentUser().Friends.Add(user);
            return Json(true);
        }
        
        public IActionResult Del(string? id)
        {
            if (id == null)
                return Json(false);

            var user = _userRepository.Users
                .FirstOrDefault(m => m.Login == id);
            if (user == null)
                return Json(false);
            
            CurrentUser().Friends.Remove(user);
            return Json(true);
        }

    public IActionResult Export()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(CurrentUser().Friends.Select(f => f.Login).ToJson() ?? "");
        var content = new System.IO.MemoryStream(bytes);
        return File(content , "application/json", "friends.json");
    }

    public IActionResult Import(IFormFile? file)
    {
        if (file == null)
            return RedirectToAction(nameof(Index));
        
        
        var json = new StreamReader(file.OpenReadStream()).ReadToEnd();
        var logins = json.FromJson<List<string>>();

        CurrentUser().Friends = logins.Select(login => _userRepository.Users.Find(u => u.Login == login)).OfType<User>().ToList();
        return RedirectToAction(nameof(Index));
    }
}