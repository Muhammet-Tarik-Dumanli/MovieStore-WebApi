using AutoMapper;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;

public class CreateCustomerCommand
{
    public CreateCustomerModel Model { get; set; }
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateCustomerCommand(MovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var customer = _context.Customers.FirstOrDefault(c => c.Name == Model.Name && c.LastName == Model.LastName);
        if(customer is not null)
            throw new InvalidOperationException("Eklemek istenilen müşteri zaten mevcut!");

        customer = _mapper.Map<Customer>(Model);
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }
}

public class CreateCustomerModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RefreshToken { get; set; }
}