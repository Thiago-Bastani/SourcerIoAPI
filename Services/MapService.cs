
using SocketIOSharp.Server;
using SocketIOSharp.Server.Client;
using SorcerIo.Domain;

namespace SorcerIo.Services
{
    public class MapService
    {

        private static List<Map> _runningMaps = new List<Map>();
        private static List<SocketIOServer> _runningSockets = new List<SocketIOServer>();
        private const ushort START_PORT = 3000;

        public void CreateMap(Player owner, string layout = "standard")
        {
            Map map = new Map(layout, owner);
            if (FindMap(owner) == null)
            {
                _runningMaps.Add(map);
                connectMapToASocket(map);
            }
            else
            {
                throw new Exception("map already exists!");
            }
        }

        public void connectMapToASocket(Map map)
        {
            SocketIOServer socket = new SocketIOServer(new SocketIOServerOption(HandlePortGeneration()));
            // add socket if it doesn't already exist on the list
            if (_runningSockets.Find(rsocket => rsocket.Option.Port == socket.Option.Port) == null)
            {
                _runningSockets.Add(socket);
                SetServerRules(map);
                StartServer(socket);
            }
            else
            {
                // if the port is beeing used by another socket, map gets that socket to run on it
                map.Socket = _runningSockets[_runningSockets.Count() - 1];
            }
        }

        public void StartServer(SocketIOServer socket)
        {
            socket.Start();
        }

        public void SetServerRules(Map map)
        {
            map.Socket.OnConnection((SocketIOSocket socket) => HandleSocketConnection(socket));
        }

        public void DestroyMap(Player owner)
        {
            Map? map = FindMap(owner);
            if (map != null)
            {
                _runningMaps.Remove(map);
            }
            else
            {
                throw new Exception("Map does not exist!");
            }
            SocketGarbageCollector();
        }

        public void HandleSocketConnection(SocketIOSocket socket)
        {
            Console.WriteLine($"Client connected: {socket.ToString()}");
        }

        public ushort HandlePortGeneration()
        {
            SocketIOServer lastAddedSocket = _runningSockets[_runningSockets.Count() - 1];
            // eatch port can have 50 clients
            if (lastAddedSocket.ClientsCounts > 50)
            {
                return Convert.ToUInt16(START_PORT + _runningSockets.Count()); // go to next port
            }
            else
            {
                return Convert.ToUInt16(START_PORT + (_runningSockets.Count() - 1)); // stay in the current port (START_PORT + socketsCount - 1)
            }
        }

        public void SocketGarbageCollector()
        {
            foreach (SocketIOServer socket in _runningSockets)
            {
                if (socket.ClientsCounts == 0)
                {
                    socket.Stop();
                    _runningSockets.Remove(socket);
                }
            }
        }

        public Map? FindMap(Player owner)
        {
            return _runningMaps.Find(cmap => cmap.Owner == owner);
        }
    }
}
