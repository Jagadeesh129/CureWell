using AutoMapper;
using CureWellUsingEF.Helper;
using CureWellUsingEF.Modals;
using CureWellUsingEF.Repos;
using CureWellUsingEF.Repos.Models;
using CureWellUsingEF.Services;
using Microsoft.EntityFrameworkCore;

namespace CureWellUsingEF.Container
{
    public class DoctorSpecializationService : IDoctorSpecializationService
    {
        private readonly CureWellDBContext context;
        private readonly IMapper mapper;
        private readonly IDoctorService doctorService;

        public DoctorSpecializationService(CureWellDBContext context, IMapper mapper, IDoctorService doctorService)
        {
            this.context = context;
            this.mapper = mapper;
            this.doctorService = doctorService;
        }
        public async Task<APIResponse> Create(DoctorSpecializationModal _data)
        {
            APIResponse response = new APIResponse();
            try
            {
                DoctorSpecialization data = this.mapper.Map<DoctorSpecializationModal, DoctorSpecialization>(_data);
                await this.context.DoctorSpecializations.AddAsync(data);
                await this.context.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.DoctorId.ToString()+" "+data.SpecializationCode;
            }
            catch (Exception ex)
            {
                response.ResponseCode = 401;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<List<DoctorSpecializationModal>> GetAll()
        {
            List<DoctorSpecializationModal> responses = new List<DoctorSpecializationModal>();
            var data = await this.context.DoctorSpecializations.ToListAsync();
            if (data != null)
            {
                responses = this.mapper.Map<List<DoctorSpecialization>, List<DoctorSpecializationModal>>(data);
            }
            return responses;
        }

        public async Task<DoctorSpecializationModal> GetById(int id, string code)
        {
            DoctorSpecializationModal response = new DoctorSpecializationModal();
            var data = await this.context.DoctorSpecializations
                .Where(ds => ds.DoctorId == id && ds.SpecializationCode == code).FirstOrDefaultAsync();
            if (data != null)
            {
                response = this.mapper.Map<DoctorSpecialization, DoctorSpecializationModal>(data);
                return response;
            }
            return null;
        }

        public async Task<List<DoctorModal>> GetDoctorsByCode(string code)
        {
            List<DoctorModal> responses = new List<DoctorModal>();
            var data = await this.context.DoctorSpecializations.Where(ds=> ds.SpecializationCode == code).ToListAsync();
            if (data != null)
            {
                foreach(var row in data)
                {
                    responses.Add( await doctorService.GetById(row.DoctorId));
                }
                return responses;
            }
            return null;
        }

        public async Task<APIResponse> Remove(int id, string code)
        {
            APIResponse response = new APIResponse();
            try
            {
                var data = await this.context.DoctorSpecializations
                .Where(ds => ds.DoctorId == id && ds.SpecializationCode == code).FirstOrDefaultAsync();
                if (data != null)
                {
                    this.context.DoctorSpecializations.Remove(data);
                    await this.context.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = id.ToString() + " " + code;
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

        public async Task<APIResponse> Update(DoctorSpecializationModal _data, int id, string code)
        {
            APIResponse response = new APIResponse();
            try
            {
                var data = await this.context.DoctorSpecializations
                .Where(ds => ds.DoctorId == id && ds.SpecializationCode == code).FirstOrDefaultAsync();
                if (data != null)
                {
                    data.SpecializationDate = _data.SpecializationDate;
                    await this.context.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = id.ToString()+" "+code;
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
