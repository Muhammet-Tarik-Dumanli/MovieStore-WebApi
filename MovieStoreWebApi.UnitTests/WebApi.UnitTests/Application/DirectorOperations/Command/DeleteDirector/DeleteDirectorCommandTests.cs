using FluentAssertions;
using MovieStoreWebApi.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.DirectorOperations.Command.DeleteDirector;
public class DeleteDirectorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public DeleteDirectorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
    {
        //Arrange
        var director = new Entities.Director()
        {
            Name = "WhenTheDirectorIsNotAvailable",
            LastName = "InvalidOperationException_ShouldBeReturn",
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        var directorId = director.Id;

        _context.Directors.Remove(director);
        _context.SaveChanges();
        DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
        command.DirectorId = directorId;

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz yönetmen mevcut değil");

    }

    [Fact]
    public void WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn2()
    {
        var director = new Director()
        {
            Name = "WhenTheDirectorIsNotAvailable",
            LastName = "InvalidOperationException_ShouldBeReturn",
        };
        _context.Directors.Add(director);

        var genre = new Genre
        {
            Name = "WhenTheDirectorIsNotAvailable"
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();

        var movie = new Movie
        {
            GenreId = genre.Id,
            DirectorId = director.Id,
            Name = "WhenTheDirectorIsNotAvailable",
            Price = 100,
            Year = 1999
        };
        _context.Movies.Add(movie);
        _context.SaveChanges();
        var directorId = director.Id;

        DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
        command.DirectorId = directorId;

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz yönetmenin filmi mevcut");

    }

    [Fact]
    public void WhenValidInputsAreGiven_DeleteActor_ShouldNotBeReturnError()
    {
        var director = new Director
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "DeleteActor_ShouldNotBeReturnError"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
        command.DirectorId = director.Id;

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
    }
}


