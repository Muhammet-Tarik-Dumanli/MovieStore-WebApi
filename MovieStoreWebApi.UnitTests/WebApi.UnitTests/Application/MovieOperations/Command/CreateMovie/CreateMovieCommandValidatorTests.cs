using AutoMapper;
using FluentAssertions;
using MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebApi.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.Application.MovieOperations.Command.CreateMovie;
public class CreateMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateMovieCommandValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Theory]
    [InlineData("name", 2000, 1, 2, 0)]
    [InlineData("", 2000, 1, 2, 20)]
    [InlineData("name", 1800, 1, 2, 20)]
    [InlineData("name", 2000, 0, 2, 20)]
    [InlineData("name", 2000, 1, 0, 20)]
    public void WhenInvalidInputAreGiven_Validator_ShouldBeReturn(string name, int year, int genreId, int directorId, decimal price)
    {
        CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
        CreateMovieModel model = new CreateMovieModel
        {
            Name = name,
            DirectorId = directorId,
            GenreId = genreId,
            Year = year,
            Price = price
        };
        command.Model = model;

        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
        var director = new Director
        {
            Name = "WhenValidInputsAreGiven",
            LastName = "Validator_ShouldNotBeReturnError"
        };
        _context.Directors.Add(director);
        _context.SaveChanges();

        var genre = new Genre
        {
            Name = "WhenValidInputsAreGiven"
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();

        CreateMovieModel model = new CreateMovieModel()
        {
            Name = "WhenValidInputsAreGiven",
            Year = 2000,
            GenreId = genre.Id,
            DirectorId = director.Id,
            Price = 100
        };
        command.Model = model;

        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}


