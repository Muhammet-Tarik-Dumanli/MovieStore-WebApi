using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.OrderOperations.Queries.GetOrders;

public class GetOrdersQuery
{
    public GetOrdersModel Model { get; set; }
    public int OrderId { get; set; }
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetOrdersQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetOrdersModel>> Handle()
    {
        var OrderList = _context.Orders.OrderBy(q => q.Id).ToList();

        List<GetOrdersModel> vm = _mapper.Map<List<GetOrdersModel>>(OrderList);
        return vm;
    }
}

public class GetOrdersModel
{
    public string CustomerName { get; set; }
    public string PurchasedMovie { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchasedDate { get; set; }
}