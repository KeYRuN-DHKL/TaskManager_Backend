using AutoMapper;
using TaskManager.Core.DTOs.AppTask;
using TaskManager.Core.DTOs.Project;
using TaskManager.Core.DTOs.Tag;
using TaskManager.Core.Entities;

namespace TaskManager.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Project Mappings
            CreateMap<ProjectEntity, ProjectResponse>()
                .ForMember(dest => dest.TaskCount,
                           opt => opt.MapFrom(src => src.Tasks.Count));

            CreateMap<CreateProjectRequest, ProjectEntity>();

            CreateMap<AppTaskEntity, TaskResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()))
                .ForMember(dest => dest.AssigneeId, opt => opt.MapFrom(src => src.AssigneeId == null ? src.AssigneeId : null))
                .ForMember(d => d.Tags, o => o.MapFrom(s => s.TaskTag.Select(tt => tt.Tag.Name).ToList()));

            CreateMap<CreateTaskRequest,AppTaskEntity>();

            CreateMap<CreateTagRequest,TagEntity>();
            CreateMap<TagEntity, TagResponse>();

        }
    }
}
