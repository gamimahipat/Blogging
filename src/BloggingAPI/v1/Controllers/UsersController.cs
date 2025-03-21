using BloggingAPI.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloggingAPI.v1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet("[action]")]
        [Authorize(Policy = AuthorizationPolicies.RequireUserOrAdmin)]
        public async Task<ActionResult<ApiResponse>> GetUsers()
        {
            return await _usersRepository.GetUsers();
        }

        //[HttpGet("[action]/{id}")]
        //public async Task<ActionResult<ApiResponse>> GetUserById(int id)
        //{
        //    if (id <= 0)
        //        return ApiResponse.ErrorResponse("Invalid user ID.");

        //    // Get logged-in user ID from claims
        //    var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        //        return ApiResponse.ErrorResponse("Invalid user session.");

        //    // Allow SuperAdmin & Admin to access all users
        //    bool isAdmin = User.IsInRole("SuperAdmin") || User.IsInRole("Admin");

        //    if (!isAdmin && userId != id)
        //        return ApiResponse.ErrorResponse("You do not have permission to view this user.");

        //    return await _usersRepository.GetUserById(id);
        //}

        [HttpGet("[action]/{id}")]
        [Authorize(Policy = AuthorizationPolicies.RequireUserOrAdmin)]
        public async Task<ActionResult<ApiResponse>> GetUserById(int id)
        {
            if (id <= 0)
                return ApiResponse.ErrorResponse("Invalid user ID.");

            return await _usersRepository.GetUserById(id);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ApiResponse>> GetUserDetail()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) || userId == 0)
                return ApiResponse.ErrorResponse("Invalid user session.");

            return await _usersRepository.GetUserById(userId);
        }

        [HttpPost("[action]"), AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> PostUsers(UserSaveViewModel user)
        {
            return await _usersRepository.PostUsers(user);
        }

        [HttpPost("[action]"), AllowAnonymous]
        public async Task<ActionResult<ApiResponse>> AuthenticateUser(LoginViewModel login)
        {
            return await _usersRepository.AuthenticateUser(login);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUsers(int id, Users users)
        //{
        //    if (id != users.Id)
        //        return BadRequest();

        //    if (!await _usersRepository.UserExistsAsync(id))
        //        return NotFound();

        //    await _usersRepository.UpdateUserAsync(users);

        //    return NoContent();
        //}

        //[HttpPost]
        //public async Task<ActionResult<Users>> PostUsers(UserSaveViewModel user)
        //{
        //    await _usersRepository.AddUserAsync(user);
        //    return Ok();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUsers(int id)
        //{
        //    if (!await _usersRepository.UserExistsAsync(id))
        //        return NotFound();

        //    await _usersRepository.DeleteUserAsync(id);

        //    return NoContent();
        //}

    }
}
