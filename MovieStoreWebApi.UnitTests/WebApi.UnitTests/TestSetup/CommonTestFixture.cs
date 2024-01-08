using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.Common;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;

public class CommonTestFixture
{
    public MovieStoreDbContext Context {get; set;}
    public IMapper Mapper { get; set; }
    public CommonTestFixture()
    {
        var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName: "MovieStoreDb").Options;
        Context = new MovieStoreDbContext(options);
        Context.Database.EnsureCreated();
        Context.AddActors();
        Context.AddMovies();
        Context.AddDirectors();
        Context.SaveChanges();

        Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
    }
}