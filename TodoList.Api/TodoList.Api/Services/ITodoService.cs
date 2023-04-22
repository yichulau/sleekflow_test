using System;
using System.Collections.Generic;
using TodoList.Api.Models;

namespace TodoList.Api.Services
{
    public interface ITodoService
    {
        List<TodoItemDto> GetAll(string? filterByStatus, DateTime? filterByDueDate, string? sortBy);
        TodoItemDto GetById(Guid id);
        TodoItemDto Create(TodoItem item);
        void Update(Guid id, TodoItem updatedItem);
        void Delete(Guid id);
    }
}
