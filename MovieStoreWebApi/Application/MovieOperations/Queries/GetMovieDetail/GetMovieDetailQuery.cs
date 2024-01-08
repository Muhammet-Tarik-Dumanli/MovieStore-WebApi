using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.Application.MovieOperations.Queries.GetMovieDetail;

public class GetMovieDetailQuery
{
    public GetMovieDetailModel Model { get; set; }
    public int MovieId { get; set; }
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetMovieDetailQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetMovieDetailModel> Handle()
    {
        var movie = _context.Movies.Include(q => q.Actors).Include(q => q.Director).Include(q => q.Genre).FirstOrDefault(q => q.Id == MovieId);

        if(movie is null)
            throw new InvalidOperationException("Aranan film bulunamadı!");

        Model = _mapper.Map<GetMovieDetailModel>(movie);
        return Model;
    }
}

public class GetMovieDetailModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public string Director { get; set; }
    public string Genre { get; set; }
    public ICollection<Actor> Actors { get; set; }
}