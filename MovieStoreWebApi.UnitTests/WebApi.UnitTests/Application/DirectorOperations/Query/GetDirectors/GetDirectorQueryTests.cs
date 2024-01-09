using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.DirectorOperations.Queries.GetDirectors;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.DirectorOperations.Query.GetDirectors;
public class GetDirectorQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetDirectorQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }
    public void WhenQueryGetResult_Director_ShouldNotBeReturnErrors()
    {
        GetDirectorsQuery query = new GetDirectorsQuery(_context, _mapper);
        FluentActions
            .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
    }
}


