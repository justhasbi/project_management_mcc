using Microsoft.EntityFrameworkCore;
using project_management_mcc.Context;
using project_management_mcc.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Repositories
{
    public class GeneralRepository<C, E, K> : IRepository<E, K>
        where E : class
        where C : MyContext
    {
        private readonly MyContext context;
        private readonly DbSet<E> dbSet;

        public GeneralRepository(MyContext context)
        {
            this.context = context;
            dbSet = context.Set<E>();
        }

        public int Delete(K key)
        {
            var data = dbSet.Find(key);
            if(data == null)
            {
                throw new ArgumentNullException();
            }

            dbSet.Remove(data);
            return context.SaveChanges();
        }

        public IEnumerable<E> Get()
        {
            if(dbSet.ToList().Count == 0)
            {
                return null;
            }
            return dbSet.ToList();
        }

        public E GetById(K key)
        {
            return dbSet.Find(key);
        }

        public int Insert(E entity)
        {
            dbSet.Add(entity);
            var insert = context.SaveChanges();
            return insert;
        }

        public int Update(E entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChanges();
        }
    }
}
