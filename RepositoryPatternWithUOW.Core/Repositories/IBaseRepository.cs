﻿using RepositoryPatternWithUOW.Core.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Repositories;

public interface IBaseRepository<T> where T : class
{
    T GetById(int id);
    Task<T> GetByIdAsync(int id);

    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();

    T Find(Expression<Func<T, bool>> criteria, string[]? includes = null);
    IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[]? includes = null);
    
    IEnumerable<T> MasterFind(Expression<Func<T, bool>> criteria,int? take,int? skip,
        Expression<Func<T, object>>? orderBy = null, string orderByDirection = OrderBy.Ascending);

    T Add(T entity);
    IEnumerable<T> AddRange(IEnumerable<T> entities);
}
