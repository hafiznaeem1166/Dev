using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NLP_Delivery.Data;
using System.Security.Claims;
using NLP_Delivery.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NLP_Delivery.Controllers
{
    public class AppUsersController : Controller
    {
        private readonly DataContext _context;

        public AppUsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Query the Users entity and include the Role navigation property
                var user = await _context.Users.Include(u => u.Role)
                                                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
                if (user != null)
                {
                    // Check if the Role navigation property is not null
                    if (user.Role != null)
                    {
                        // Create claims for the authenticated user
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()), // Use UserID as NameIdentifier
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(ClaimTypes.Role, user.Role.RoleName) // Assuming RoleName is a property of the Role class
                        };

                        // Create ClaimsIdentity
                        var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");

                        // Create Authentication properties
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true // Remember user authentication
                        };

                        // Sign in the user
                        //await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                        // Redirect to the original URL or a default URL after successful authentication
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        // Role is null, handle the situation accordingly
                        ModelState.AddModelError("", "User role is not defined");
                    }
                }
                else
                {
                    // Invalid credentials, display error message
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            // If login fails or validation fails, return to the login page
            return View();
        }

        [HttpGet]
        public IActionResult AddNewUser()
        {
            // Provide data for populating role dropdown in the view
            var roles = _context.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "RoleID", "RoleName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(AppUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new Users
                {
                    Username = model.Username,
                    EmailID = model.EmailID,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    RoleID = model.RoleID // Assuming RoleID is correctly bound
                };

                // Add the new user to the database
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return RedirectToAction("ListUsers", "AppUsers");
            }

            // Reload roles for the view if model state is invalid
            var roles = _context.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "RoleID", "RoleName");

            return View(model);
        }



        public IActionResult ListUsers()
        {
            var users = _context.Users.Include(u => u.Role).ToList(); // Include the Role entity when querying for Users
            return View(users); // Pass the list of users to the view
        }

        public IActionResult Edit(int id)
        {
            // Retrieve the user with the specified ID from the database
            var user = _context.Users.FirstOrDefault(u => u.UserID == id);

            if (user == null)
            {
                return NotFound(); // Handle the case where the user with the specified ID is not found
            }

            // Retrieve the list of roles from the database
            var roles = _context.Roles.Select(r => new SelectListItem
            {
                Value = r.RoleID.ToString(),
                Text = r.RoleName
            }).ToList();

            // Set ViewBag.Roles to the list of roles
            ViewBag.Roles = roles;

            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AppUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing user from the database
                var existingUser = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserID == model.UserID);

                if (existingUser == null)
                {
                    return NotFound(); // Handle the case where the user with the specified ID is not found
                }

                // Update the existing user's properties with the new values
                existingUser.Username = model.Username;
                existingUser.Password = model.Password;
                existingUser.EmailID = model.EmailID;
                existingUser.PhoneNumber = model.PhoneNumber;
                existingUser.RoleID = model.RoleID;

                // Save the changes to the database
                await _context.SaveChangesAsync();

                return RedirectToAction("ListUsers", "AppUsers"); // Redirect to the user list page after successful update
            }

            // If model state is invalid, return the edit form with validation errors
            return View(model);
        }

        public IActionResult ReSetPassword(int id)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserID == id);
            if (user == null)
            {
                return NotFound(); // Handle the case where the user with the specified ID is not found
            }

            // Load roles from the database
            var roles = _context.Roles.ToList();

            // Check if the user's role is loaded properly
            if (user.Role == null)
            {
                // If the role is not loaded, manually load it
                var userRole = _context.Roles.FirstOrDefault(r => r.RoleID == user.RoleID);
                if (userRole != null)
                {
                    // If the user's role is found, assign it to the user
                    user.Role = userRole;
                }
            }

            // Map the Users object to AppUserViewModel
            var appUserViewModel = new AppUserViewModel
            {
                UserID = user.UserID,
                Username = user.Username,
                Password = user.Password,
                RoleID = user.RoleID,
                PhoneNumber = user.PhoneNumber,
                EmailID = user.EmailID
            };

            // Pass user's RoleID and RoleName to the view
            ViewBag.UserRoleID = user.RoleID;
            return View(appUserViewModel); // Pass AppUserViewModel to the view
        }

        [HttpPost]
        public async Task<IActionResult> ReSetPassword(AppUserViewModel model, string oldPassword, string newPassword)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing user from the database
                var existingUser = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserID == model.UserID);

                if (existingUser == null)
                {
                    return NotFound(); // Handle the case where the user with the specified ID is not found
                }

                // Check if the old password matches the one in the database
                if (existingUser.Password != oldPassword)
                {
                    ModelState.AddModelError("OldPassword", "Incorrect old password.");
                    return View(model);
                }

                // Update the user's password with the new one
                existingUser.Password = newPassword;

                // Save the changes to the database
                await _context.SaveChangesAsync();

                return RedirectToAction("ListUsers", "AppUsers"); // Redirect to the user list page after successful update
            }

            // If model state is invalid, return the edit form with validation errors
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(); // Handle the case where the user with the specified ID is not found
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("ListUsers", "AppUsers"); // Redirect to the user list page after successful deletion
        }

    }
}
