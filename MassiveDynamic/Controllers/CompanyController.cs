using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassiveDynamic.Data.Repositories;
using MassiveDynamic.DTOs;
using MassiveDynamic.Data.Models;
using MassiveDynamic.AppliactionServices;
using Microsoft.AspNetCore.Authorization;
using MassiveDynamic.Data.ModelConfigs;

namespace MassiveDynamic.Controllers
{
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Secretary + "," + RoleNames.Client)]
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IRepository<Company, string> _companyRepository;
        private readonly IMapper _mapper;
        private readonly ICompanyApplicationService _companyApplicationService;

        public CompanyController(
            IRepository<Company, string> companyRepository,
            IMapper mapper,
            ICompanyApplicationService companyApplicationService
        ) {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _companyApplicationService = companyApplicationService;
        }

        [HttpGet]
        public async Task<GetCompanyDto> Get(string id)
        {
            return await _companyApplicationService.GetAsync(id);
        }

        [HttpGet("getall")]
        public async Task<IEnumerable<GetCompanyDto>> GetAll()
        {
            return await _companyApplicationService.GetAllAsync();
        }

        [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Secretary)]
        [HttpPost]
        public async Task Insert([FromBody] CompanyDto company)
        {
            var companyDb = _mapper.Map<Company>(company);
            await _companyRepository.InsertAsync(companyDb);
        }

        [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Secretary)]
        [HttpPut]
        public async Task Update([FromBody] CompanyDto company)
        {
            var companyDb = _mapper.Map<Company>(company);
            await _companyRepository.UpdateAsync(companyDb);
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete]
        public async Task Delete(string id)
        {
            await _companyRepository.DeleteAsync(id);
        }
    }
}
