using FITApp.PublicationsService.Contracts.Requests;
using FITApp.PublicationsService.Helpers;
using FITApp.PublicationsService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FITApp.PublicationsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController(IAuthorService authorService) : ControllerBase
    {
        private readonly IAuthorService _authorService = authorService;

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] AuthorDTO authorDTO)
        {
            if (string.IsNullOrEmpty(id) || !authorDTO.Validate())
            {
                return BadRequest();
            }

            await _authorService.UpdateAsync(id, authorDTO);
            return Ok();
        }
    }
}