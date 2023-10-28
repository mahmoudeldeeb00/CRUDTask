using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Task.BL.UOW
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly DbContext _context;
        public BaseRepo(DbContext db)
        {
            this._context = db;
        }
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
        public async Task<T> GetByIdToEditAsync(int id) {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var x = await _context.Set<T>().FindAsync(id);
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            return x ;
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);
            return await query.SingleOrDefaultAsync(criteria);
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(criteria).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
        {
            return await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>>? orderBy = null/* ,string orderByDirection = OrderBy.Ascending*/)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            //if (orderBy != null)
            //{
            //    if (orderByDirection == OrderBy.Ascending)
            //        query = query.OrderBy(orderBy);
            //    else
            //        query = query.OrderByDescending(orderBy);
            //}

            return await query.ToListAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
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
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
        public void DeleteRange(IEnumerable<T> entities) { _context.Set<T>().RemoveRange(entities); }
        public void Attach(T entity) {
            _context.Set<T>().Attach(entity); 
        }
        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        
        }
        public void AttachRange(IEnumerable<T> entities) { _context.Set<T>().AttachRange(entities); }
        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria) => await _context.Set<T>().CountAsync(criteria);
    }
}
