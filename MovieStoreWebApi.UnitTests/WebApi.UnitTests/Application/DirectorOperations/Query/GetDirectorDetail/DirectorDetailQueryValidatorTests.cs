using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.DirectorOperations.Query.GetDirectorDetail;
public class DirectorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public DirectorDetailQueryValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenDirectorIdLessThanZero_Validator_ShouldBeReturnError()
    {
        GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context, _mapper);
        query.DirectorId = 0;

        GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
        var result = validator.Validate(query);
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

        var directorId = director.Id;

        GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context, _mapper);
        query.DirectorId = directorId;

        GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
        var result = validator.Validate(query);
        result.Errors.Count.Should().Be(0);
    }
}

