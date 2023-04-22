using System;

namespace TodoList.Api.Models

{
    public class TodoItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TodoStatus Status { get; set; }
    }
}
