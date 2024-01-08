using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.DirectorOperations.Commands.DeleteDirector;

public class DeleteDirectorCommand
{
    public int DirectorId { get; set; }
    private readonly MovieStoreDbContext _context;

    public DeleteDirectorCommand(MovieStoreDbContext context)
    {
        _context = context;
    }

    public async Task Handle()
    {
        var Director = _context.Directors.FirstOrDefault(q => q.Id == DirectorId);
        if(Director is null)
            throw new InvalidOperationException("Silmek istediğiniz yönetmen bulunamadı!");

        _context.Directors.Remove(Director);
        await _context.SaveChangesAsync();
    }
}
