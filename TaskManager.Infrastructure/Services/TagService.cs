using AutoMapper;
using TaskManager.Core.DTOs.AppTask;
using TaskManager.Core.DTOs.Tag;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public TagService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TagResponse> CreateTagAsync(CreateTagRequest request)
        {
            var tags = _mapper.Map<TagEntity>(request);
            await _unitOfWork.Tags.AddAsync(tags);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TagResponse>(tags);
        }

        public async Task<IEnumerable<TagResponse>> GetAllTagsAsync()
        {
            var tags = await _unitOfWork.Tags.GetAllAsync();
            return _mapper.Map<IEnumerable<TagResponse>>(tags);

        }

        public async Task<TagResponse?> GetTagByIdAsync(int tagId)
        {
            var tag = await _unitOfWork.Tags.GetByIdAsync(tagId);
            return tag == null ? null : _mapper.Map<TagResponse>(tag);
        }
    }
}
