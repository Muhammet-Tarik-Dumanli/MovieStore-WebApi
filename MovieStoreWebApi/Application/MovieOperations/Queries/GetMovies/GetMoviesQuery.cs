using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.Application.MovieOperations.Queries.GetMovies;

public class GetMoviesQuery
{
    public GetMoviesModel Model { get; set; }
    public int MovieId { get; set; }
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetMoviesQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetMoviesModel>> Handle()
    {
        var MovieList = _context.Movies.Include(q => q.Actors).Include(q => q.Director).Include(q => q.Genre).OrderBy(q => q.Id).ToList();

        List<GetMoviesModel> vm = _mapper.Map<List<GetMoviesModel>>(MovieList);
        return vm;
    }
}

public class GetMoviesModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public string Director { get; set; }
    public string Genre { get; set; }
    public ICollection<Actor> Actors { get; set; }

}