using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers.Pagination;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string username);
        void AddUser(AppUser user);
        void UpdateUser(AppUser user);
        void DeleteUser(AppUser user);
        Task<bool> AnyUsersAsync();
        Task<PagedList<UserAdminDto>> GetUsersAsync(PaginationParams paginationParams);
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
    }
}