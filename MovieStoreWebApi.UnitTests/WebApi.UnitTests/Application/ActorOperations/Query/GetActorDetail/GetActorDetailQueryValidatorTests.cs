using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.ActorOperations.Query.GetActorDetail;
public class GetActorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetActorDetailQueryValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenActorIdLessThanZero_Validator_ShouldBeReturnError()
    {
        GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
        query.ActorId = 0;

        GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        var actor = new Actor()
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Validator_ShouldNotBeReturnError"
        };
        _context.Actors.Add(actor);
        _context.SaveChanges();

        GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
        query.ActorId = actor.Id;

        GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);

    }
}

