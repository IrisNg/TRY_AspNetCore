using AutoMapper;
using TRY_AspNetCore_API.Models.DTOs.v1;

namespace TRY_AspNetCore_API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            /* v1 Resource */

            // CreateMap<Resource, ResourceDtoV1>().ReverseMap();

            /* v2 Resource */
        }
    }
}
