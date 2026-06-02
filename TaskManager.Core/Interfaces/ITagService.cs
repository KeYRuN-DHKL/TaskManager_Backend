using TaskManager.Core.DTOs.AppTask;
using TaskManager.Core.DTOs.Tag;

namespace TaskManager.Core.Interfaces
{
    public interface ITagService
    {
        Task<TagResponse> CreateTagAsync(CreateTagRequest request);
        Task<IEnumerable<TagResponse>> GetAllTagsAsync();
        Task<TagResponse?> GetTagByIdAsync(int tagId);
    }
}
