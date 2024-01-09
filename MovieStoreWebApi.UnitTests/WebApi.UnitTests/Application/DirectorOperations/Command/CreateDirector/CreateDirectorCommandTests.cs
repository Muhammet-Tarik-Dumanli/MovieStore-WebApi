using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.DirectorOperations.Command.CreateDirector;
public class CreateDirectorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateDirectorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistDirector_InvalidOperationException_ShouldBeReturn()
    {
        var director = new Entities.Director()
        {
            Name = "WhenAlreadyExistDirector",
            LastName = "InvalidOperationException_ShouldBeReturn"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper);
        command.Model = new CreateDirectorModel() { Name = director.Name, LastName = director.LastName };

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Eklemek istediğiniz yönetmen mevcut");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Director_ShouldBeCreated()
    {
        CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper);
        CreateDirectorModel model = new CreateDirectorModel()
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "irector_ShouldBeCreated"
        };
        command.Model = model;

        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult()).Invoke();

        var director = _context.Directors.SingleOrDefault(c => c.Name == model.Name && c.LastName == model.LastName);
        director.Should().NotBeNull();
    }
}

