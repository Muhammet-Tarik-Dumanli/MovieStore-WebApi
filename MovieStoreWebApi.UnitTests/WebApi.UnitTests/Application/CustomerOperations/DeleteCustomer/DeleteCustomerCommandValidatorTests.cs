using FluentAssertions;
using MovieStoreWebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.CustomerOperations.DeleteCustomer;
public class DeleteCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public DeleteCustomerCommandValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
    {
        var customer = new Customer
        {
            Name = "WhenActorIdLessThanZero",
            LastName = "Validator_ShouldBeReturnError",
            Email = "deneme@deneme.com",
            Password = "123456",
            RefreshToken = "null"
        };
        _context.Customers.Add(customer);
        _context.SaveChanges();

        DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
        command.CustomerId = 0;

        DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {

        var customer = new Entities.Customer
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Validator_ShouldNotBeReturnError",
            Email = "deneme@deneme.com",
            Password = "123456",
            RefreshToken = "null"
        };
        _context.Customers.Add(customer);
        _context.SaveChanges();

        DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
        command.CustomerId = customer.Id;

        DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}

