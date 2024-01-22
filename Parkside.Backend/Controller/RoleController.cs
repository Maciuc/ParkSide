using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parkisde.Models.ViewModel;

namespace Parkisde.Backend.Controller
{
    [Route("api")]
    [ApiController]
    //[Authorize]
    // [Authorize(Roles = "Administrator")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager/*, UserManager<ApplicationUser> userManager*/)
        {
            _roleManager = roleManager;
            //_userManager = userManager;
        }

        [HttpGet("getRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return Ok(roles);
        }

        /*[HttpPut("updateUserRole")]
        public async Task<IActionResult> UpdateUserRole(UpdateRoleViewModel updateRole)
        {
            var user = await _userManager.FindByIdAsync(updateRole.UserId);
            var role = await _roleManager.FindByIdAsync(updateRole.RoleId);

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, role.Name);

            return Ok("The user role has been updated successfully");
        }*/

        [HttpPost("createRole")]
        public async Task<IActionResult> CreateUserRole(CreateRoleViewModel role)
        {
            var roles = await _roleManager.Roles.Where(x => x.Name == role.Name).FirstOrDefaultAsync();

            if (roles != null)
            {
                throw new Exception("This role already exist");
            }
            IdentityRole addRole = new IdentityRole()
            {
                Name = role.Name,
            };

            await _roleManager.CreateAsync(addRole);
            return Ok();
        }
    }
}
