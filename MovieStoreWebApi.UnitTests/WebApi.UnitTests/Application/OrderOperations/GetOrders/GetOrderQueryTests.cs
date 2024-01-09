using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.OrderOperations.Queries.GetOrders;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.OrderOperations.GetOrders;
public class GetOrderQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetOrderQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }
    [Fact]
    public void WhenQueryGetResult_Order_ShouldNotBeReturnErrors()
    {
        GetOrdersQuery query = new GetOrdersQuery(_context, _mapper);
        FluentActions
            .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
    }
}


