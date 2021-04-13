using System.Threading.Tasks;
using API.DTOs;
using API.Helpers.Pagination;

namespace API.Interfaces
{
    public interface IMemberRepository
    {
        Task<PagedList<MemberDto>> GetMembersAsync(PaginationParams paginationParams);
        Task<MemberDto> GetMemberByUsernameAsync(string username);
        Task<MemberDto> GetMemberByIdAsync(int id);
    }
}