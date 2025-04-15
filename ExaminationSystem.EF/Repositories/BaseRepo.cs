using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.EF.Repositories
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        protected ExaminationDBContext _context;

        public BaseRepo(ExaminationDBContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T GetById(long id)
        {
            return _context.Set<T>().Find(id); // find search for the value passed in the primary key column no matter its name is
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T Find(Expression<Func<T, bool>> criteria) 
        {
            //Expression<Func<T, bool>> this is an expression tree search for it can only take func
            // Func<> is special — it's built for Expression trees
            // Entity Framework needs expressions to generate proper SQL like:
            //SELECT* FROM Users WHERE Age > 18
            // and expression has to be of type func 
            IQueryable<T> query = _context.Set<T>();


            return query.SingleOrDefault(criteria);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> query = _context.Set<T>();


            return await query.SingleOrDefaultAsync(criteria);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> query = _context.Set<T>();


            return query.Where(criteria).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToList();
        }

        //public IEnumerable<T> FindAll(int? take, int? skip, Expression<Func<T, bool>> criteria = null,
        //    Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        //{

        //    IQueryable<T> query = _context.Set<T>().AsQueryable();
        //    if(criteria != null)
        //        query = query.Where(criteria);
        //    if (skip.HasValue)
        //        query = query.Skip(skip.Value);

        //    if (take.HasValue)
        //        query = query.Take(take.Value);

        //    if (orderBy != null)
        //    {
        //        if (orderByDirection == OrderBy.Ascending)
        //            query = query.OrderBy(orderBy);
        //        else
        //            query = query.OrderByDescending(orderBy);
        //    }

        //    return query.ToList(); // the query will be only executed at that moment anything above is just building the query
        //}

        public PaginatedResult<T> FindAll(
             int? take = 10,  
             int? skip = 0,   
             Expression<Func<T, bool>> criteria = null,
             Expression<Func<T, object>> orderBy = null,
             string orderByDirection = OrderBy.Ascending
            )
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();


            if (criteria != null)
                query = query.Where(criteria);

            var totalItems = query.Count(); 
            var totalPages = (int)Math.Ceiling(totalItems / (double)take.GetValueOrDefault(10));  

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            var items = query.ToList();  

            return new PaginatedResult<T>
            {
                Items = items,
                CurrentPage = (skip.GetValueOrDefault(0) / take.GetValueOrDefault(10)) + 1,  
                PageSize = take.GetValueOrDefault(10), 
                TotalPages = totalPages,
                TotalItems = totalItems
            };
        }




        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> query = _context.Set<T>();

            return await query.Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
        {
            return await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(int? take, int? skip, Expression<Func<T, bool>> criteria = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();
            if (criteria != null)
                query = query.Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();// the query will be only executed at that moment anything above is just building the query
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AttachRange(entities);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Count(criteria);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}
