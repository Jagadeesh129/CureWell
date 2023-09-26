using AutoMapper;
using CureWellUsingEF.Modals;
using CureWellUsingEF.Repos.Models;

namespace CureWellUsingEF.Helper
{
    public class AutoMapperHandler:Profile
    {
        public AutoMapperHandler()
        {
            CreateMap<Doctor, DoctorModal>().ReverseMap();
            CreateMap<Specialization, SpecializationModal>().ReverseMap();
            CreateMap<Surgery, SurgeryModal>().ReverseMap();
            CreateMap<DoctorSpecialization, DoctorSpecializationModal>().ReverseMap();
        }
    }
}
