using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.DirectorOperations.Queries.GetDirectors;

public class GetDirectorsQuery
{
    public GetDirectorsModel Model { get; set; }
    public int DirectorId { get; set; }
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetDirectorsQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetDirectorsModel>> Handle()
    {
        var directorList = _context.Directors.OrderBy(q => q.Id).ToList();

        List<GetDirectorsModel> vm = _mapper.Map<List<GetDirectorsModel>>(directorList);
        return vm;
    }
}

public class GetDirectorsModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public ICollection<string> Movies { get; set; }
}