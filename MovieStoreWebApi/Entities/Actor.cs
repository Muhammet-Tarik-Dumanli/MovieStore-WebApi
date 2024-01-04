using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreWebApi.Entities;

public class Actor
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public ICollection<Movie> Movies { get; set; }
}