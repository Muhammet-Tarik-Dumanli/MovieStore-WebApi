using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebApi.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.MovieOperations.Query.GetMovieDetail;
public class MovieDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public MovieDetailQueryValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenMovieIdLessThanZero_Validator_ShouldBeReturnError()
    {
        GetMovieDetailQuery query = new GetMovieDetailQuery(_context, _mapper);
        query.MovieId = 0;

        GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
        var result = validator.Validate(query);
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

        var movieId = movie.Id;
        GetMovieDetailQuery query = new GetMovieDetailQuery(_context, _mapper);
        query.MovieId = movieId;

        GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
        var result = validator.Validate(query);
        result.Errors.Count.Should().Be(0);
    }
}


