using CureWellUsingEF.Helper;
using CureWellUsingEF.Modals;

namespace CureWellUsingEF.Services
{
    public interface IDoctorService
    {
        Task<List<DoctorModal>> GetAll();
        Task<DoctorModal> GetById(int id);
        Task<APIResponse> Remove(int id);
        Task<APIResponse> Create(DoctorModal data);
        Task<APIResponse> Update(DoctorModal data,int id);
    }
}
