using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.ActorOperations.Commands.DeleteActor;

public class DeleteActorCommand
{
    public int ActorId { get; set; }
    private readonly MovieStoreDbContext _context;

    public DeleteActorCommand(MovieStoreDbContext context)
    {
        _context = context;
    }

    public async Task Handle()
    {
        var actor = _context.Actors.FirstOrDefault(q => q.Id == ActorId);
        if(actor is null)
            throw new InvalidOperationException("Silmek istediğiniz oyuncu bulunamadı!");

        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();
    }
}
