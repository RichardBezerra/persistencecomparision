using System.Collections.Generic;

namespace PersistenceComparision.Core
{
    public interface Repo<T> where T : class
    {
        IEnumerable<T> Read();

        T FindById(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
