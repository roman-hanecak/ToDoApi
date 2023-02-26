using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using ToDoApi.Entities.DTO;
using ToDoApi.Entities.Model;
using ToDoApi.Exceptions;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Tests
{
    [TestFixture]
    public class TaskItemServiceTests
    {
        private ITaskItemService _taskItemService;
        private ITaskListService _taskListService;

        [SetUp]
        public void Setup()
        {
            _taskItemService = Substitute.For<ITaskItemService>();
            _taskListService = Substitute.For<ITaskListService>();
        }

        [Test]
        public async Task CreateAsync_InvalidTaskListId_ThrowsNotFoundException()
        {
            var taskListId = new Guid();

            var taskItem = new TaskItemModel
            {
                Title = "task",
                Description = "Description",
                CreatedDate = DateTime.Now,
                EndDate = DateTime.Today.AddDays(1),
                Completed = false
            };

            _taskListService.GetAsync(taskListId, Arg.Any<CancellationToken>()).ReturnsNull();
            //act && assert
            Assert.ThrowsAsync<NotFoundException>(() => _taskItemService.CreateAsync(taskListId, taskItem));

            //Assert.Pass();
        }
    }
}