using AutoMapper;
using GazlVolunteer.Core.Dtos;
using GazlVolunteer.Core.Models;

namespace GazlVolunteer.Apis.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CivilAssociations,CivilAssociationsDto>().ReverseMap();
            CreateMap<complaintModel,complaintModelDto>().ReverseMap();
            CreateMap<VolunteerModel,VolunteerModelDto>().ReverseMap();
        }
    }
}
