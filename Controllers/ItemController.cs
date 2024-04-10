using Microsoft.AspNetCore.Mvc;
using TMA_Warehouse.Interfaces;
using TMA_Warehouse.Models;

namespace TMA_Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IItemRepository itemRepository;
        public ItemController(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Item>))]
        public IActionResult GetItems() 
        {
            var items = itemRepository.GetItems();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(items);
        }

        [HttpGet("{itemId}")]
        [ProducesResponseType(200, Type = typeof(Item))]
        [ProducesResponseType(400)]
        public IActionResult GetItem(int itemId)
        {
            if (!itemRepository.IsExist(itemId))
                return NotFound();

            var item = itemRepository.GetItem(itemId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(item);
        }


        [HttpGet("{itemId}/quantity")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetQantity(int itemId)
        {
            if (!itemRepository.IsExist(itemId))
                return NotFound();

            var item = itemRepository.GetQantity(itemId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(item);
        }
        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateItem([FromBody] Item itemModel)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var item = new Item()
            {
                //Item_ID = itemModel.Item_ID,
                Item_Group = itemModel.Item_Group,
                Name = itemModel.Name,
                Unit_of_measurement = itemModel.Unit_of_measurement,
                Quantity = itemModel.Quantity,
                Price_without_VAT = itemModel.Price_without_VAT,
                Status = itemModel.Status,
                Storage_location = itemModel.Storage_location,
                Contact_person = itemModel.Contact_person
            };
            
            if(!itemRepository.CreateItem(item))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfuly Created");
                

        }

        [HttpPut("{itemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateItem(int itemId, [FromBody]Item updatedItem)
        {
            if(updatedItem == null)
                return BadRequest(ModelState);
            
            if (itemId != updatedItem.Item_ID)
                return BadRequest(ModelState);

            if (!itemRepository.IsExist(itemId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!itemRepository.UpdateItem(updatedItem))
            {
                ModelState.AddModelError("", "Something went wrong updating item");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{itemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteItem(int itemId)
        {
            if (!itemRepository.IsExist(itemId))
                return NotFound();

            var itemToDelete = itemRepository.GetItem(itemId);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (!itemRepository.DeleteItem(itemToDelete))
            {
                ModelState.AddModelError("", "Something went wrong updating item");
                return StatusCode(500, ModelState);
            }

            return NoContent();
            
        }

    }
}
