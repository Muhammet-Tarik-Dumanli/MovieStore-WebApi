using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.DbOperations;

public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
{
    public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options)
    {
        
    }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    public int SaveChanges()
    {
        return base.SaveChanges();
    }
}
