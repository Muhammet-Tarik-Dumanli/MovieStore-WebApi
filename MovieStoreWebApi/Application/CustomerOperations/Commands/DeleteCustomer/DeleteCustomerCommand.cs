using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.CustomerOperations.Commands.DeleteCustomer;

public class DeleteCustomerCommand
{
    public int CustomerId { get; set; }
    private readonly MovieStoreDbContext _context;

    public DeleteCustomerCommand(MovieStoreDbContext context)
    {
        _context = context;
    }

    public async Task Handle()
    {
        var Customer = _context.Customers.FirstOrDefault(q => q.Id == CustomerId);
        if(Customer is null)
            throw new InvalidOperationException("Silmek istediğiniz müşteri bulunamadı!");

        _context.Customers.Remove(Customer);
        await _context.SaveChangesAsync();
    }
}
