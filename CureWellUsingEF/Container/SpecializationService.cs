using AutoMapper;
using CureWellUsingEF.Helper;
using CureWellUsingEF.Modals;
using CureWellUsingEF.Repos;
using CureWellUsingEF.Repos.Models;
using CureWellUsingEF.Services;
using Microsoft.EntityFrameworkCore;

namespace CureWellUsingEF.Container
{
    public class SpecializationService : ISpecializationService
    {

        private readonly CureWellDBContext context;
        private readonly IMapper mapper;

        public SpecializationService(CureWellDBContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<APIResponse> Create(SpecializationModal _data)
        {
            APIResponse response = new APIResponse();
            try
            {
                Specialization data = this.mapper.Map<SpecializationModal, Specialization>(_data);
                await this.context.Specializations.AddAsync(data);
                await this.context.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.SpecializationCode;
            }
            catch (Exception ex)
            {
                response.ResponseCode = 401;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<List<SpecializationModal>> GetAll()
        {
            List<SpecializationModal> responses = new List<SpecializationModal>();
            var data = await this.context.Specializations.ToListAsync();
            if (data != null)
            {
                responses = this.mapper.Map<List<Specialization>, List<SpecializationModal>>(data);
            }
            return responses;
        }

        public async Task<SpecializationModal> GetByCode(string code)
        {
            SpecializationModal response = new SpecializationModal();
            var data = await this.context.Specializations.FindAsync(code);
            if (data != null)
            {
                response = this.mapper.Map<Specialization, SpecializationModal>(data);
                return response;
            }
            return null;
        }

        public async Task<APIResponse> Remove(string code)
        {
            APIResponse response = new APIResponse();
            try
            {
                var data = await this.context.Specializations.FindAsync(code);
                if (data != null)
                {
                    this.context.Specializations.Remove(data);
                    await this.context.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = code;
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

        public async Task<APIResponse> Update(SpecializationModal _data, string code)
        {
            APIResponse response = new APIResponse();
            try
            {
                var data = await this.context.Specializations.FindAsync(code);
                if (data != null)
                {
                    data.SpecializationName = _data.SpecializationName;
                    await this.context.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = code;
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
