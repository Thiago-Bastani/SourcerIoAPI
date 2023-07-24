using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SorcerIo.Domain;
public class PlayerAttributes
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Player")]
    public int PlayerId { get; set; }
    public Player? Player { get; set; }
    public int Speed { get; set; }
    public int Strength { get; set; }

}