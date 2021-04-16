using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Extensions;
using API.Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MembersController : BaseApiController
    {
        private readonly UnitOfWork _unitOfWork;
        public MembersController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MemberDto>>> GetMembers([FromQuery] PaginationParams paginationParams)
        {
            var membersList = await _unitOfWork.MemberRepository.GetMembersAsync(paginationParams);

            Response.AddPaginationHeader(membersList);

            return membersList;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MemberDto>> GetMemberById([FromRoute] int id)
        => await _unitOfWork.MemberRepository.GetMemberByIdAsync(id);

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetMemberByUsername([FromRoute] string username)
        => await _unitOfWork.MemberRepository.GetMemberByUsernameAsync(username);
    }
}