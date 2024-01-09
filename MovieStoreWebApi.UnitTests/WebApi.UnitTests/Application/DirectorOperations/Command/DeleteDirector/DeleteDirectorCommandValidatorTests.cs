using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebApi.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.DirectorOperations.Command.DeleteDirector;
public class DeleteDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public DeleteDirectorCommandValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenDirectorIdLessThanZero_Validator_ShouldBeReturnError()
    {
        DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
        command.DirectorId = 0;

        DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        var director = new Director
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Validator_ShouldNotBeReturnError"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        var directorId = director.Id;

        _context.Remove(director);
        _context.SaveChanges();

        DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
        command.DirectorId = directorId;

        DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}


