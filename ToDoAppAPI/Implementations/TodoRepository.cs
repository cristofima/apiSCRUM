using ToDoAppAPI.Interfaces;
using ToDoAppAPI.Models;

namespace ToDoAppAPI.Implementations
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        public TodoRepository(SCRUMContext Context) : base(Context)
        {
        }
    }
}