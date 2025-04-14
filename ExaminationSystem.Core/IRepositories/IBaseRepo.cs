using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Core.IRepositories
{
    public interface IBaseRepo<T> where T : class
    {
        T GetById(long id);
        Task<T> GetByIdAsync(long id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Find(Expression<Func<T, bool>> criteria);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int take, int skip);
        public PaginatedResult<T> FindAll(
             int? take = 10,
             int? skip = 0,
             Expression<Func<T, bool>> criteria = null,
             Expression<Func<T, object>> orderBy = null,
             string orderByDirection = OrderBy.Ascending
             );

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);
        public PaginatedResult<T> FindAllAsync(
             int? take = 10,
             int? skip = 0,
             Expression<Func<T, bool>> criteria = null,
             Expression<Func<T, object>> orderBy = null,
             string orderByDirection = OrderBy.Ascending
            );
        T Add(T entity);
        Task<T> AddAsync(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        T Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);
        int Count();
        int Count(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);


        Task<IEnumerable<T>> FindAllAsync(
                int? take, int? skip, 
                Expression<Func<T, bool>> criteria = null,
                Expression<Func<T, object>> orderBy = null,
                string orderByDirection = OrderBy.Ascending,
                params Expression<Func<T, object>>[] includes);



    }
}
