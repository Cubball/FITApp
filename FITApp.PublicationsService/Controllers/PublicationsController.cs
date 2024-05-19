using FITApp.PublicationsService.Contracts.Requests;
using FITApp.PublicationsService.Contracts.Responses;
using FITApp.PublicationsService.Helpers;
using FITApp.PublicationsService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FITApp.PublicationsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicationsController(IPublicationsService publicationsService) : ControllerBase
    {
        private readonly IPublicationsService _publicationsService = publicationsService;

        [HttpGet]
        public async Task<ActionResult<AllPublicationsDTO>> GetAll(int pageNumber, int pageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest();
            }

            var userId = this.GetUserId();
            var result = await _publicationsService.GetAll(userId, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FullPublication>> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var result = await _publicationsService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UpsertPublicationDTO publicationDTO)
        {
            if (!publicationDTO.Validate())
            {
                return BadRequest();
            }

            var userId = this.GetUserId();
            await _publicationsService.CreateAsync(publicationDTO, userId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            await _publicationsService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] UpsertPublicationDTO publicationDTO)
        {
            if (string.IsNullOrEmpty(id) || !publicationDTO.Validate())
            {
                return BadRequest();
            }

            await _publicationsService.UpdateAsync(id, publicationDTO);
            return Ok();
        }
    }
}
