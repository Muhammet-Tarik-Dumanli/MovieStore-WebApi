using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.MovieOperations.Queries.GetMovies;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.MovieOperations.Query.GetMovies;
public class GetMovieQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetMovieQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenQueryGetResult_Movie_ShouldNotBeReturnErrors()
    {
        GetMoviesQuery query = new GetMoviesQuery(_context, _mapper);
        FluentActions
            .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
    }
}


