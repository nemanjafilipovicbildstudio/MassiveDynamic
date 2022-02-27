using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassiveDynamic.Data.Models;
using MassiveDynamic.Data.Repositories;
using MassiveDynamic.DTOs;

namespace MassiveDynamic.AppliactionServices
{
    public class CompanyApplicationService : ICompanyApplicationService
    {
        private readonly IRepository<Company, string> _companyRepository;
        private readonly IRepository<ContactPerson, string> _contactPersonRepository;

        public CompanyApplicationService(
            IRepository<Company, string> companyRepository,
            IRepository<ContactPerson, string> contactPersonRepository
        ) {
            _companyRepository = companyRepository;
            _contactPersonRepository = contactPersonRepository;
        }

        public async Task<GetCompanyDto> GetAsync(string id)
        {
            return (await GetDataAsync(id)).FirstOrDefault();
        }

        public Task<IEnumerable<GetCompanyDto>> GetAllAsync()
        {
            return GetDataAsync();
        }

        private async Task<IEnumerable<GetCompanyDto>> GetDataAsync(string companyId = null)
        {
            var data = await (from company in _companyRepository.GetQueryable()
                              join contactPerson in _contactPersonRepository.GetQueryable() on company.Id equals contactPerson.CompanyId into g
                              from contactPerson in g.DefaultIfEmpty()
                              where companyId == null || company.Id == companyId
                              select new
                              {
                                  CompanyId = company.Id,
                                  CompanyName = company.Name,
                                  CompanyAddress = company.Address,
                                  ContactPersonId = contactPerson.Id,
                                  ContactPersonCompanyId = contactPerson.CompanyId,
                                  ContactPersonFirstName = contactPerson.FirstName,
                                  ContactPersonLastName = contactPerson.LastName,
                                  ContactPersonEmail = contactPerson.Email,
                                  ContactPersonPhoneNumber = contactPerson.PhoneNumber,
                                  ContactPersonAddress = contactPerson.Address
                              }).Distinct().ToListAsync();

            return from d in data
                   group d by new
                   {
                       CompanyId = d.CompanyId,
                       CompanyName = d.CompanyName,
                       CompanyAddress = d.CompanyAddress
                   } into g
                   select new GetCompanyDto
                   {
                       Id = g.Key.CompanyId,
                       Name = g.Key.CompanyName,
                       Address = g.Key.CompanyAddress,
                       ContactPersons = g.ToList()
                                         .Where(x => x.ContactPersonCompanyId == g.Key.CompanyId)
                                         .Select(x => new ContactPersonDto
                                         {
                                             Id = x.ContactPersonId,
                                             CompanyId = x.ContactPersonCompanyId,
                                             FirstName = x.ContactPersonFirstName,
                                             LastName = x.ContactPersonLastName,
                                             Email = x.ContactPersonEmail,
                                             PhoneNumber = x.ContactPersonPhoneNumber,
                                             Address = x.ContactPersonAddress
                                         })
                   };
        }
    }
}
