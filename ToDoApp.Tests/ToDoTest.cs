using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoAppAPI.Controllers;
using ToDoAppAPI.Interfaces;
using ToDoAppAPI.Models;
using Xunit;

namespace ToDoApp.Tests
{
    public class ToDoTest
    {
        private Mock<ITodoRepository> TodoRepository;
        private TodosController TodosController;

        public ToDoTest()
        {
            this.TodoRepository = new Mock<ITodoRepository>();
            this.TodosController = new TodosController(this.TodoRepository.Object);
        }

        [Fact]
        public void TodoList_OkObjectResult()
        {
            var result = this.TodosController.GetTodos();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void CreateTodo_CreatedResult()
        {
            var todo = new Todo
            {
                Name = "Test Unit"
            };

            var result = this.TodosController.CreateTodo(todo);
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public void CreateTodo_BadRequest()
        {
            var todo = new Todo
            {
                IsComplete = true
            };
            this.TodosController.ModelState.AddModelError("Name", "Name is required");

            var result = this.TodosController.CreateTodo(todo);

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(new SerializableError(this.TodosController.ModelState), actionResult.Value);
        }
    }
}