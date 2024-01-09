using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.DirectorOperations.Command.CreateDirector;
public class CreateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateDirectorCommandValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Theory]
    [InlineData("", "LastName")]
    [InlineData("Name", "")]
    public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(string name, string lastName)
    {
        CreateDirectorCommand command = new CreateDirectorCommand(null, null);
        CreateDirectorModel model = new CreateDirectorModel()
        {
            Name = name,
            LastName = lastName
        };
        command.Model = model;

        CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        CreateDirectorCommand command = new CreateDirectorCommand(null, null);
        CreateDirectorModel model = new CreateDirectorModel()
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Validator_ShouldNotBeReturnError"
        };
        command.Model = model;

        CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}


