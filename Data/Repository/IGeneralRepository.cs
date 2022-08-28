using Data.DatabaseContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IGeneralRepository
    {
       
            IQueryable<T> FindAll<T>() where T:class;
            IQueryable<T> FindByCondition<T>(Expression<Func<T, bool>> expression) where T:class;
            void Create<T>(T entity) where T:class;
            void Update<T>(T entity) where T:class;
            void Delete<T>(T entity) where T:class;
            void DeleteByCondition<T>(Expression<Func<T, bool>> expression) where T:class;

            void Save();
            NTTContext getContext();
        
    }
}
