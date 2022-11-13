using AutoMapper;
using VPSDemo.Api.Contracts.Task;

namespace VPSDemo.Api.Mapper
{
    public class TaskMapperProfiles: Profile
    {
        public TaskMapperProfiles()
        {
            // Source Domain -> Destination API
            CreateMap<Domain.Entities.Task, TaskResponse>()
                .ForMember(dest => dest.Identifier, option => option.MapFrom(source => source.Id))
                .ForMember(dest => dest.CreationDate, option => option.MapFrom(source => source.CreationDate.ToString("s")));

        }
    }
}
