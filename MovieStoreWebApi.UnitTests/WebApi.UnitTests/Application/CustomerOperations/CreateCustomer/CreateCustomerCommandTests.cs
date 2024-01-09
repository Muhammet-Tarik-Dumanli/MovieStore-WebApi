using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.CustomerOperations.CreateCustomer;
public class CreateCustomerCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateCustomerCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistCustomer_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new Entities.Genre
        {
            Name = "WhenAlreadyExistCustomer"
        };
        _context.Genres.Add(genre);
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

        CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
        command.Model = new CreateCustomerModel() { Name = customer.Name, LastName = customer.LastName };

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Eklemek istediğiniz müşteri zaten var");
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Customer_ShouldBeCreated()
    {
        CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
        CreateCustomerModel model = new CreateCustomerModel()
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Customer_ShouldBeCreated",
            Email = "deneme@deneme.com",
            Password = "123456",
            RefreshToken = "null"
        };
        command.Model = model;

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();

        var customer = _context.Customers.SingleOrDefault(c => c.Name == model.Name && c.LastName == model.LastName);
        customer.Should().NotBeNull();
    }

}

