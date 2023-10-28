﻿using System.Linq.Expressions;

namespace Task.BL.UOW
{
    public interface IBaseRepo<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdToEditAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[]? includes = null);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[]? includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>>? orderBy = null/*, string orderByDirection = OrderBy.Ascending*/);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        T Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        void Edit(T entity);
        void AttachRange(IEnumerable<T> entities);

        Task<int> CountAsync(Expression<Func<T, bool>> criteria);

    }
}
