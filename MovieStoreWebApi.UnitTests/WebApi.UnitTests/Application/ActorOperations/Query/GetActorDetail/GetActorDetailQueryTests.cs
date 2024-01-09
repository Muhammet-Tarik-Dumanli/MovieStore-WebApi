using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.ActorOperations.Query.GetActorDetail;
public class GetActorDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetActorDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenTheActorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
    {
        var actor = new Actor()
        {
            Name = "WhenTheActorIsNotAvailable",
            LastName = "InvalidOperationException_ShouldBeReturn"
        };
        _context.Actors.Add(actor);
        _context.SaveChanges();

        var actorId = actor.Id;
        _context.Actors.Remove(actor);
        _context.SaveChanges();

        GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
        query.ActorId = actorId;

        FluentActions
            .Invoking(() => query.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktör mevcut değil");
    }

    [Fact]
    public void WhenTheActorIsNotAvailable_Actor_ShouldNotBeReturnErrors()
    {
        var actor = new Actor
        {
            Name = "WhenTheActorIsNotAvailable",
            LastName = "Actor_ShouldNotBeReturnError"
        };
        _context.Actors.Add(actor);
        _context.SaveChanges();

        GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
        query.ActorId = actor.Id;

        FluentActions
            .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
    }
}

