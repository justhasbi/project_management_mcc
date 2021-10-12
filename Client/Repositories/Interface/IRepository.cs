using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Client.Repositories.Interface
{
    /**
     * E = Entity
     * K = id
     */
    public interface IRepository<E, K>
        where E : class
    {
        Task<List<E>> Get();
        Task<E> Get(K id);
        HttpStatusCode Post(E entity);
        HttpStatusCode Put(K id, E entity);
        HttpStatusCode Delete(K id);
    }
}
