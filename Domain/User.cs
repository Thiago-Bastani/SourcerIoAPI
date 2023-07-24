using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SorcerIo.Domain;

public class User
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public IEnumerable<Player>? Player { get; set; }
}