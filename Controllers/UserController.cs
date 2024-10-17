using Microsoft.AspNetCore.Mvc;
using _.Models;

namespace MyApp.Namespace
{
    public class UserController : Controller
    {
        private static List<User> Users= new List<User>()
            {
                new User { Id = 1, Name = "John", Age = 30 },
                new User { Id = 2, Name = "Jane", Age = 25 },
                new User { Id = 3, Name = "Doe", Age = 40 }
            };
        public ActionResult Index()
        {
            return View(Users);
        }

        // GET: /User/ById/{id}
        [HttpGet("/User/ById/{id:int}")]
        public IActionResult GetById(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("User not found.");
            
            return View("UserDetails", user); // Assuming UserDetails.cshtml exists
        }

        // GET: /User/ByAgeRange/{min}/{max}
        [HttpGet("/User/ByAgeRange/{min:int}/{max:int}")]
        public IActionResult GetByAgeRange(int min, int max)
        {
            var filteredUsers = Users.Where(u => u.Age >= min && u.Age <= max).ToList();
            if (!filteredUsers.Any())
                return NotFound("No users found in this age range.");
            
            return View("Index", filteredUsers); // Assuming Index.cshtml displays the list of users
        }

        // GET: /User/ByName/{name}
        [HttpGet("/User/ByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var filteredUsers = Users.Where(u => u.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!filteredUsers.Any())
                return NotFound("No users found with the given name.");
            
            return View("Index", filteredUsers); // Assuming Index.cshtml displays the list of users
        }

        [HttpGet("/User/{*wildcard}")]
        public IActionResult CatchAllRoutes(string wildcard)
        {
            return Content($"This is a catch-all route. You entered: {wildcard}");
        }
    }
}


