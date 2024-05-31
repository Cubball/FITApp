using FITApp.PublicationsService.Contracts.Requests;
using FITApp.PublicationsService.Contracts.Responses;
using FITApp.PublicationsService.Exceptions;
using FITApp.PublicationsService.Helpers;
using FITApp.PublicationsService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FITApp.PublicationsService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PublicationsController(IPublicationsService publicationsService) : ControllerBase
    {
        private readonly IPublicationsService _publicationsService = publicationsService;

        [HttpGet]
        public async Task<ActionResult<AllPublicationsDTO>> GetAll(int page, int pageSize)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest();
            }

            var userId = this.GetUserId();
            var result = await _publicationsService.GetAll(userId, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FullPublication>> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var userId = this.GetUserId();

            try
            {
                var result = await _publicationsService.GetById(id, userId);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (NotAllowedException)
            {
                return Forbid();
            }
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

            var userId = this.GetUserId();
            try
            {
                await _publicationsService.DeleteAsync(id, userId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (NotAllowedException)
            {
                return Forbid();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(
            string id,
            [FromBody] UpsertPublicationDTO publicationDTO
        )
        {
            if (string.IsNullOrEmpty(id) || !publicationDTO.Validate())
            {
                return BadRequest();
            }

            var userId = this.GetUserId();

            try
            {
                await _publicationsService.UpdateAsync(id, publicationDTO, userId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (NotAllowedException)
            {
                return Forbid();
            }
        }

        [HttpGet("/api/report")]
        public async Task<ActionResult> GetReport(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate || startDate > DateTime.Now || endDate > DateTime.Now)
            {
                return BadRequest();
            }

            var userId = this.GetUserId();

            var ms = await _publicationsService.GetReport(userId, startDate, endDate);
            return File(ms, "application/octet-stream", $"report_{startDate.ToString("dd-MM-yy")}_{endDate.ToString("dd-MM-yy")}.pdf");
        }
    }
}
