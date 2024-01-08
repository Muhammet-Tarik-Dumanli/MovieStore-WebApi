using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebApi.Application.MovieOperations.Commands.DeleteMovie;
using MovieStoreWebApi.Application.MovieOperations.Commands.UpdateMovie;
using MovieStoreWebApi.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStoreWebApi.Application.MovieOperations.Queries.GetMovies;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Controllers;

[Route("[controller]")]
public class MoviesController : Controller
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public MoviesController(MovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        GetMoviesQuery query = new GetMoviesQuery(_context, _mapper);

        var result = await query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        GetMovieDetailQuery query = new GetMovieDetailQuery(_context, _mapper);
        query.MovieId = id;

        GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
        validator.ValidateAndThrow(query);
        
        var result = await query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateMovieModel newMovie)
    {
        CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
        command.Model = newMovie;
        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateMovieModel updateMovie)
    {
        UpdateMovieCommand command = new UpdateMovieCommand(_context);
        command.MovieId = id;
        command.Model = updateMovie;
        UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        DeleteMovieCommand command = new DeleteMovieCommand(_context);
        command.MovieId = id;
        DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }
}