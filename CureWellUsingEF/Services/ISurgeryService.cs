using CureWellUsingEF.Helper;
using CureWellUsingEF.Modals;

namespace CureWellUsingEF.Services
{
    public interface ISurgeryService
    {
        Task<List<SurgeryModal>> GetAll();
        Task<SurgeryModal> GetById(int id);
        Task<APIResponse> Remove(int id);
        Task<APIResponse> Create(SurgeryModal data);
        Task<APIResponse> Update(SurgeryModal data, int id);
    }
}
