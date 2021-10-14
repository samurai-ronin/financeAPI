using System;
using System.Linq;
using System.Linq.Expressions;
using api.Data;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
   private readonly FinanceContext _context;
        public Repository(FinanceContext context)
        {
            _context=context;
        }
        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($" entity must not be null");
            }

            try
            {
                 _context.Remove(entity);
                 _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(entity)} could not be delete");
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return _context.Set<TEntity>().AsNoTracking();
            }
            catch (Exception)
            {
                throw new Exception("Couldn't retrieve entities");
            }
        }

        public IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                return _context.Set<TEntity>().Where(filter).AsNoTracking();
            }
            catch (Exception)
            {
                throw new Exception("Couldn't retrieve entities");
            }
        }

        public TEntity GetById(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            try
            {
                return _context.Set<TEntity>().FromSqlRaw(query);
            }
            catch (Exception)
            {
                throw new Exception("Couldn't retrieve entities");
            }
        }

        public TEntity Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(" entity must not be null");
            }

            try
            {
                 _context.Add(entity);
                 _context.SaveChanges();
                 return entity;
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(entity)} could not be saved");
            }
        }

        public void Save()
        {
           _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity must not be null");
            }

            try
            {
                _context.Update(entity);
                 _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(entity)} could not be updated");
            }
        }
    }
}