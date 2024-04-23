using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DorukSoft.OfficialWeb.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MSSqlContext _context;

        public Repository(MSSqlContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync(List<Expression<Func<T, object>>>? includes = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter, List<Expression<Func<T, object>>>? includes = null)
        {
            var query = _context.Set<T>().AsQueryable().Where(filter);
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter, List<Expression<Func<T, object>>>? includes = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.AsNoTracking().FirstAsync(filter);
        }

        public async Task<T?> GetByIdAsync(object id, List<Expression<Func<T, object>>>? includes = null)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if(entity != null && includes != null)
            {
                for (var i = 0; i < includes.Count; i++)
                {
                    await _context.Entry(entity).Reference(includes[i]!).LoadAsync();
                }
            }
            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
