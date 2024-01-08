using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebApi.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStoreWebApi.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStoreWebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using MovieStoreWebApi.Application.DirectorOperations.Queries.GetDirectors;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Controllers;

[Route("[controller]")]
public class DirectorsController : Controller
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public DirectorsController(MovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        GetDirectorsQuery query = new GetDirectorsQuery(_context, _mapper);

        var result = await query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context, _mapper);
        query.DirectorId = id;

        GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
        validator.ValidateAndThrow(query);
        
        var result = await query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDirectorModel newDirector)
    {
        CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper);
        command.Model = newDirector;
        CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateDirectorModel updateDirector)
    {
        UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
        command.DirectorId = id;
        command.Model = updateDirector;
        UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
        command.DirectorId = id;
        DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }
}