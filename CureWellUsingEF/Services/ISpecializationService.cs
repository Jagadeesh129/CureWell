using CureWellUsingEF.Helper;
using CureWellUsingEF.Modals;

namespace CureWellUsingEF.Services
{
    public interface ISpecializationService
    {
        Task<List<SpecializationModal>> GetAll();
        Task<SpecializationModal> GetByCode(string code);
        Task<APIResponse> Remove(string code);
        Task<APIResponse> Create(SpecializationModal data);
        Task<APIResponse> Update(SpecializationModal data, string code);
    }
}
