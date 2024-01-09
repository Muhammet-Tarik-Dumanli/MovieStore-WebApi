using FluentAssertions;
using MovieStoreWebApi.Application.MovieOperations.Commands.DeleteMovie;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.MovieOperations.Command.DeleteMovie;
public class DeleteMovieCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public DeleteMovieCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn()
    {
        var director = new Director
        {
            Name = "WhenTheMovieIsNotAvailable",
            LastName = "InvalidOperationException_ShouldBeReturn"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        var genre = new Genre
        {
            Name = "WhenTheMovieIsNotAvailable"
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();

        var movie = new Movie
        {
            Name = "WhenTheMovieIsNotAvailable",
            Year = 2000,
            DirectorId = director.Id,
            GenreId = genre.Id,
            Price = 200
        };
        _context.Movies.Add(movie);
        _context.SaveChanges();

        var movieId = movie.Id;

        _context.Remove(movie);
        _context.SaveChanges();

        DeleteMovieCommand command = new DeleteMovieCommand(_context);
        command.MovieId = movieId;
        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz film mevcut değil");
    }

    [Fact]
    public void WhenValidInputsAreGiven_DeleteMovie_ShouldNotBeReturnError()
    {
        var director = new Director
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "DeleteMovie_ShouldNotBeReturnError"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        var genre = new Genre
        {
            Name = "WhenValidInputsAreGiven"
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();
        
        var movie = new Movie
        {
            Name = "WhenValidInputsAreGiven",
            Year = 2000,
            DirectorId = director.Id,
            GenreId = genre.Id,
            Price = 200
        };
        _context.Movies.Add(movie);
        _context.SaveChanges();

        var movieId = movie.Id;
        DeleteMovieCommand command = new DeleteMovieCommand(_context);
        command.MovieId = movieId;

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
    }
}


