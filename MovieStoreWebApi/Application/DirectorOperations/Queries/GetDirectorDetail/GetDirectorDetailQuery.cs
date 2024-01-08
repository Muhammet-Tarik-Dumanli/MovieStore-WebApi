using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.DirectorOperations.Queries.GetDirectorDetail;

public class GetDirectorDetailQuery
{
    public GetDirectorDetailModel Model { get; set; }
    public int DirectorId { get; set; }
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetDirectorDetailQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetDirectorDetailModel> Handle()
    {
        var director = _context.Directors.FirstOrDefault(q => q.Id == DirectorId);

        if(director is null)
            throw new InvalidOperationException("Aranan yönetmen bulunamadı!");

        Model = _mapper.Map<GetDirectorDetailModel>(director);
        return Model;
    }
}

public class GetDirectorDetailModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public ICollection<string> Movies { get; set; }
}