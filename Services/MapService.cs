using SorcerIo.Domain;

namespace SorcerIo.Services
{
    public class MapService
    {

        private static List<Map> _runningMaps = new List<Map>();
        private SocketService _socketService;
        public MapService(SocketService socketService)
        {
            _socketService = socketService;
        }

        public void CreateMap(Player owner, string layout = "standard")
        {
            Map map = new Map(layout, owner);
            if (FindMap(owner) == null)
            {
                _runningMaps.Add(map);
                _socketService.connectMapToASocket(map);
            }
            else
            {
                throw new Exception("map already exists!");
            }
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
            _socketService.SocketGarbageCollector();
        }

        public Map? FindMap(Player owner)
        {
            return _runningMaps.Find(cmap => cmap.Owner == owner);
        }
    }
}
