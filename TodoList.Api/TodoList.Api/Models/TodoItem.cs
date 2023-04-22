using System;

public class TodoItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TodoStatus Status { get; set; }
}

public enum TodoStatus
{
    NotStarted,
    InProgress,
    Completed
}