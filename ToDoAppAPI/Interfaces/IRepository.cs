using System.Collections.Generic;

namespace ToDoAppAPI.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        T GetById(int id);

        IEnumerable<T> GetAll();

        void Remove(T entity);

        void Save();

        void Update(T entity);
    }
}