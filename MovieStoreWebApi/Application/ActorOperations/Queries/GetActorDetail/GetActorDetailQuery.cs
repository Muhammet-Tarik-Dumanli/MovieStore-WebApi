using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.ActorOperations.Queries.GetActorDetail;

public class GetActorDetailQuery
{
    public GetActorDetailModel Model { get; set; }
    public int ActorId { get; set; }
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetActorDetailQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetActorDetailModel> Handle()
    {
        var actor = _context.Actors.Include(q => q.Movies).FirstOrDefault(q => q.Id == ActorId);

        if(actor is null)
            throw new InvalidOperationException("Aranan oyuncu bulunamadı!");

        Model = _mapper.Map<GetActorDetailModel>(actor);
        return Model;
    }
}

public class GetActorDetailModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public ICollection<string> Movies { get; set; }
}