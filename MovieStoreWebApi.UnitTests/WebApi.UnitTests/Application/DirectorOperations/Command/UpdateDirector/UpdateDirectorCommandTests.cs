using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebApi.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.DirectorOperations.Command.UpdateDirector;
public class UpdateDirectorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public UpdateDirectorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }
    [Fact]
    public void WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
    {
        var director = new Entities.Director
        {
            Name = "WhenTheDirectorIsNotAvailable",
            LastName = "InvalidOperationException_ShouldBeReturn"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        var directorId = director.Id;

        _context.Directors.Remove(director);
        _context.SaveChanges();

        UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
        command.DirectorId = director.Id;

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yönetmen bulunamadı");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Director_ShouldBeUpdated()
    {
        var director = new Entities.Director
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Director_ShouldBeUpdated"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        UpdateDirectorCommand command = new UpdateDirectorCommand(_context);

        UpdateDirectorModel model = new UpdateDirectorModel()
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Director_ShouldBeUpdated"
        };
        command.DirectorId = director.Id;
        command.Model = model;

        FluentActions
            .Invoking(() => command.Handle()).Invoke();
        director = _context.Directors.FirstOrDefault(c => c.Id == command.DirectorId);
        director.Name.Should().Be(model.Name);
        director.LastName.Should().Be(model.LastName);

    }
}


