using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.UnitTests.WebApi.UnitTests.TestSetup;

public static class Directors
{
    public static void AddDirectors(this MovieStoreDbContext context)
    {
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
    }
}