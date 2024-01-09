using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Commands.DeleteActor;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.ActorOperations.Commands.DeleteActor;

public class DeleteActorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public DeleteActorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenTheActorIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        var actor = new Actor()
        { 
            Name = "WhenTheActorIsNotExist", 
            LastName = "_InvalidOperationException_ShouldBeReturn"
        };
        _context.Actors.Add(actor);
        _context.SaveChanges();

        var actorId = actor.Id;
        _context.Actors.Remove(actor);
        _context.SaveChanges();

        DeleteActorCommand command = new DeleteActorCommand(_context);
        command.ActorId = actorId;

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>("Silmek istediğiniz oyuncu bulunamadı!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_DeleteActor_ShouldNotBeReturnError()
    {
        var actor = new Actor()
        {
            Name = "WhenValidInputsAreGiven_",
            LastName = "_DeleteActor_ShouldNotBeReturnError"
        };
        _context.Actors.Add(actor);
        _context.SaveChanges();
        
        DeleteActorCommand command = new DeleteActorCommand(_context);
        command.ActorId = actor.Id;
        
        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
    }
}