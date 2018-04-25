using aspnet_core_api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_api.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        long Add(T entity);
        long Update(T entity);
        long Delete(int id);
    }
    public class Repository<T> : IRepository<T> where T : class
    {
        ApplicationContext ctx;
        public Repository(ApplicationContext context)
        {
            ctx = context;
        }
        public long Add(T entity)
        {
            ctx.Set<T>().Add(entity);
            return ctx.Save();
        }

        public long Delete(int id)
        {
            var entity = Get(id);
            ctx.Set<T>().Remove(entity);
            return ctx.Save();
        }

        public T Get(int id)
        {
            return ctx.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return ctx.Set<T>();
        }

        public long Update(T entity)
        {
            ctx.Set<T>().Update(entity);
            return ctx.Save();
        }
    }
}
