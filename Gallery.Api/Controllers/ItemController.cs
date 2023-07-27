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
        public IEnumerable<ItemModel> List()
        {
            return _repository.GetAllItems();
        }

        [HttpGet]
        [Route("{itemId:int}")]
        public IActionResult GetFake(int itemId)
        {
            var foundItem = _repository.GetFakeItems().Find((item) => item.Id == itemId);

            return foundItem != null ? Ok(foundItem) : NotFound();
        }

        [HttpGet]
        [Route("Fake/{itemId:int}")]
        public IActionResult Get(int itemId)
        {
            var foundItem = _repository.GetItem(itemId);

            return foundItem != null ? Ok(foundItem) : NotFound();
        }

        [HttpPost]
        public ActionResult Post(ItemModel item)
        {
            try
            {
                var isSuccess = _repository.CreateItem(item);
                return isSuccess ? Ok() : BadRequest("Unable to create item.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return BadRequest("Unable to create item.");
            }
        }

        [HttpPut]
        public ActionResult Put(ItemModel item)
        {
            var isSuccess = _repository.UpdateItem(item);

            return isSuccess ? Ok() : BadRequest("Nothing to update.");
        }

        [HttpDelete]
        [Route("{itemId:int}")]
        public ActionResult Delete(int itemId)
        {
            var isSuccess = _repository.DeleteItem(itemId);

            return isSuccess ? Ok() : NotFound();
        }
    }
}
