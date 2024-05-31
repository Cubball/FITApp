using FITApp.Auth.Attributes;
using FITApp.Auth.Data;
using FITApp.PublicationsService.Contracts;
using FITApp.PublicationsService.Helpers;
using FITApp.PublicationsService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FITApp.PublicationsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthorsController(IAuthorService authorService) : ControllerBase
    {
        private readonly IAuthorService _authorService = authorService;

        [HttpPut("{id}")]
        [RequiresPermission(Permissions.All, Permissions.UsersUpdate)]
        public async Task<ActionResult> Update(string id, [FromBody] AuthorDTO authorDTO)
        {
            if (string.IsNullOrEmpty(id) || !authorDTO.Validate())
            {
                return BadRequest();
            }

            await _authorService.UpdateAsync(id, authorDTO);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] AuthorDTO authorDTO)
        {
            if (!authorDTO.Validate())
            {
                return BadRequest();
            }

            var userId = this.GetUserId();
            await _authorService.UpdateAsync(userId, authorDTO);
            return Ok();
        }
    }
}
