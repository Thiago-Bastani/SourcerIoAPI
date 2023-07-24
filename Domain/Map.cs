using SocketIOSharp.Server;
namespace SorcerIo.Domain;
public class Map
{
    public Guid Id { get; set; }
    public string Layout { get; set; }
    public Player Owner { get; set; }
    public IList<Player>? ConnectedPlayers { get; set; }
    public SocketIOServer Socket { get; set; }
    public Map(string layout, Player owner, SocketIOServer socket)
    {
        Layout = layout;
        Owner = owner;
        Socket = socket;
    }
}