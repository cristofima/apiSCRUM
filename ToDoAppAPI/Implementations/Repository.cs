using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ToDoAppAPI.Interfaces;
using ToDoAppAPI.Models;

namespace ToDoAppAPI.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SCRUMContext Context;
        //internal DbSet<T> dbSet;

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

        protected IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = this.Context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
    }
}