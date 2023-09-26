using AutoMapper;
using CureWellUsingEF.Helper;
using CureWellUsingEF.Modals;
using CureWellUsingEF.Repos;
using CureWellUsingEF.Repos.Models;
using CureWellUsingEF.Services;
using Microsoft.EntityFrameworkCore;

namespace CureWellUsingEF.Container
{
    public class DoctorService : IDoctorService
    {

        private readonly CureWellDBContext context;
        private readonly IMapper mapper;

        public DoctorService(CureWellDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<APIResponse> Create(DoctorModal _data)
        {
            APIResponse response = new APIResponse();
            try
            {
                Doctor data = this.mapper.Map<DoctorModal, Doctor>(_data);
                await this.context.Doctors.AddAsync(data);
                await this.context.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result=data.DoctorId.ToString();
            }
            catch(Exception ex)
            {
                response.ResponseCode = 401;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<List<DoctorModal>> GetAll()
        {
            List<DoctorModal> responses = new List<DoctorModal>();
            var data= await this.context.Doctors.ToListAsync();
            if (data != null)
            {
                responses = this.mapper.Map<List<Doctor>, List<DoctorModal>>(data);
            }
            return responses;
        }

        public async Task<DoctorModal> GetById(int id)
        {
            DoctorModal response = new DoctorModal();
            var data = await this.context.Doctors.FindAsync(id);
            if (data == null)
            {
                return null;
            }
            response = this.mapper.Map<Doctor, DoctorModal>(data);
            return response;
        }

        public async Task<APIResponse> Remove(int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var data = await this.context.Doctors.FindAsync(id);
                if (data != null)
                {
                    this.context.Doctors.Remove(data);
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

        public async Task<APIResponse> Update(DoctorModal _data, int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var data = await this.context.Doctors.FindAsync(id);
                if (data != null)
                {
                    data.DoctorName = _data.DoctorName;
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
