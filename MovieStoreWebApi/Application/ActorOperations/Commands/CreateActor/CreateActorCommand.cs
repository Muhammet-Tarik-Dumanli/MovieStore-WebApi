using AutoMapper;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;

public class CreateActorCommand
{
    public CreateActorModel Model { get; set; }
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateActorCommand(MovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var actor = _context.Actors.FirstOrDefault(c => c.Name == Model.Name && c.LastName == Model.LastName);
        if(actor is not null)
            throw new InvalidOperationException("Eklemek istenilen oyuncu zaten mevcut!");

        actor = _mapper.Map<Actor>(Model);
        _context.Actors.Add(actor);
        await _context.SaveChangesAsync();
    }
}

public class CreateActorModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
}