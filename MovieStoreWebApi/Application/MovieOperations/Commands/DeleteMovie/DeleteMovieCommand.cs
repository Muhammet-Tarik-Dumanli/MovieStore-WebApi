using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.MovieOperations.Commands.DeleteMovie;

public class DeleteMovieCommand
{
    public int MovieId { get; set; }
    private readonly MovieStoreDbContext _context;

    public DeleteMovieCommand(MovieStoreDbContext context)
    {
        _context = context;
    }

    public async Task Handle()
    {
        var Movie = _context.Movies.FirstOrDefault(q => q.Id == MovieId);
        if(Movie is null)
            throw new InvalidOperationException("Silmek istediğiniz film bulunamadı!");

        _context.Movies.Remove(Movie);
        await _context.SaveChangesAsync();
    }
}
