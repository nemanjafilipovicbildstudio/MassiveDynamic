using System.Collections.Generic;
using System.Threading.Tasks;
using MassiveDynamic.DTOs;

namespace MassiveDynamic.AppliactionServices
{
    public interface ICompanyApplicationService
    {
        Task<GetCompanyDto> GetAsync(string id);
        Task<IEnumerable<GetCompanyDto>> GetAllAsync();
    }
}
