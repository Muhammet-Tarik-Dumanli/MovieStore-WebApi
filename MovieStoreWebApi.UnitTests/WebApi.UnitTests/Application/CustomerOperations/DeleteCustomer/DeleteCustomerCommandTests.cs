using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.CustomerOperations.DeleteCustomer;
public class DeleteCustomerCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public DeleteCustomerCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }
    public void WhenTheCustomerIsNotAvailable_InvalidOperationException_ShouldBeReturn()
    {
        var customer = new Entities.Customer
        {
            Name = "WhenTheCustomerIsNotAvailable",
            LastName = "InvalidOperationException_ShouldBeReturn"
        };
        _context.Customers.Add(customer);
        _context.SaveChanges();

        DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
        command.CustomerId = customer.Id;

        _context.Remove(customer);
        _context.SaveChanges();

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz aktör mevcut değil");
    }
    public void WhenValidInputsAreGiven_DeleteCustomer_ShouldNotBeReturnError()
    {
        var customer = new Customer
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "DeleteCustomer_ShouldNotBeReturnError"
        };
        _context.Customers.Add(customer);
        _context.SaveChanges();

        DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
        command.CustomerId = customer.Id;

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
    }
}


