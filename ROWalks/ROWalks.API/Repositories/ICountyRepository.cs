using ROWalks.API.Data;
using ROWalks.API.Models.Domain;
using System.Runtime.CompilerServices;

namespace ROWalks.API.Repositories
{
    public interface ICountyRepository
    { 
    
        Task<List<County>> GetAllAsync();

        Task<County?> GetAsync(Guid id);

        Task<County> CreateAsync(County county);
        Task<County?> UpdateAsync(Guid id, County county);

        Task<County?> DeleteAsync(Guid id);

        

    }
}
