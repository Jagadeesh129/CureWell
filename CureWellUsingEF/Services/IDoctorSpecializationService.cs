using CureWellUsingEF.Helper;
using CureWellUsingEF.Modals;

namespace CureWellUsingEF.Services
{
    public interface IDoctorSpecializationService
    {
        Task<List<DoctorSpecializationModal>> GetAll();
        Task<DoctorSpecializationModal> GetById(int id,string code);
        Task<APIResponse> Remove(int id, string code);
        Task<APIResponse> Create(DoctorSpecializationModal data);
        Task<APIResponse> Update(DoctorSpecializationModal data, int id, string code);
        Task<List<DoctorModal>> GetDoctorsByCode(string code);
    }
}
