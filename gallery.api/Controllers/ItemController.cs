using gallery.data.Model;
using gallery.data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace gallery.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IGalleryDbRepository _repository;

        public ItemController(ILogger<ItemController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _repository = new GalleryDbRepository(configuration);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ItemModel>), StatusCodes.Status200OK)]
        public IEnumerable<ItemModel> List()
        {
            return _repository.GetFakeItems();
        }

        [HttpGet]
        [Route("{itemId:int}")]
        [ProducesResponseType(typeof(ItemModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public IActionResult Get(int itemId)
        {
            var foundItem = _repository.GetFakeItems().Find((item) => item.Id == itemId);

            return foundItem != null ? Ok(foundItem) : NotFound();
        }
    }
}
