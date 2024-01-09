using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Commands.UpdateActor;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.ActorOperations.Commands.UpdateActor;
public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public UpdateActorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }
    
    [Fact]
    public void WhenTheActorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
    {
        //Arrange
        var actor = new Actor()
        {
            Name = "ForNotExistActor",
            LastName = "ForNotExistActor"
        };
        _context.Actors.Add(actor);
        _context.SaveChanges();

        var actorId = actor.Id;

        _context.Actors.Remove(actor);
        _context.SaveChanges();

        UpdateActorCommand command = new UpdateActorCommand(_context);
        command.ActorId = actor.Id;

        //Act
        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellemek istediğiniz aktör bulunamadı");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Actor_ShouldBeUpdated()
    {
        var actor = new Actor()
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Actor_ShouldBeUpdated"
        };
        _context.Actors.Add(actor);
        _context.SaveChanges();

        UpdateActorCommand command = new UpdateActorCommand(_context);
        UpdateActorModel model = new UpdateActorModel()
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Actor_ShouldBeUpdated"
        };

        command.Model = model;
        command.ActorId = actor.Id;

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();
        actor = _context.Actors.FirstOrDefault(c => c.Id == command.ActorId);
        actor.Should().NotBeNull();
        actor.Name.Should().Be(model.Name);
        actor.LastName.Should().Be(model.LastName);

    }
}

