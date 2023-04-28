using AutoMapper;
using ROWalks.API.Models.Domain;
using ROWalks.API.Models.DTO;

namespace ROWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<County,CountyDto>().ReverseMap();
            CreateMap<AddCountyRequestDto,County>().ReverseMap();
            CreateMap<UpdateCountyDto,County>().ReverseMap();
            CreateMap<Walk,CreateWalkDto>().ReverseMap();
            CreateMap<Walk,WalkDto>().ReverseMap();
            CreateMap<Difficulty,DifficultyDto>().ReverseMap();
            CreateMap<Walk,UpdateWalkDto>().ReverseMap();
        }

    }
}
