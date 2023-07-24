using SocketIOSharp.Server;
using SocketIOSharp.Server.Client;
using SorcerIo.Domain;

namespace SorcerIo.Services;

public class SocketService
{
    private const ushort START_PORT = 3000;
    private static List<SocketIOServer> _runningSockets = new List<SocketIOServer>();

    public void connectMapToASocket(Map map)
    {
        SocketIOServer socket = loadBalancer().Item1;
        Boolean socketAlreadyExists = loadBalancer().Item2;
        map.Socket = socket;
        if (!socketAlreadyExists)
        {
            SetSocketRules(map);
            StartServer(socket);
        }
    }

    public void StartServer(SocketIOServer socket)
    {
        socket.Start();
    }


    public (SocketIOServer, Boolean) loadBalancer()
    {
        SocketIOServer socket = new SocketIOServer(new SocketIOServerOption(HandlePortGeneration()));
        Boolean socketAlreadyExists = _runningSockets.Find(rsocket => rsocket.Option.Port == socket.Option.Port) != null;
        // add socket if it doesn't already exist on the list
        if (!socketAlreadyExists)
        {
            _runningSockets.Add(socket);
            return (socket, socketAlreadyExists);
        }
        else
        {
            SocketIOServer lastCreatedSocket = _runningSockets[_runningSockets.Count() - 1];
            // if the port of the new socket is already beeing used, map gets another socket to run on it
            return (RecicleAvailableSocket() ?? lastCreatedSocket, socketAlreadyExists);
        }
    }

    public void SetSocketRules(Map map)
    {
        map.Socket.OnConnection((SocketIOSocket socket) => HandleSocketConnection(socket));
    }

    public SocketIOServer? RecicleAvailableSocket()
    {
        foreach (SocketIOServer socket in _runningSockets)
        {
            if (socket.ClientsCounts < 50)
                return socket;
        }
        return null;
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

}