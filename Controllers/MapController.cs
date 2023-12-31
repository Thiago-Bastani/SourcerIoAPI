using Microsoft.AspNetCore.Mvc;
using SorcerIo.Domain;
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
            _mapService.CreateMap(player);
            return Ok($"Socket server started on port {port}.");
        }

        [HttpPost("destroy-map")]
        public IActionResult DestroyMap(Player player, ushort port)
        {
            _mapService.DestroyMap(player);
            return Ok($"Socket server on port {port} stopped.");
        }
    }
}
