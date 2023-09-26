using CureWellUsingEF.Helper;
using CureWellUsingEF.Modals;
using CureWellUsingEF.Repos.Models;

namespace CureWellUsingEF.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<APIResponse> Remove(int id);
        Task<APIResponse> Create(User data);
        Task<APIResponse> Update(User data, int id);
    }
}
