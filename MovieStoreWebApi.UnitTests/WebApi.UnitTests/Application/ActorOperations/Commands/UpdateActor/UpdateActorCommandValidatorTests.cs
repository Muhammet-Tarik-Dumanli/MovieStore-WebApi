using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Commands.UpdateActor;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.ActorOperations.Commands.UpdateActor;
public class UpdateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public UpdateActorCommandValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Theory]
    [InlineData(1, "", "LastName")]
    [InlineData(1, "name", "")]
    [InlineData(0, "name", "LastName")]
    public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int id, string name, string lastName)
    {
        UpdateActorCommand command = new UpdateActorCommand(null);
        command.Model = new UpdateActorModel()
        {
            Name = name,
            LastName = lastName
        };
        command.ActorId = id;
        UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);

    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        UpdateActorCommand command = new UpdateActorCommand(_context);
        var actor = new Actor()
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Validator_ShouldNotBeReturnError"
        };
        _context.Actors.Add(actor);
        _context.SaveChanges();

        UpdateActorModel model = new UpdateActorModel();
        command.Model = new UpdateActorModel()
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Validator_ShouldNotBeReturnError"
        };
        command.ActorId = actor.Id;
        UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}


