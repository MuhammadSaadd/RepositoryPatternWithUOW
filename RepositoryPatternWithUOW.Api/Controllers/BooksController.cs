using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Constants;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.Core.Repositories;

namespace RepositoryPatternWithUOW.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public BooksController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetById")]
    public IActionResult GetById()
    {
        var book = _unitOfWork.Books.GetById(1);
        return Ok(book);
    }

    [HttpGet("GetByIdAsync")]
    public async Task<IActionResult> GetByIdAsync()
    {
        var author = await _unitOfWork.Books.GetByIdAsync(1);
        return Ok(author);
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var books = _unitOfWork.Books.GetAll();
        return Ok(books);
    }

    [HttpGet("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        var books = await _unitOfWork.Books.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("GetByName")]
    public IActionResult GetByName()
    {
        var book = _unitOfWork.Books.Find(b => b.Title == "Wasitya", new[] {"Author"});
        return Ok(book);
    }

    [HttpGet("GetAllWithAuthors")]
    public IActionResult GetAllWithAuthors()
    {
        var books = _unitOfWork.Books.FindAll(b => b.Title == "Wasitya", new[] { "Author" });
        return Ok(books);
    }

    [HttpGet("GetOrdered")]
    public IActionResult GetOrdered()
    {
        var books = _unitOfWork.Books.MasterFind(b => b.AuthorId == 1, null, null, 
            b => b.Title, OrderBy.Descending);
        return Ok(books); 
    }

    [HttpPost("AddOne")]
    public IActionResult AddOne()
    {
        var book = _unitOfWork.Books.Add(new Book { Title = "El swa3k el morsla", AuthorId = 2 });
        _unitOfWork.Complete();
        return Ok(book);
    }
}   
