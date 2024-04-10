using Microsoft.AspNetCore.Mvc;
using TMA_Warehouse.Dto;
using TMA_Warehouse.Interfaces;
using TMA_Warehouse.Models;

namespace TMA_Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : Controller
    {
        private readonly IRequestRepository requestRepository;
        public RequestController(IRequestRepository requestRepository)
        {
            this.requestRepository = requestRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RequestRow>))]
        public IActionResult GetRequests()
        {
            var requests = requestRepository.GetRequests();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(requests);
        }
        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRequest([FromBody] RequestDto request)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            
            var newRequest = new Request()
            {
                //Request_ID = request.Request_ID,
                Employee_name = request.Employee_name,
                Status = request.Status,
                Comment = request.Status
            };
            
            if(!requestRepository.CreateRequest(newRequest))
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
        public IActionResult UpdateRequest(int requestId, [FromBody]RequestDto updatedRequest)
        {
            if(updatedRequest == null)
                return BadRequest(ModelState);
            
            if (requestId != updatedRequest.Request_ID)
                return BadRequest(ModelState);

            if (!requestRepository.IsExist(requestId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = new Request()
            {
                Request_ID = updatedRequest.Request_ID,
                Employee_name = updatedRequest.Employee_name,
                Status = updatedRequest.Comment,
                Comment = updatedRequest.Status
            };

            if (!requestRepository.UpdateRequest(request))
            {
                ModelState.AddModelError("", "Something went wrong updating request");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{requestId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRequest(int requestId)
        {
            if (!requestRepository.IsExist(requestId))
                return NotFound();

            var requestToDelete = requestRepository.GetRequest(requestId);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            
            if (!requestRepository.DeleteRequest(requestToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting request");
                return StatusCode(500, ModelState);
            }

            return NoContent();
            
        }
        
        
    }
}
