using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.MovieOperationsOperations.Command.CreateMovie;
public class CreateMovieCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateMovieCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistMovie_InvalidOperationException_ShouldBeReturn()
    {
        var director = new Director
        {
            Name = "WhenAlreadyExistMovie",
            LastName = "InvalidOperationException_ShouldBeReturn"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        var genre = new Genre
        {
            Name = "WhenAlreadyExistMovie"
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();

        var movie = new Movie
        {
            Name = "WhenAlreadyExistMovie",
            Year = 2000,
            Price = 90,
            DirectorId = director.Id,
            GenreId = genre.Id
        };
        _context.Movies.Add(movie);
        _context.SaveChanges();

        CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
        command.Model = new CreateMovieModel() { Name = movie.Name, DirectorId = movie.DirectorId, GenreId = movie.GenreId, Price = movie.Price, Year = movie.Year };
        FluentActions
            .Invoking(() => command.Handle().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Eklemek istediğiniz film zaten mevcut");

    }
}

