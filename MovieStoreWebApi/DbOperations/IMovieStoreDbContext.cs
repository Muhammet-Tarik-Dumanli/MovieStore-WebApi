using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.DbOperations;

public interface IMovieStoreDbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    int SaveChanges();
}