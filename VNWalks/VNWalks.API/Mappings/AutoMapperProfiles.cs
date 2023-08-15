using AutoMapper;
using VNWalks.API.Models.Domain;
using VNWalks.API.Models.DTOs.Difficulty;
using VNWalks.API.Models.DTOs.Region;
using VNWalks.API.Models.DTOs.Walk;

namespace VNWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Create mapping
            /* Region */
            //Mapping Domain Models -> DTOs 
            CreateMap<Region, RegionDto>().ReverseMap();

            //Mapping DTOs  -> Domain Models method Create
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();

            //Mapping DTOs  -> Domain Models method Update
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();


            /* Walk */
            //Mapping Domain Models -> DTOs 
            CreateMap<Walk, WalkDto>().ReverseMap();

            //Mapping DTOs  -> Domain Models method Create
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();

            //Mapping DTOs  -> Domain Models method Update
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();


            /* Difficulty */
            //Mapping Domain Models -> DTOs 
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();

            //Mapping DTOs  -> Domain Models method Create
            //CreateMap<AddWalkRequestDto, Difficulty>().ReverseMap();

            //Mapping DTOs  -> Domain Models method Update
            //CreateMap<UpdateRegionRequestDto, Difficulty>().ReverseMap();



        }

    }


}
