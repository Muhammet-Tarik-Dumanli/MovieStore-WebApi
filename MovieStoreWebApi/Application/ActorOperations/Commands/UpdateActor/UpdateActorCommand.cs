using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.ActorOperations.Commands.UpdateActor;

public class UpdateActorCommand
{
    public UpdateActorModel Model { get; set; }
    public int ActorId { get; set; }
    private readonly MovieStoreDbContext _context;

    public UpdateActorCommand(MovieStoreDbContext context)
    {
        _context = context;
    }

    public async Task Handle()
    {
        var actor = _context.Actors.FirstOrDefault(q => q.Id == ActorId);

        if(actor is null)
            throw new InvalidOperationException("Güncellenecek oyuncu bulunamadı!");

        actor.Name = Model.Name != default ? Model.Name : actor.Name;
        actor.LastName = Model.LastName != default ? Model.LastName : actor.LastName;
        await _context.SaveChangesAsync();
    }
}

public class UpdateActorModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
}