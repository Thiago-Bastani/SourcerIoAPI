
using SocketIOSharp.Server;
using SocketIOSharp.Server.Client;
using SorcerIo.Domain;

namespace SorcerIo.Services
{
    public class MapService
    {

        private static List<Map> _currentMaps = new List<Map>();

        public void CreateMap(Player owner, ushort port, string layout = "standard")
        {
            SocketIOServer socket = new SocketIOServer(new SocketIOServerOption(port));
            Map map = new Map(layout, owner, socket);
            if (FindMap(owner, port) == null)
            {
                _currentMaps.Add(map);
                StartServer(map);
            }
            else
            {
                throw new Exception("map already exists!");
            }
        }

        public void StartServer(Map map)
        {
            SetServerRules(map);
            map.Socket.Start();
        }

        public void SetServerRules(Map map)
        {
            map.Socket.OnConnection((SocketIOSocket socket) => HandleSocketConnection(socket));
        }

        public void DestroyMap(Player owner, ushort port)
        {
            Map? map = FindMap(owner, port);
            if (map != null)
            {
                map.Socket.Stop();
                _currentMaps.Remove(map);
            }
            else
            {
                throw new Exception("Map does not exist!");
            }
        }

        public void HandleSocketConnection(SocketIOSocket socket)
        {
            Console.WriteLine($"Client connected: {socket.ToString()}");
        }

        public Map? FindMap(Player owner, ushort port)
        {
            // consider only owner and socket port
            return _currentMaps.Find(cmap =>
            (cmap.Owner == owner) && (cmap.Socket.Option.Port == port));
        }
    }
}
