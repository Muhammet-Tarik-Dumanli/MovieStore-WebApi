using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.ActorOperations.Commands.CreateActor;

public class CreateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateActorCommandValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Theory]
    [InlineData("ActorName", "")]
    [InlineData("", "ActorLastName")]
    [InlineData("", "")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturn(string Name, string LastName)
    {
        CreateActorCommand command = new CreateActorCommand(null, null);
        command.Model = new CreateActorModel()
        {
            Name = Name,
            LastName = LastName
        };

        CreateActorCommandValidator validator = new CreateActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenvalidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        CreateActorCommand command = new CreateActorCommand(null, null);
        CreateActorModel model = new CreateActorModel()
        {
            Name = "WhenValidInputsAreGiven_",
            LastName = "Validator_ShouldNotBeReturnError"
        };
        command.Model = model;
        
        CreateActorCommandValidator validator = new CreateActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}