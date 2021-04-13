using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.Helpers.Pagination;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AdminController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-role")]
        public async Task<ActionResult<PagedList<UserAdminDto>>> GetUsers([FromQuery] PaginationParams paginationParams)
        {
            var users = await _unitOfWork.UserRepository.GetUsersAsync(paginationParams);

            Response.AddPaginationHeader(users);

            return users;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("change-role/{username}")]
        public async Task<ActionResult> ChangeUserRole([FromRoute] string username, string role)
        {
            if (User.GetUserName() == username.ToLower())
                return BadRequest("You can not change your own role");

            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);

            if (user is null) return NotFound($"Could not find user with username: '{username}'");

            user.UserRole = role;

            _unitOfWork.UserRepository.UpdateUser(user);

            if (!await _unitOfWork.Complete()) return BadRequest("Failed to change user role");

            return Ok(user.UserRole);
        }
    }
}