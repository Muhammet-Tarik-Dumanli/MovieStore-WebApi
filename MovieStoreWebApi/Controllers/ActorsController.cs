using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApi.Application.ActorOperations.Commands.DeleteActor;
using MovieStoreWebApi.Application.ActorOperations.Commands.UpdateActor;
using MovieStoreWebApi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStoreWebApi.Application.ActorOperations.Queries.GetActors;
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

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        GetActorsQuery query = new GetActorsQuery(_context, _mapper);

        var result = await query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
        query.ActorId = id;

        GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
        validator.ValidateAndThrow(query);
        
        var result = await query.Handle();
        return Ok(result);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateActorModel updateActor)
    {
        UpdateActorCommand command = new UpdateActorCommand(_context);
        command.ActorId = id;
        command.Model = updateActor;
        UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        DeleteActorCommand command = new DeleteActorCommand(_context);
        command.ActorId = id;
        DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }
}