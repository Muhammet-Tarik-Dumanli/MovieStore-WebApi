using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.ActorOperations.Queries.GetActors;

public class GetActorsQuery
{
    public GetActorsModel Model { get; set; }
    public int ActorId { get; set; }
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetActorsQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetActorsModel>> Handle()
    {
        var actorList = _context.Actors.Include(q => q.Movies).OrderBy(q => q.Id).ToList();

        List<GetActorsModel> vm = _mapper.Map<List<GetActorsModel>>(actorList);
        return vm;
    }
}

public class GetActorsModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public ICollection<string> Movies { get; set; }
}