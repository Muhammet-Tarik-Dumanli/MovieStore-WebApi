using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.OrderOperations.Commands.CreateOrder;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.OrderOperations.CreateOrder;
public class CreateOrderCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateOrderCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }
    
    [Fact]
    public void WhenAlreadyExistOrder_InvalidOperationException_ShouldBeReturn()
    {

        var customer = new Entities.Customer
        {
            Name = "WhenAlreadyExistCustomer",
            LastName = "InvalidOperationException_ShouldBeReturn",
            Email = "deneme@deneme.com",
            Password = "123456",
            RefreshToken = "null"
        };
        _context.Customers.Add(customer);
        _context.SaveChanges();
        var order = new Entities.Order
        {
            CustomerId = customer.Id,
            MovieId = 1
        };
        _context.Orders.Add(order);
        _context.SaveChanges();

        CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
        command.Model = new CreateOrderModel { MovieId = 1, CustomerId = customer.Id };

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
    }

}


