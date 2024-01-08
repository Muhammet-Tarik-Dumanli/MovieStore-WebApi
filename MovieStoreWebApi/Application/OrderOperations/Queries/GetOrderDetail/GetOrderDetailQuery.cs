using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.OrderOperations.Queries.GetOrderDetail;

public class GetOrderDetailQuery
{
    public GetOrderDetailModel Model { get; set; }
    public int OrderId { get; set; }
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetOrderDetailQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetOrderDetailModel> Handle()
    {
        var order = _context.Orders.FirstOrDefault(q => q.Id == OrderId);

        if(order is null)
            throw new InvalidOperationException("Aranan sipariş bulunamadı!");

        Model = _mapper.Map<GetOrderDetailModel>(order);
        return Model;
    }
}

public class GetOrderDetailModel
{
    public string CustomerName { get; set; }
    public string PurchasedMovie { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchasedDate { get; set; }
}