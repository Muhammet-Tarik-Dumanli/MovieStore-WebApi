using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreWebApi.Entities;

public class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Movie> PurchasedMovies { get; set; }
    public ICollection<Genre> FavoriteGenres { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpireDate { get; set; }
}