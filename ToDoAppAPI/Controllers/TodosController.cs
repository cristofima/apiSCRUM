using Microsoft.AspNetCore.Mvc;
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

        public IActionResult GetTodos()
        {
            return Ok(this.TodoRepository.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTodo(int id)
        {
            var todo = this.TodoRepository.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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