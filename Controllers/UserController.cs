using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MvcSocial.Models;

namespace MvcSocial.Controllers
{
    //[Route("/User")]
    [Authorize(Roles = "Admin")]
    public class UserController() : Controller
    {
        private readonly IUserRepository _userRepository = UserRepository.Instance;
        [HttpGet]
        [Authorize]
        public async Task<List<string>> Get()
        {
            return await _userRepository.GetUserNames();
        }
        
        // GET: User
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            return View(_userRepository.Users);
        }
        
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Login")] User user)
        {
            user.DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture);
            ModelState["DateOfCreation"]!.ValidationState = ModelValidationState.Valid;

            if (_userRepository.Users.Find(u => u.Login == user.Login) != null)
                ModelState.AddModelError("Login", "Not Unique");

            if (!ModelState.IsValid) return View(user);
            _userRepository.Users.Add(user);
            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Del(string? id)
        {
            if (id == null)
                return Json(false);

            var user = _userRepository.Users
                .FirstOrDefault(m => m.Login == id);
            if (user == null)
                return Json(false);

            _userRepository.Users.Remove(user);
            return RedirectToAction(nameof(Index));
        }

        [Route("/Init")]
        public IActionResult Init()
        {
            _userRepository.Users.AddRange([
                new User
                {
                    Login = "alcBond",
                    DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                },
                new User
                {
                    Login = "user123",
                    DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                },
                new User
                {
                    Login = "plzKillMe",
                    DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                },
                new User
                {
                    Login = "user1",
                    DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                },
                new User
                {
                    Login = "user2",
                    DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                },
                new User
                {
                    Login = "user3",
                    DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                },
                new User
                {
                    Login = "user4",
                    DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                },
                new User
                {
                    Login = "user5",
                    DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                },
                new User
                {
                    Login = "user6",
                    DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                },
                new User
                {
                    Login = "user7",
                    DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                },
                new User
                {
                    Login = "user8",
                    DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                },
                new User
                {
                    Login = "user9",
                    DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                },
                new User
                {
                    Login = "user10",
                    DateOfCreation = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                },
            ]);
            
            var random = new Random();
            foreach (var user in _userRepository.Users)
            {
                var numberOfFriends = random.Next(2, 6);
                var potentialFriends = _userRepository.Users.Where(u => u != user).OrderBy(_ => random.Next()).Take(numberOfFriends).ToList();
                user.Friends.AddRange(potentialFriends);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
