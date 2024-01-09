using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Commands.DeleteActor;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.ActorOperations.Commands.DeleteActor;
public class DeleteActorCommandValidatorTests:IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
    {
        DeleteActorCommand command = new DeleteActorCommand(null);
        command.ActorId = 0;

        DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        var result = validator.Validate(command);


        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
    {
        DeleteActorCommand command = new DeleteActorCommand(null);
        command.ActorId = 1;
        
        DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}

