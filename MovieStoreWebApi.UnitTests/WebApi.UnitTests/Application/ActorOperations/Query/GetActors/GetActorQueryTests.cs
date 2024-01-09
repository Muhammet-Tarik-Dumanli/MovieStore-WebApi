using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Queries.GetActors;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.ActorOperations.Query.GetActorDetail;
public class GetActorQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetActorQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenQueryGetResult_Actor_ShouldNotBeReturnErrors()
    {
        GetActorsQuery query = new GetActorsQuery(_context, _mapper);
        FluentActions.Invoking(() => query.Handle().GetAwaiter().GetResult());
    }
}
