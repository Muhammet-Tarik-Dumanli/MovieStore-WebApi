using FluentAssertions;
using MovieStoreWebApi.Application.MovieOperations.Commands.UpdateMovie;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.MovieOperations.Command.UpdateMovie;
public class UpdateMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public UpdateMovieCommandValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Theory]
    [InlineData(1, "Name", 1996, 1, 2, 0)]
    [InlineData(1, "", 2000, 1, 2, 20)]
    [InlineData(1, "Name", 1800, 1, 2, 20)]
    [InlineData(1, "Name", 2000, 0, 2, 20)]
    [InlineData(1, "Name", 2000, 1, 0, 20)]
    [InlineData(0, "Name", 2000, 1, 1, 20)]
    public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int id, string name, int year, int genreId, int directorId, decimal price)
    {
        UpdateMovieModel model = new UpdateMovieModel()
        {
            Name = name,
            Price = price
        };

        UpdateMovieCommand command = new UpdateMovieCommand(_context);
        command.Model = model;

        UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
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

        UpdateMovieModel model = new UpdateMovieModel()
        {
            Name = "WhenValidInputsAreGiven",
            Price = 300
        };
        var movieId = movie.Id;

        UpdateMovieCommand command = new UpdateMovieCommand(_context);
        command.Model = model;
        command.MovieId = movieId;

        UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}


