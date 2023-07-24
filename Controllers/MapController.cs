using Microsoft.AspNetCore.Mvc;
using SorcerIo.Services;

namespace SorcerIo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapController : ControllerBase
    {
        private readonly MapService _mapService;

        public MapController(MapService mapService)
        {
            _mapService = mapService;
        }

        [HttpPost("create-map")]
        public IActionResult CreateMap(Player player, ushort port)
        {
            _mapService.CreateMap(player, port);
            return Ok($"Socket server started on port {port}.");
        }

        [HttpPost("destroy-map")]
        public IActionResult DestroyMap(Player player, ushort port)
        {
            _mapService.DestroyMap(player, port);
            return Ok($"Socket server on port {port} stopped.");
        }
    }
}
