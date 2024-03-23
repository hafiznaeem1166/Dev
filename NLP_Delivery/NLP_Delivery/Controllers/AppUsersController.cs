using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NLP_Delivery.Data;
using System.Security.Claims;
using NLP_Delivery.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace NLP_Delivery.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppUsersController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser<int>> _userManager;
        //private readonly RoleManager<IdentityUser<int>> _roleManager;

        public AppUsersController(DataContext context, UserManager<IdentityUser<int>> userManager)
        {
            _context = context;
            _userManager = userManager;
            //_roleManager = roleManager;
        }
        

        [HttpGet]
        public IActionResult AddNewUser()
        {
            // Provide data for populating role dropdown in the view
            var user = new AppUserViewModel();
            user.AppRoles = _context.Roles.ToList();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(AppUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new IdentityUser<int>()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                };

                await _userManager.CreateAsync(newUser, model.Password);

                var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == model.RoleID);

                await _userManager.AddToRoleAsync(newUser, role.Name);

                return RedirectToAction("ListUsers", "AppUsers");
            }

            // Reload roles for the view if model state is invalid
            //var roles = _context.Roles.ToList();
            //ViewBag.Roles = new SelectList(roles, "RoleID", "RoleName");
            model.AppRoles = _context.Roles.ToList();

            return View(model);
        }



        public async Task<IActionResult> ListUsers()
        {
            var appUsersViewModel = new List<AppUserViewModel>();
            
            var users = await _context.Users.ToListAsync();

            foreach(var user in users)
            {
                appUsersViewModel.Add(new AppUserViewModel
                {
                    UserID = user.Id,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                });

            }

            return View(appUsersViewModel); // Pass the list of users to the view
        }

        public IActionResult Edit(int id)
        {
            // Retrieve the user with the specified ID from the database
            //var user = _context.Users.FirstOrDefault(u => u.UserID == id);

            //if (user == null)
            //{
            //    return NotFound(); // Handle the case where the user with the specified ID is not found
            //}

            //// Retrieve the list of roles from the database
            //var roles = _context.Roles.Select(r => new SelectListItem
            //{
            //    Value = r.RoleID.ToString(),
            //    Text = r.RoleName
            //}).ToList();

            //// Set ViewBag.Roles to the list of roles
            //ViewBag.Roles = roles;

            //return View(user);
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AppUserViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    // Retrieve the existing user from the database
            //    var existingUser = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserID == model.UserID);

            //    if (existingUser == null)
            //    {
            //        return NotFound(); // Handle the case where the user with the specified ID is not found
            //    }

            //    // Update the existing user's properties with the new values
            //    existingUser.Username = model.Username;
            //    existingUser.Password = model.Password;
            //    existingUser.EmailID = model.EmailID;
            //    existingUser.PhoneNumber = model.PhoneNumber;
            //    existingUser.RoleID = model.RoleID;

            //    // Save the changes to the database
            //    await _context.SaveChangesAsync();

            //    return RedirectToAction("ListUsers", "AppUsers"); // Redirect to the user list page after successful update
            //}

            // If model state is invalid, return the edit form with validation errors
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            //var user = await _context.Users.FindAsync(id);
            //if (user == null)
            //{
            //    return NotFound(); // Handle the case where the user with the specified ID is not found
            //}

            //_context.Users.Remove(user);
            //await _context.SaveChangesAsync();

            return RedirectToAction("ListUsers", "AppUsers"); // Redirect to the user list page after successful deletion
        }

    }
}
