using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoAppAPI.Interfaces;
using ToDoAppAPI.Models;

namespace ToDoAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository TodoRepository;

        public TodosController(ITodoRepository TodoRepository) => this.TodoRepository = TodoRepository;

        /// <summary>
        /// Retrieves a TodoItem's List.
        /// </summary>
        /// <returns>A TodoItem's List</returns>
        /// <response code="200">Returns the TodoItem's List</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Todo>))]
        public IActionResult GetTodos()
        {
            return Ok(this.TodoRepository.GetAll());
        }

        /// <summary>
        /// Retrieves a TodoItem.
        /// </summary>
        /// <returns>A specific TodoItem</returns>
        /// <response code="200">Returns the specific item</response>
        /// <response code="404">The item doesn't exist</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(Todo))]
        [ProducesResponseType(404)]
        public IActionResult GetTodo(int id)
        {
            var todo = this.TodoRepository.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <param name="todo">The item to create</param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">The item is invalid</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateTodo(Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.TodoRepository.Add(todo);
            this.TodoRepository.Save();

            return Created("", todo);
        }

        /// <summary>
        /// Updates a TodoItem.
        /// </summary>
        /// <returns>The updated TodoItem</returns>
        /// <response code="200">Returns the updated item</response>
        /// <response code="400">The item is invalid</response>
        /// <response code="404">The item doesn't exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Todo))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTodo(int id, Todo todoParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = this.TodoRepository.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Name = todoParam.Name;
            todo.IsComplete = todoParam.IsComplete;

            this.TodoRepository.Update(todo);
            this.TodoRepository.Save();

            return Ok(todo);
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">The item was deleted</response>
        /// <response code="404">The item doesn't exist</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTodo(int id)
        {
            var todo = this.TodoRepository.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            this.TodoRepository.Remove(todo);
            this.TodoRepository.Save();

            return NoContent();
        }
    }
}