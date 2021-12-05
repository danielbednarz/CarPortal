using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AdminController : AppController
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("usersWithRoles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var data = await _userManager.Users
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                }).ToListAsync();

            return Ok(data);
        }

        [HttpPost("editRoles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                return NotFound("Błąd przy próbie znalezienia użytkownika");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if(!result.Succeeded)
            {
                return BadRequest("Błąd przy próbie dodania roli");
            }

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if(!result.Succeeded)
            {
                return BadRequest("Błąd przy próbie usunięcia roli");
            }

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Policy = "UserPhotoManagement")]
        [HttpGet("photosToManage")]
        public ActionResult GetPhotosToManage()
        {
            return Ok("Tylko admin może edytować zdjęcia");
        }
    }
}
