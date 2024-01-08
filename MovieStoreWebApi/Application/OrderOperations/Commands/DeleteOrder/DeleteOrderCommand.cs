using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.OrderOperations.Commands.DeleteOrder;

public class DeleteOrderCommand
{
    public int OrderId { get; set; }
    private readonly MovieStoreDbContext _context;

    public DeleteOrderCommand(MovieStoreDbContext context)
    {
        _context = context;
    }

    public async Task Handle()
    {
        var Order = _context.Orders.FirstOrDefault(q => q.Id == OrderId);
        if(Order is null)
            throw new InvalidOperationException("İptal etmek istediğiniz sipariş bulunamadı!");

        _context.Orders.Remove(Order);
        await _context.SaveChangesAsync();
    }
}
