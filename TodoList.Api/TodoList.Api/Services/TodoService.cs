using System;
using System.Collections.Generic;
using System.Linq;
using TodoList.Api.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace TodoList.Api.Services
{
    public class TodoService : ITodoService
    {
        private readonly IMongoCollection<TodoItem> _todos;

        public TodoService(IMongoClient client)
        {
            var database = client.GetDatabase("todolist");
            _todos = database.GetCollection<TodoItem>("todos");
        }

        public List<TodoItemDto> GetAll(string? filterByStatus, DateTime? filterByDueDate, string? sortBy)
        {
            var filter = Builders<TodoItem>.Filter.Empty;

            if (!string.IsNullOrEmpty(filterByStatus) && Enum.TryParse(filterByStatus, out TodoStatus status))
            {
                filter &= Builders<TodoItem>.Filter.Eq(item => item.Status, status);
            }

            if (filterByDueDate.HasValue)
            {
                var startOfDay = filterByDueDate.Value.Date;
                var endOfDay = startOfDay.AddDays(1).AddTicks(-1);
                filter &= Builders<TodoItem>.Filter.Gte(item => item.DueDate, startOfDay) & Builders<TodoItem>.Filter.Lte(item => item.DueDate, endOfDay);
            }

            var sort = sortBy switch
            {
                "status" => Builders<TodoItem>.Sort.Ascending(item => item.Status),
                "dueDate" => Builders<TodoItem>.Sort.Ascending(item => item.DueDate),
                "name" => Builders<TodoItem>.Sort.Ascending(item => item.Name),
                _ => Builders<TodoItem>.Sort.Ascending(item => item.Id)
            };

            var items = _todos.Find(filter).Sort(sort).ToList();

            return items.Select(item => new TodoItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                DueDate = item.DueDate,
                Status = item.Status
            }).ToList();
        }

        public TodoItemDto GetById(Guid id)
        {
            var item = _todos.Find(x => x.Id == id).FirstOrDefault();
            if (item == null)
            {
                return null;
            }

            return new TodoItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                DueDate = item.DueDate,
                Status = item.Status
            };
        }

        public TodoItemDto Create(TodoItem item)
        {
            item.Id = Guid.NewGuid();
            _todos.InsertOne(item);

            return new TodoItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                DueDate = item.DueDate,
                Status = item.Status
            };
        }

        public void Update(Guid id, TodoItem updatedItem)
        {
            updatedItem.Id = id;
            _todos.ReplaceOne(item => item.Id == id, updatedItem);
        }

        public void Delete(Guid id)
        {
            _todos.DeleteOne(item => item.Id == id);
        }
    }
}
