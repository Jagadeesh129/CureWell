using AutoMapper;
using CureWellUsingEF.Helper;
using CureWellUsingEF.Modals;
using CureWellUsingEF.Repos;
using CureWellUsingEF.Repos.Models;
using CureWellUsingEF.Services;
using Microsoft.EntityFrameworkCore;

namespace CureWellUsingEF.Container
{
    public class SurgerySevice : ISurgeryService
    {
        private readonly CureWellDBContext context;
        private readonly IMapper mapper;

        public SurgerySevice(CureWellDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<APIResponse> Create(SurgeryModal _data)
        {
            APIResponse response = new APIResponse();
            try
            {
                Surgery data = this.mapper.Map<SurgeryModal, Surgery>(_data);
                await this.context.Surgeries.AddAsync(data);
                await this.context.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.SurgeryId.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 401;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<List<SurgeryModal>> GetAll()
        {
            DateTime currentDate = DateTime.Today;
            List<SurgeryModal> responses = new List<SurgeryModal>();
            var data = await this.context.Surgeries.Where(s=>s.SurgeryDate== currentDate).ToListAsync();
            if (data != null)
            {
                responses = this.mapper.Map<List<Surgery>, List<SurgeryModal>>(data);
            }
            return responses;
        }

        public async Task<SurgeryModal> GetById(int id)
        {
            SurgeryModal response = new SurgeryModal();
            var data = await this.context.Surgeries.FindAsync(id);
            if (data != null)
            {
                response = this.mapper.Map<Surgery, SurgeryModal>(data);
                return response;
            }
            return null;
        }

        public async Task<APIResponse> Remove(int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var data = await this.context.Surgeries.FindAsync(id);
                if (data != null)
                {
                    this.context.Surgeries.Remove(data);
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

        public async Task<APIResponse> Update(SurgeryModal _data, int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var data = await this.context.Surgeries.FindAsync(id);
                if (data != null)
                {
                    data.DoctorId = _data.DoctorId;
                    data.SurgeryDate = _data.SurgeryDate;
                    data.StartTime = _data.StartTime;
                    data.EndTime = _data.EndTime;
                    data.SurgeryCategory = _data.SurgeryCategory;
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
