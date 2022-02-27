using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassiveDynamic.Data.Repositories;
using MassiveDynamic.DTOs;
using MassiveDynamic.Data.Models;
using MassiveDynamic.Data.ModelConfigs;
using Microsoft.AspNetCore.Authorization;

namespace MassiveDynamic.Controllers
{
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Secretary + "," + RoleNames.Client)]
    [ApiController]
    [Route("[controller]")]
    public class ContactPersonController : ControllerBase
    {
        private readonly IRepository<ContactPerson, string> _contactPersonRepository;
        private readonly IMapper _mapper;

        public ContactPersonController(
            IRepository<ContactPerson, string> contactPersonRepository,
            IMapper mapper
        ) {
            _contactPersonRepository = contactPersonRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ContactPersonDto> Get(string id)
        {
            return _mapper.Map<ContactPersonDto>(await _contactPersonRepository.GetAsync(id));
        }

        [HttpGet("getall")]
        public async Task<IEnumerable<ContactPersonDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<ContactPersonDto>>(await _contactPersonRepository.GetAllAsync());
        }

        [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Secretary)]
        [HttpPost]
        public async Task Insert([FromBody] ContactPersonDto contactPerson)
        {
            var contactPersonDb = _mapper.Map<ContactPerson>(contactPerson);
            await _contactPersonRepository.InsertAsync(contactPersonDb);
        }

        [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Secretary)]
        [HttpPut]
        public async Task Update([FromBody] ContactPersonDto contactPerson)
        {
            var contactPersonDb = _mapper.Map<ContactPerson>(contactPerson);
            await _contactPersonRepository.UpdateAsync(contactPersonDb);
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete]
        public async Task Delete(string id)
        {
            await _contactPersonRepository.DeleteAsync(id);
        }
    }
}
