using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SorcerIo.Domain;

public class Player
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Format { get; set; }
    public string? Color { get; set; }
    public PlayerAttributes? Attributes { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User? User { get; set; }

}