using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.DbOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
        {
            context.Actors.AddRange(
                new Actor{
                    Id = 1,
                    Name = "Tom",
                    LastName = "HANKS"
                },
                new Actor{
                    Id = 2,
                    Name = "Al",
                    LastName = "PACINO"
                },
                new Actor{
                    Id = 3,
                    Name = "Marlon",
                    LastName = "BRANDO"
                }
            );
            
            context.Directors.AddRange(
                new Director{
                    Id = 1,
                    Name = "Quentin ",
                    LastName = "TARANTINO"
                },
                new Director{
                    Id = 2,
                    Name = "David ",
                    LastName = "FINCHER"
                },
                new Director{
                    Id = 3,
                    Name = "Christopher",
                    LastName = "NOLAN"
                }
            );

            context.Genres.AddRange(
                new Genre{
                    Id = 1,
                    Name = "Science Fiction "
                },
                new Genre{
                    Id = 2,
                    Name = "Action "
                },
                new Genre{
                    Id = 3,
                    Name = "Fantastic"
                }
            );

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
}