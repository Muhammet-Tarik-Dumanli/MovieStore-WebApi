using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.CustomerOperations.Queries.GetCustomers;

public class GetCustomersQuery
{
    public GetCustomersModel Model { get; set; }
    public int CustomerId { get; set; }
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetCustomersQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetCustomersModel>> Handle()
    {
        var customerList = _context.Customers.OrderBy(q => q.Id).ToList();

        List<GetCustomersModel> vm = _mapper.Map<List<GetCustomersModel>>(customerList);
        return vm;
    }
}

public class GetCustomersModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public ICollection<string> Movies { get; set; }
}