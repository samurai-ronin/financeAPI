using System;
using System.Linq;
using System.Linq.Expressions;

namespace api.Interfaces
{
   public interface IRepository<TEntity> where TEntity :class
    {
    void Delete(TEntity entity);
    IQueryable<TEntity> GetAll(
        Expression<Func<TEntity, bool>> filter = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
        string includeProperties = "");
    IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> filter);
    IQueryable<TEntity> GetWithRawSql(string query,params object[] parameters);
    TEntity Insert(TEntity entity);
    void Update(TEntity entity);
    void Save();
    TEntity GetById(object id);
    }
}