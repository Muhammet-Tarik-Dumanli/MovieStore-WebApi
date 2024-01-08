using AutoMapper;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.Application.MovieOperations.Commands.CreateMovie;

public class CreateMovieCommand
{
    public CreateMovieModel Model { get; set; }
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateMovieCommand(MovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var movie = _context.Movies.FirstOrDefault(c => c.Name == Model.Name);
        if(movie is not null)
            throw new InvalidOperationException("Eklemek istenilen film zaten mevcut!");

        movie = _mapper.Map<Movie>(Model);
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
    }
}

public class CreateMovieModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public int DirectorId { get; set; }
    public int GenreId { get; set; }
}