
using Contactos.Data.Contracts;
using Contactos.Data.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Data.Repositories
{
    //Repositorio base para las posibles diferentes entidades en la BD
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;

        public BaseRepository()
        {
        }

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public void Error(string message)
        {
            throw new Exception(message);
        }

        #region Métodos

        public virtual Task<int> Commit()
        {
            return _context.SaveChangesAsync();
        }

        public virtual Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                var query = _context.Set<TEntity>().Where(x => x.ID == id);
                
                return query.FirstOrDefaultAsync();
            }
            catch (SqliteException se)
            {
                throw new Exception($"Error en db/GetByIdAsync, Entity:{typeof(TEntity)}", se);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual TEntity GetById(int id)
        {
            try
            {
                var query = _context.Set<TEntity>().Where(x => x.ID == id);
                
                return query.FirstOrDefault();
            }
            catch (SqliteException se)
            {
                throw new Exception($"Error en db/GetById, Entity:{typeof(TEntity)}", se);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {

            try
            {
                var query = _context.Set<TEntity>().Where(x => true);
                return await query.ToListAsync();
            }
            catch (SqliteException se)
            {
                throw new Exception($"Error en db/GetAllAsync, Entity:{typeof(TEntity)}", se);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            try
            {
                var query = _context.Set<TEntity>().Where(x => true);
                return query.ToList();
            }
            catch (SqliteException se)
            {
                throw new Exception($"Error en db/GetAll, Entity:{typeof(TEntity)}", se);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual TEntity Create(TEntity entity)
        {
            try
            {
                
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (SqliteException se)
            {
                throw new Exception($"Error en db/Create, Entity:{typeof(TEntity)}", se);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public virtual TEntity Update(TEntity entity)
        {
            try
            {
               
                _context.Set<TEntity>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return entity;
            }
            catch (SqliteException se)
            {
                throw new Exception($"Error en db/Update, Entity:{typeof(TEntity)}", se);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void Delete(TEntity entity)
        {
            try
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _context.Set<TEntity>().Attach(entity);
                }
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
            catch (SqliteException se)
            {
                throw new Exception($"Error en db/Delete, Entity:{typeof(TEntity)}", se);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void Delete(int id)
        {
            try
            {
                var entity = GetById(id);
                if (entity == null)
                {
                    return;
                }

                Delete(entity);
            }
            catch (SqliteException se)
            {
                throw new Exception($"Error en db/Delete, Entity:{typeof(TEntity)}", se);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public virtual async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate )
        {
            try
            {
                var query = _context.Set<TEntity>().Where(predicate);
                return await query?.ToListAsync();
            }
            catch (SqliteException se)
            {
                throw new Exception($"Error en db/Where, Entity:{typeof(TEntity)}", se);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var query = _context.Set<TEntity>().Where(predicate);
                return await query?.AnyAsync();
            }
            catch (SqliteException se)
            {
                throw new Exception($"Error en db/Any, Entity:{typeof(TEntity)}", se);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var query = _context.Set<TEntity>().Where(predicate);
                return await query?.CountAsync();
            }
            catch (SqliteException se)
            {
                throw new Exception($"Error en db/Count, Entity:{typeof(TEntity)}", se);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var query = _context.Set<TEntity>().Where(predicate);
                return await (query?.SingleOrDefaultAsync());
            }
            catch (SqliteException se)
            {
                throw new Exception($"Error en db/SingleOrDefault, Entity:{typeof(TEntity)}", se);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion
    }
}
