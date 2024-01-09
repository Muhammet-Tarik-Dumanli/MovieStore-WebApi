using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.CustomerOperations.CreateCustomer;
public class CreateCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateCustomerCommandValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Theory]
    [InlineData("", "LastName")]
    [InlineData("Name", "")]
    public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(string name, string LastName)
    {
        CreateCustomerCommand command = new CreateCustomerCommand(null, null);
        CreateCustomerModel model = new CreateCustomerModel
        {
            Name = name,
            LastName = LastName
        };
        command.Model = model;
        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        CreateCustomerCommand command = new CreateCustomerCommand(null, null);
        CreateCustomerModel model = new CreateCustomerModel
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Validator_ShouldNotBeReturnError"
        };
        command.Model = model;
        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);

    }
}


