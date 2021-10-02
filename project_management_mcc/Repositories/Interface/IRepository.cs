using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Repository.Interface
{
    /**
     * E = Entity
     * K = Key
     */
    public interface IRepository<E, K> 
        where E : class
    {
        IEnumerable<E> Get();
        E GetById(K key);
        int Insert(E entity);
        int Update(E entity);
        int Delete(K key);
    }
}
