using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;

public static class Actors
{
    public static void AddActors(this MovieStoreDbContext context)
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
    }
}