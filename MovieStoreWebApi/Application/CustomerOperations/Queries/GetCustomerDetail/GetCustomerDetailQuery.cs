using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.CustomerOperations.Queries.GetCustomerDetail;

public class GetCustomerDetailQuery
{
    public GetCustomerDetailModel Model { get; set; }
    public int CustomerId { get; set; }
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetCustomerDetailQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetCustomerDetailModel> Handle()
    {
        var customer = _context.Customers.FirstOrDefault(q => q.Id == CustomerId);

        if(customer is null)
            throw new InvalidOperationException("Aranan müşteri bulunamadı!");

        Model = _mapper.Map<GetCustomerDetailModel>(customer);
        return Model;
    }
}

public class GetCustomerDetailModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public ICollection<string> Movies { get; set; }
}