using FluentAssertions;
using MovieStoreWebApi.Application.MovieOperations.Commands.DeleteMovie;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.MovieOperations.Command.DeleteMovie;
public class DeleteMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public DeleteMovieCommandValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
    {
        DeleteMovieCommand command = new DeleteMovieCommand(_context);
        command.MovieId = 0;

        DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        var director = new Entities.Director
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Validator_ShouldNotBeReturnError"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();
        var genre = new Entities.Genre
        {
            Name = "WhenValidInputsAreGiven"
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();
        var movie = new Entities.Movie
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

        DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}


