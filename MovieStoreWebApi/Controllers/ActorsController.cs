using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Application.ActorOperations.Commands;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Controllers;

[Route("[controller]")]
public class ActorsController : Controller
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public ActorsController(MovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateActorModel newActor)
    {
        CreateActorCommand command = new CreateActorCommand(_context, _mapper);
        command.Model = newActor;
        CreateActorCommandValidator validator = new CreateActorCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }
}