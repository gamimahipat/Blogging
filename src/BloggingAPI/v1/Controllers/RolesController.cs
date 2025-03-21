using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloggingAPI.Generic;

namespace BloggingAPI.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {
            var roles = await _rolesRepository.GetAllRolesAsync();
            return Ok(roles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRoles(int id)
        {
            var role = await _rolesRepository.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoles(int id, Roles roles)
        {
            if (id != roles.Id)
            {
                return BadRequest();
            }

            try
            {
                await _rolesRepository.UpdateRoleAsync(roles);
                await _rolesRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _rolesRepository.RoleExistsAsync(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<Roles>> PostRoles(Roles roles)
        {
            await _rolesRepository.AddRoleAsync(roles);
            await _rolesRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoles), new { id = roles.Id }, roles);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoles(int id)
        {
            var role = await _rolesRepository.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            await _rolesRepository.DeleteRoleAsync(role);
            await _rolesRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
