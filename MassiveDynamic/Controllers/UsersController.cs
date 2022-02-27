using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassiveDynamic.DTOs;
using MassiveDynamic.Data.Models;
using MassiveDynamic.Data.ModelConfigs;

namespace MassiveDynamic.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IMapper mapper
        ) {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<GetApplicationUserDto> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentException($"User with Id: {id} cannot be found.");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var ret = _mapper.Map<GetApplicationUserDto>(user);
            ret.Roles = roles;
            return ret;
        }

        [HttpGet("getall")]
        public async Task<IEnumerable<GetApplicationUserDto>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            var ret = new List<GetApplicationUserDto>();
            foreach(var user in users)
            {
                var a = _mapper.Map<GetApplicationUserDto>(user);
                a.Roles = await _userManager.GetRolesAsync(user);
                ret.Add(a);
            }

            return ret;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                throw new ArgumentException($"User with Id: {id} cannot be found.");
            }
            await _userManager.DeleteAsync(user);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GetApplicationUserDto user)
        {
            var userDb = await _userManager.FindByIdAsync(user.Id);
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.UserName = user.UserName;
            user.Email = user.Email;
            await _userManager.UpdateAsync(userDb);

            var existingRoles = await _userManager.GetRolesAsync(userDb);
            
            var rolesToRemove = existingRoles.Where(x => !user.Roles.Contains(x));
            await _userManager.RemoveFromRolesAsync(userDb, rolesToRemove);
            
            var rolesToAdd = user.Roles.Where(x => !existingRoles.Contains(x));
            await _userManager.AddToRolesAsync(userDb, rolesToAdd);

            return Ok();
        }
    }
}
