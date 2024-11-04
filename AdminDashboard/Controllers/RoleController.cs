using AdminDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.Controllers
{

    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var role = await _roleManager.Roles.ToListAsync();
            return View(role);
        }


        [HttpPost]
        public async Task<IActionResult> Create(RoleFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(model.Name);
                if (roleExist)
                {
                    ModelState.AddModelError("Name", "This Role Is Already Exist");
                    return RedirectToAction("Index");
                }

                await _roleManager.CreateAsync(new IdentityRole { Name = model.Name.Trim() });

                return RedirectToAction("Index");


            }

            return RedirectToAction("Index");

        }

       
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if(role is not null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var mappedRole = new RoleViewModel { Id = id, Name = role.Name.Trim() };

            return View(mappedRole);

        }
         [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {


            if (ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(model.Name.Trim());
                if (roleExist)
                {
                    ModelState.AddModelError("Name", "This Role Is Already Exist");
                    return RedirectToAction("Index");
                }

                var role = await _roleManager.FindByIdAsync(model.Id);

                role.Name = model.Name;

                await _roleManager.UpdateAsync(role);

                return RedirectToAction("Index");
            }

            return View(model);


        }




    }
}
