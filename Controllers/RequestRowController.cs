using Microsoft.AspNetCore.Mvc;
using TMA_Warehouse.Dto;
using TMA_Warehouse.Interfaces;
using TMA_Warehouse.Models;

namespace TMA_Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestRowController : Controller
    {
        private readonly IRequestRowRepository requestRowRepository;
        public RequestRowController(IRequestRowRepository requestRowRepository)
        {
            this.requestRowRepository = requestRowRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RequestRow>))]
        public IActionResult GetItems()
        {
            var requestRows = requestRowRepository.GetRequestRows();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(requestRows);
        }
        
        [HttpGet("{requestRowId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RequestRow>))]
        public IActionResult GetRequestRow(int requestRowId)
        {
            var requestRow = requestRowRepository.GetRequestRow(requestRowId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(requestRow);
        }

        [HttpGet("RequestRow/{requestId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Request>))]
        public IActionResult GetRequestRowsByRequest(int requestId)
        {
            var requestRowByRequests = requestRowRepository.GetRequestRowsByRequest(requestId);
        
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            return Ok(requestRowByRequests);
        }
        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRequestRow([FromBody] RequestRowDto requestRow)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            
            var newRequestRow = new RequestRow()
            {
                Comment = requestRow.Comment,
                Item_ID = requestRow.Item_ID,
                Price_without_VAT = requestRow.Price_without_VAT,
                Quantity = requestRow.Quantity,
                Request_ID = requestRow.Request_ID,
                Unit_of_measurement = requestRow.Unit_of_measurement

            };
            
            if(!requestRowRepository.CreateRequestRow(newRequestRow))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfuly Created");
                

        }

        [HttpPut("{requestId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRequestRow(int requestId, [FromBody]RequestRowDto updatedRequestRow)
        {
            if(updatedRequestRow == null)
                return BadRequest(ModelState);
            
            if (requestId != updatedRequestRow.Item_ID)
                return BadRequest(ModelState);

            if (!requestRowRepository.IsExist(requestId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var requestRow = new RequestRow()
            {
                Comment = updatedRequestRow.Comment,
                Item_ID = updatedRequestRow.Item_ID,
                Price_without_VAT = updatedRequestRow.Price_without_VAT,
                Quantity = updatedRequestRow.Quantity,
                Request_ID = updatedRequestRow.Request_ID

            };

            if (!requestRowRepository.UpdateRequestRow(requestRow))
            {
                ModelState.AddModelError("", "Something went wrong updating request row");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{requestRowId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRequestRow(int requestRowId)
        {
            if (!requestRowRepository.IsExist(requestRowId))
                return NotFound();

            var requestRowToDelete = requestRowRepository.GetRequestRow(requestRowId);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (!requestRowRepository.DeleteRequestRow(requestRowToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting requestRow");
                return StatusCode(500, ModelState);
            }

            return NoContent();
            
        }

    }
}
