using Data.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class GeneralRepository : IGeneralRepository
    {
        private NTTContext contexto;

        public GeneralRepository(NTTContext contexto)
        {
            this.contexto = contexto;
            this.contexto.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Create<T>(T entity) where T : class
        {
           contexto.Set<T>().Add(entity);
            
        }

        public void Delete<T>(T entity) where T : class
        {
            contexto.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll<T>() where T : class
        {
            return contexto.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return contexto.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update<T>(T entity) where T : class
        {
            contexto.Set<T>().Update(entity);
        }

        public void Save()
        {
            contexto.SaveChanges();
        }

       
        public NTTContext getContext()
        {
            return this.contexto;
        }

        public void DeleteByCondition<T>(Expression<Func<T, bool>> expression) where T : class
        {
            IQueryable<T> deleteRows = contexto.Set<T>().Where(expression);
            contexto.RemoveRange(deleteRows);
        }
    }
}
