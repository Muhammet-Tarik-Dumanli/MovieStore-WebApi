using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.ActorOperations.Commands.CreateActor;

public class CreateActorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateActorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistActorIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var actor = new Actor(){ Name = "WhenAlreadyExistActorIsGiven", LastName = "_InvalidOperationException_ShouldBeReturn"};
        _context.Actors.Add(actor);
        _context.SaveChanges();

        CreateActorCommand command = new CreateActorCommand(_context, _mapper);
        command.Model = new CreateActorModel(){Name = actor.Name};

        FluentActions
            .Invoking(() => command.Handle())
            .Should().ThrowAsync<InvalidOperationException>("Eklemek istenilen oyuncu zaten mevcut!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
    {
        CreateActorCommand command = new CreateActorCommand(_context, _mapper);
        CreateActorModel model = new CreateActorModel()
        {
            Name = "WhenValidInputsAreGiven_",
            LastName = "Actor_ShouldBeCreated"
        };
        command.Model = model;
        
        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();

        var actor = _context.Actors.SingleOrDefault(q => q.Name == model.Name && q.LastName == model.LastName);
    }
}