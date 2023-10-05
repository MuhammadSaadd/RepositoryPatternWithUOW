using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.Core.Repositories;

namespace RepositoryPatternWithUOW.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthorsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetById")]
    public IActionResult GetById()
    {
        var author = _unitOfWork.Authors.GetById(1);
        return Ok(author);
    }
    
    [HttpGet("GetByIdAsync")]
    public async Task<IActionResult> GetByIdAsync()
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(1);
        return Ok(author);
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var authors = _unitOfWork.Authors.GetAll();
        return Ok(authors);
    }

    [HttpGet("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        var authors = await _unitOfWork.Authors.GetAllAsync();
        return Ok(authors);
    }

    [HttpPost("AddOne")]
    public IActionResult AddOne()
    {
        var author = _unitOfWork.Authors.Add(new Author { Name = "Ibn Qym" });
        _unitOfWork.Complete();
        return Ok(author);
    }
}
