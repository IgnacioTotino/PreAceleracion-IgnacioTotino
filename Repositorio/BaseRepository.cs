using ChallengeDisney.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeDisney.Repositorio
{
    public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : class 
        where TContext : DbContext 

    {
        private readonly TContext _dbContext;
        private DbSet<TEntity> _dbSet; //expongo el dbset

        protected DbSet<TEntity> DbSet
        {
                //retorna este valor ('??' si es nulo '=' igualala a esto)
            get { return _dbSet ??= _dbContext.Set<TEntity>(); }
        }

        public BaseRepository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TEntity> GetAllEntities()
        {
            return _dbContext.Set<TEntity>().ToList();          //busqueda en general
        }
        
        public TEntity GetEntity(int id) 
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public TEntity Add(TEntity entity) //post
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var entityDelete = _dbContext.Find<TEntity>(id);
            _dbContext.Remove(entityDelete);
            _dbContext.SaveChanges();
        }
        
        //TODO: talves agaregar mas cosas esto es un CRUD
    
    }
}
