using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.Core.Repositories;
using RepositoryPatternWithUOW.EF.ClassServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF;

public class UnitOfWork : IUnitOfWork
{
    protected readonly AppDbContext _context;
    public IBaseRepository<Author> Authors { get; private set; }
    public IBooksRepository Books { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Authors = new BaseRepository<Author>(_context);
        Books = new BooksRepository(_context);
    }


    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
