using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Constants;
using RepositoryPatternWithUOW.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF.ClassServices;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public T GetById(int id)
    {
        var obj = _context.Set<T>().Find(id);
        return obj!;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var obj = await _context.Set<T>().FindAsync(id);
        return obj!;
    }

    public IEnumerable<T> GetAll()
    {
        var objs = _context.Set<T>().ToList();
        return objs;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var objs = await _context.Set<T>().ToListAsync();
        return objs;
    }

    public T Find(Expression<Func<T, bool>> criteria, string[]? includes = null)
    {
        IQueryable<T> query = _context.Set<T>();
        if(includes is not null)
        {
            foreach (var include in includes)
                query = query.Include(include);
        }
        var obj = query.SingleOrDefault(criteria);
        return obj!;
    }

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[]? includes = null)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes is not null)
        {
            foreach (var include in includes)
                query = query.Include(include);
        }
        var objs = query.Where(criteria).ToList();
        return objs!;
    }

    public IEnumerable<T> MasterFind(Expression<Func<T, bool>> criteria, int? take, int? skip,
        Expression<Func<T, object>>? orderBy = null, string orderByDirection = OrderBy.Ascending)
    {
        IQueryable<T> query = _context.Set<T>().Where(criteria);

        if(take.HasValue)
            query = query.Take(take.Value);
        if (skip.HasValue)
            query = query.Skip(skip.Value);
        
        if(orderBy is not null)
        {
            if (orderByDirection == OrderBy.Ascending)
                query = query.OrderBy(orderBy);
            else
                query = query.OrderByDescending(orderBy);
        }

        var objs = query.ToList();
        return objs;
    }

    public T Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return (entity);
    }

    public IEnumerable<T> AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
        return (entities);
    }
}
