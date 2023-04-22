using System;
using System.Linq;
using TodoList.Api.Models;
using TodoList.Api.Services;
using Xunit;
using MongoDB.Driver;
using Mongo2Go;

namespace TodoList.Tests
{
    public class TodoServiceTests : IDisposable
    {
        private readonly TodoService _todoService;
        private readonly MongoDbRunner _runner;

        public TodoServiceTests()
        {
            // Start an in-memory MongoDB instance
            _runner = MongoDbRunner.Start();
            var client = new MongoClient(_runner.ConnectionString);

            // Pass the client to TodoService
            _todoService = new TodoService(client);
        }

        public void Dispose()
        {
            _runner.Dispose();
        }

        [Fact]
        public void Create_AddsNewTodoItem()
        {
            // Arrange
            var newItem = new TodoItem
            {
                Name = "Test",
                Description = "Test description",
                DueDate = DateTime.Now,
                Status = TodoStatus.NotStarted
            };

            // Act
            var createdItem = _todoService.Create(newItem);

            // Assert
            Assert.NotNull(createdItem);
            Assert.NotEqual(Guid.Empty, createdItem.Id);
            Assert.Equal(newItem.Name, createdItem.Name);
            Assert.Equal(newItem.Description, createdItem.Description);
            Assert.Equal(newItem.DueDate, createdItem.DueDate);
            Assert.Equal(newItem.Status, createdItem.Status);
        }
    }
}
