using AutoMapper;
using Moq;
using TaskManager.Core.DTOs.Project;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;
using TaskManager.Infrastructure.Services;
using MockQueryable;

namespace TaskManager.Tests.Services
{
    public class ProjectServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IRepository<ProjectEntity>> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProjectService _sut;

        public ProjectServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repoMock = new Mock<IRepository<ProjectEntity>>();
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock.Setup(uow => uow.Projects).Returns(_repoMock.Object);
            _sut = new ProjectService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        private static UserEntity MakeOwner(int id) => new() { Id = id };

        private static ProjectEntity MakeProject(int id,int ownerId,string name="Alex",bool isDeleted = false)
        {
            return new ProjectEntity
            {
                Id = id,
                OwnerId = ownerId,
                Owner = MakeOwner(ownerId),
                Name = name,
                IsDeleted = isDeleted
            };
        }

        [Fact]
        public async Task GetUserProjectsAsync_UserHasProjects_ReturnsOnlyThatUsersProject()
        {
            var userId = 10;

            var projects = new List<ProjectEntity>
            {
                MakeProject(1,userId,"Project 1"),
                MakeProject(2,userId,"Project 2"),
                MakeProject(3,20,"Other User Project")
            };

            _repoMock.Setup(repo => repo.Query()).Returns(projects.AsQueryable().BuildMock<ProjectEntity>());

            var mapped = new List<ProjectResponse>
            {
                new() {Id = 1, Name = "Project 1" },
                new() {Id = 2,Name= "Project 2" }
            };

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ProjectResponse>>(It.IsAny<IEnumerable<ProjectEntity>>())).Returns(mapped);

            var result = await _sut.GetUserProjectsAsync(userId);

            Assert.Equal(2, result.Count());
            Assert.All(result, project => Assert.NotEqual(3, project.Id));
        }
    }
}
