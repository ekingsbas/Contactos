using Contactos.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contactos.Data.Contracts
{
    //Interface básica para un repositorio con las funciones más comunes
    internal interface IRepository<T> where T: BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();

        Task<T> GetByIdAsync(int id);
        T GetById(int id);

        T Create(T entity);

        T Update(T entity);

        void Delete(int id);

        Task<int> Commit();
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate);
        Task<bool> Any(Expression<Func<T, bool>> predicate);
        Task<int> Count(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefault(Expression<Func<T, bool>> predicate);

    }
}
