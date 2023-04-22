using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TodoList.Api.Services;
using TodoList.Api.Models;
using Microsoft.AspNetCore.Cors;

namespace TodoList.Api.Controllers
{
    // Controller for the TODO API
    [Route("api/todos")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public ToDoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // Create a new TODO item
        [HttpPost]
        [EnableCors("_myAllowAnyOrigin")]
        public IActionResult Create(TodoItem item)
        {
            var newItem = _todoService.Create(item);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        // Get all TODO items
        [HttpGet]
        [EnableCors("_myAllowAnyOrigin")]
        public IActionResult GetAll(string? filterByStatus = null, DateTime? filterByDueDate = null, string? sortBy = null)
        {
            var items = _todoService.GetAll(filterByStatus, filterByDueDate, sortBy);
            return Ok(items);
        }

        // Get a specific TODO item by ID
        [HttpGet("{id}")]
        [EnableCors("_myAllowAnyOrigin")]
        public IActionResult GetById(Guid id)
        {
            var item = _todoService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // Update a specific TODO item by ID
        [HttpPut("{id}")]
        [EnableCors("_myAllowAnyOrigin")]
        public IActionResult Update(Guid id, TodoItem updatedItem)
        {
            _todoService.Update(id, updatedItem);
            return NoContent();
        }

        // Delete a specific TODO item by ID
        [HttpDelete("{id}")]
        [EnableCors("_myAllowAnyOrigin")]
        public IActionResult Delete(Guid id)
        {
            _todoService.Delete(id);
            return NoContent();
        }
    }
}
