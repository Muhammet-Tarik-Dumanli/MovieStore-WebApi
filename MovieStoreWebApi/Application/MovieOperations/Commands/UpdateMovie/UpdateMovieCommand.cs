using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.MovieOperations.Commands.UpdateMovie;

public class UpdateMovieCommand
{
    public UpdateMovieModel Model { get; set; }
    public int MovieId { get; set; }
    private readonly MovieStoreDbContext _context;

    public UpdateMovieCommand(MovieStoreDbContext context)
    {
        _context = context;
    }

    public async Task Handle()
    {
        var Movie = _context.Movies.FirstOrDefault(q => q.Id == MovieId);

        if(Movie is null)
            throw new InvalidOperationException("Güncellenecek film bulunamadı!");

        Movie.Name = Model.Name != default ? Model.Name : Movie.Name;
        Movie.Price = Model.Price != default ? Model.Price : Movie.Price;
        await _context.SaveChangesAsync();
    }
}

public class UpdateMovieModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}