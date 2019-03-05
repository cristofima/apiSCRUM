using System.Collections.Generic;
using System.Linq;
using ToDoAppAPI.Interfaces;
using ToDoAppAPI.Models;

namespace ToDoAppAPI.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SCRUMContext Context;

        public Repository(SCRUMContext Context) => this.Context = Context;

        public void Add(T entity)
        {
            this.Context.Set<T>().Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return this.Context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return this.Context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            this.Context.Remove(entity);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Update(T entity)
        {
            this.Context.Set<T>().Attach(entity);
        }
    }
}