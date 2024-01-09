using FluentAssertions;
using MovieStoreWebApi.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.DirectorOperations.Command.UpdateDirector;
public class UpdateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public UpdateDirectorCommandValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Theory]
    [InlineData(1, "", "LastName")]
    [InlineData(1, "Name", "")]
    [InlineData(0, "Name", "LastName")]
    public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int id, string name, string lastName)
    {
        UpdateDirectorCommand command = new UpdateDirectorCommand(null);
        UpdateDirectorModel model = new UpdateDirectorModel
        {
            Name = name,
            LastName = lastName
        };
        command.DirectorId = id;
        command.Model = model;

        UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);

    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {

        UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
        var director = new Entities.Director
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Validator_ShouldNotBeReturnError"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        UpdateDirectorModel model = new UpdateDirectorModel();
        command.Model = new UpdateDirectorModel
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Validator_ShouldNotBeReturnError"
        };

        command.DirectorId = director.Id;

        UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}


