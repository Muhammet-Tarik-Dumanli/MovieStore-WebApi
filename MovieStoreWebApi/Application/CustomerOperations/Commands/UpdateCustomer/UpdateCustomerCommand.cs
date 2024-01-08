using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.CustomerOperations.Commands.UpdateCustomer;

public class UpdateCustomerCommand
{
    public UpdateCustomerModel Model { get; set; }
    public int CustomerId { get; set; }
    private readonly MovieStoreDbContext _context;

    public UpdateCustomerCommand(MovieStoreDbContext context)
    {
        _context = context;
    }

    public async Task Handle()
    {
        var Customer = _context.Customers.FirstOrDefault(q => q.Id == CustomerId);

        if(Customer is null)
            throw new InvalidOperationException("Güncellenecek oyuncu bulunamadı!");

        Customer.Name = Model.Name != default ? Model.Name : Customer.Name;
        Customer.LastName = Model.LastName != default ? Model.LastName : Customer.LastName;
        await _context.SaveChangesAsync();
    }
}

public class UpdateCustomerModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}