using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.MovieOperations.Query.GetMovieDetail;
public class MovieDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public MovieDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenTheMovieIsNotAvailable_InvalidOperationException_ShouldBeReturn()
    {
        var director = new Entities.Director
        {
            Name = "WhenTheMovieIsNotAvailable",
            LastName = "InvalidOperationException_ShouldBeReturn"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        var genre = new Entities.Genre
        {
            Name = "WhenTheMovieIsNotAvailable"
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();

        var movie = new Entities.Movie
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

        GetMovieDetailQuery query = new GetMovieDetailQuery(_context, _mapper);
        query.MovieId = movie.Id;

        FluentActions
            .Invoking(() => query.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film mevcut değil");
    }

    [Fact]
    public void WhenTheMovieIsAvailable_Movie_ShouldNotBeReturnErrors()
    {
        var director = new Entities.Director
        {
            Name = "WhenTheMovieIsAvailable",
            LastName = "Movie_ShouldNotBeReturnError"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();
        var genre = new Entities.Genre
        {
            Name = "WhenTheMovieIsAvailable"
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();
        var movie = new Entities.Movie
        {
            Name = "WhenTheMovieIsAvailable",
            Year = 2000,
            DirectorId = director.Id,
            GenreId = genre.Id,
            Price = 200
        };
        _context.Movies.Add(movie);
        _context.SaveChanges();

        var movieId = movie.Id;
        GetMovieDetailQuery query = new GetMovieDetailQuery(_context, _mapper);
        query.MovieId = movie.Id;

        FluentActions
            .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
    }
}


