using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.DirectorOperations.Query.GetDirectorDetail;
public class DirectorDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public DirectorDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenTheDirectorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
    {
        var director = new Entities.Director
        {
            Name = "WhenTheDirectorIsNotAvailable",
            LastName = "InvalidOperationException_ShouldBeReturn"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        var directorId = director.Id;

        _context.Remove(director);
        _context.SaveChanges();

        GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context, _mapper);
        query.DirectorId = directorId;

        FluentActions
            .Invoking(() => query.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yönetmen mevcut değil");

    }

    [Fact]
    public void WhenTheDirectorIsAvailable_Director_ShouldNotBeReturnErrors()
    {
        var director = new Director
        {
            Name = "WhenTheDirectorIsAvailable",
            LastName = "Director_ShouldNotBeReturnError"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        var directorId = director.Id;

        GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context, _mapper);
        query.DirectorId = directorId;

        FluentActions
            .Invoking(() => query.Handle().GetAwaiter().GetResult()).Invoke();
    }
}

