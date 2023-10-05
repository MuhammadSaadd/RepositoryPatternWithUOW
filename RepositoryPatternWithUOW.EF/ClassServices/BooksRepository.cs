using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF.ClassServices;

public class BooksRepository : BaseRepository<Book>, IBooksRepository
{
    private readonly AppDbContext _context;

    public BooksRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Book> SpecialMethod()
    {
        throw new NotImplementedException();
    }
}
