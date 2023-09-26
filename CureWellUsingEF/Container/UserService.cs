using CureWellUsingEF.Helper;
using CureWellUsingEF.Repos.Models;
using CureWellUsingEF.Repos;
using CureWellUsingEF.Services;
using Microsoft.EntityFrameworkCore;

namespace CureWellUsingEF.Container
{
    public class UserService:IUserService
    {
        private readonly CureWellDBContext context;

        public UserService(CureWellDBContext context)
        {
            this.context = context;
        }

        public async Task<APIResponse> Create(User data)
        {
            APIResponse response = new APIResponse();
            try
            {
                await this.context.Users.AddAsync(data);
                await this.context.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.Id.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 401;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<List<User>> GetAll()
        {
            var responses = await this.context.Users.ToListAsync();
            return responses;
        }

        public async Task<User> GetById(int id)
        {
            var response = await this.context.Users.FindAsync(id);
            return response;
        }

        public async Task<APIResponse> Remove(int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var data = await this.context.Users.FindAsync(id);
                if (data != null)
                {
                    this.context.Users.Remove(data);
                    await this.context.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = id.ToString();
                }
                else
                {
                    response.ResponseCode = 404;
                    response.Result = "Data not Found";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> Update(User _data, int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var data = await this.context.Users.FindAsync(id);
                if (data != null)
                {
                    data.UserName = _data.UserName;
                    data.Name = _data.Name;
                    data.Mobile = _data.Mobile;
                    data.Password = _data.Password;
                    data.Role = _data.Role;
                    data.Email = _data.Email;
                    await this.context.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = id.ToString();
                }
                else
                {
                    response.ResponseCode = 404;
                    response.ErrorMessage = "Data not Found";
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = 404;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}
