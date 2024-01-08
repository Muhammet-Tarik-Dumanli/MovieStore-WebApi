using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;

public static class Movies
{
    public static void AddMovies(this MovieStoreDbContext context)
    {
        context.Movies.AddRange(
                new Movie{
                    Name = "TOP GUN",
                    Year = 1986,
                    Actors = context.Actors.Where(c=> new[] {1}.Contains(c.Id)).ToList(),
                    DirectorId = 3,
                    GenreId = 3,
                    Price = 90
                },
                new Movie{
                    Name = "IRON MAN",
                    Year = 2008,
                    Actors = context.Actors.Where(c => new[] { 2, 3 }.Contains(c.Id)).ToList(),
                    DirectorId = 2,
                    GenreId = 1,
                    Price = 103
                },
                new Movie{
                    Name = "The Green Mile",
                    Year = 1999,
                    Actors = context.Actors.Where(c => new[] { 1, 2, 3 }.Contains(c.Id)).ToList(),
                    DirectorId = 1,
                    GenreId = 2,
                    Price = 75
                });
    }
}