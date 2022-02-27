using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassiveDynamic.Data.Models;
using MassiveDynamic.DTOs;
using MassiveDynamic.Data.ModelConfigs;

namespace MassiveDynamic.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor, 
            UserManager<ApplicationUser> userManager
        ) {
            _roleManager = roleManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpGet]
        public async Task<IEnumerable<IdentityRoleDto>> GetAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<IEnumerable<IdentityRoleDto>>(roles);
        }

        [HttpGet("getUserRoles")]
        public async Task<IEnumerable<string>> GetUserRoles()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if(user != null)
            {
                return await _userManager.GetRolesAsync(user);
            }
            return new List<string>();
        }
    }
}
