using FluentAssertions;
using MovieStoreWebApi.Application.MovieOperations.Commands.UpdateMovie;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.MovieOperations.Command.UpdateMovie;

public class UpdateMovieCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public UpdateMovieCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn()
    {
        var director = new Director
        {
            Name = "WhenTheMovieIsNotAvailable",
            LastName = "nvalidOperationException_ShouldBeReturnUpdateMovie"
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

        UpdateMovieCommand command = new UpdateMovieCommand(_context);
        command.MovieId = movieId;
        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellemek istediğiniz film mevcut değil");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Movie_ShouldBeUpdated()
    {
        var director = new Director
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Movie_ShouldBeUpdated"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        var actor = new Actor
        {
            Name = "ForHappyCodeUpdateMovie",
            LastName = "ForHappyCodeUpdateMovie"
        };
        _context.Actors.Add(actor);
        _context.SaveChanges();

        var genre = new Genre
        {
            Name = "ForHappyCodeUpdateMovie"
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();

        var movie = new Movie
        {
            Name = "ForHappyCodeUpdateMovie",
            Year = 2001,
            DirectorId = director.Id,
            GenreId = genre.Id,
            Price = 200
        };
        _context.Movies.Add(movie);
        _context.SaveChanges();

        var movieId = movie.Id;
        UpdateMovieModel model = new UpdateMovieModel
        {
            Name = "UpdateTest",
            Price = 308,
        };

        UpdateMovieCommand command = new UpdateMovieCommand(_context);
        command.MovieId = movieId;
        command.Model = model;
        
        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        movie = _context.Movies.FirstOrDefault(c => c.Id == command.MovieId);
        movie.Name.Should().Be(model.Name);
        movie.Price.Should().Be(model.Price);
    }
}


