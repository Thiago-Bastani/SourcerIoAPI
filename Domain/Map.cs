using SocketIOSharp.Server;

public class Map
{
    public string Layout { get; set; }
    public Player Owner { get; set; }
    public SocketIOServer Socket { get; set; }
    public Map(string layout, Player owner, SocketIOServer socket)
    {
        Layout = layout;
        Owner = owner;
        Socket = socket;
    }
}